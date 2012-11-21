using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Data;
using ClassLibrary.ENUM;

namespace ClassLibrary.Repository.Implement
{
    public class MoldRepository : IMoldRepository
    {
        ToolManDataContext context;

        public MoldRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }

        // add mold
        public void Add(Mold mold)
        {
            context.Mold.InsertOnSubmit(mold);
        }

        // add molds
        public void Add(List<Mold> molds)
        {
            context.Mold.InsertAllOnSubmit(molds);
        }

        // delete mold by mold id
        public void DeleteById(string moldNR)
        {
            Mold mold = GetById(moldNR);
            context.Mold.DeleteOnSubmit(mold);
        }

        // delete molds by mold type id
        public void DeleteByMoldTypeId(string moldTypeId)
        {
            List<Mold> molds = GetByMoldTypeId(moldTypeId);
            context.Mold.DeleteAllOnSubmit(molds);
        }

        // get mold by mold id
        public Mold GetById(string moldNR)
        {
            Mold mold = context.Mold.Single(m => m.MoldNR.Equals(moldNR));
            return mold;
        }

        // get molds by mold type id
        public List<Mold> GetByMoldTypeId(string moldTypeId)
        {
            List<Mold> molds = context.Mold.Where(m => m.MoldTypeID.Equals( moldTypeId)).ToList();
            return molds;
        }

      

        /// <summary>
        /// get the molds by its project id
        /// </summary>
        /// <param name="projectId">the id of the mold's project id</param>
        /// <returns>the list of molds</returns>
        public List<Mold> GetByProjectId(string projectId)
        {
            List<Mold> molds = (context.Mold.Where(m => m.ProjectID.Equals( projectId))).ToList();
            return molds;
        }

        /// <summary>
        /// get molds by its state
        /// </summary>
        /// <param name="moldState">the state of mold</param>
        /// <returns>the list of molds</returns>
        public List<Mold> GetByState(MoldStateType moldState)
        {
            List<Mold> molds = (context.Mold.Where(m => m.State == moldState)).ToList();
            return molds;
        }

        /// <summary>
        ///  get mold detail by mold nr
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <returns>the moldview</returns>
        public MoldView GetMoldViewByMoldNR(string moldNR)
        {
            MoldView moldview = context.MoldView.Where(mv => mv.MoldNR.Equals(moldNR)).First();
            return moldview;
        }

        /// <summary>
        /// get the list of mold by the muti conditions
        /// </summary>
        /// <returns>the list of mold</returns>
        public List<MoldView> GetByMutiConditions(MoldSearchCondition condition)
        {

            //List<MoldView> molds = !string.IsNullOrEmpty(condition.MoldNR) ?
            //      context.MoldView.Where(m => m.MoldNR.Equals(condition.MoldNR))
            //      .ToList() :
            //      context.MoldView
            //      .Where(m => (string.IsNullOrEmpty(condition.MoldTypeId) ? true : m.MoldCateID.Equals(condition.MoldTypeId))
            //           && (string.IsNullOrEmpty(condition.ProjectId) ? true : m.ProjectID.Equals(condition.ProjectId))
            //          && (condition.State == MoldStateType.NULL ? true : m.State == condition.State))
            //         .ToList();
            List<MoldView> molds = !string.IsNullOrEmpty(condition.MoldNR) ?
                 context.MoldView.Where(m => m.MoldNR.Equals(condition.MoldNR))
                 .ToList() :
                 context.MoldView
                 .Where(m => (string.IsNullOrEmpty(condition.MoldTypeId) ? true : m.MoldTypeID.Equals(condition.MoldTypeId))
                      && (string.IsNullOrEmpty(condition.ProjectId) ? true : m.ProjectID.Equals(condition.ProjectId))
                     && (condition.State == MoldStateType.NULL ? true : m.State == condition.State))
                    .ToList();


            //if (string.IsNullOrEmpty(condition.MoldNR))
            //{
            //    switch (condition.Warn)
            //    {
            //        case MoldWarnType.OutTime:
            //            {
            //                molds = molds.Where(m => (m.State == MoldStateType.NotReturned) && (m.LastRecordDate.Value.AddHours((double)m.MaxLendHour) < DateTime.Now)).ToList();
            //                break;
            //            }
            //        case MoldWarnType.InTime:
            //            {
            //                molds = molds.Where(m => (m.State == MoldStateType.NotReturned) && (m.LastRecordDate.Value.AddHours((double)m.MaxLendHour) >= DateTime.Now)).ToList();
            //                break;
            //            }
            //    }
            //}

            molds = molds.Distinct().ToList();

            return molds;
        }


        public List<MoldView> GetByWarnType(MoldWarnType type)
        {
            List<MoldView> molds = type == MoldWarnType.OutTime ?
                (context.MoldView.Where(m => (m.State == MoldStateType.NotReturned) && (m.LastRecordDate.Value.AddHours((double)m.MaxLendHour) < DateTime.Now)).ToList())
                : (context.MoldView.Where(m => (m.State == MoldStateType.NotReturned) && (m.LastRecordDate.Value.AddHours((double)m.MaxLendHour) >= DateTime.Now)).ToList());
            return molds.Distinct().ToList();
        }
    
        /// <summary>
        /// gett current position of mold by storage record nr
        /// </summary>
        /// <param name="storageRecordNR"></param>
        /// <returns></returns>
        public string GetMoldCurrPosiByRecordNR(Guid storageRecordNR)
        {
            string position = string.Empty;
            return context.StorageRecord.Single(sr => sr.StorageRecordNR.Equals(storageRecordNR)).Destination;
        }

        /// <summary>
        /// get mold current position by mold nr
        /// </summary>
        /// <param name="moldNR"></param>
        /// <returns></returns>
        public string GetMoldCurrPosiByMoldNR(string moldNR)
        {
            Guid storageRecordNR = context.MoldLastRecord.Single(r => r.MoldNR.Equals(moldNR)).StorageRecordNR;

            return GetMoldCurrPosiByRecordNR(storageRecordNR);
        }

        /// <summary>
        /// judege if mold exsits by mold nr
        /// </summary>
        /// <param name="moldNR"></param>
        /// <returns></returns>
        public bool MoldExsit(string moldNR)
        {
            if (context.Mold.Where(m => m.MoldNR.Equals(moldNR)).Count()> 0)
                return true;
            return false;
        }

        /// <summary>
        /// get mold nr by the position nr
        /// </summary>
        /// <param name="posiNr"></param>
        /// <returns></returns>
        public string GetMoldNrByPosiNr(string posiNr)
        {
           UniqStorage  s= context.UniqStorage.SingleOrDefault(u => u.Position.PositionNR.Equals(posiNr));
           return s == null ? "" : s.UniqNR;
        }
    
        // get all molds
        public List<Mold> All()
        {
            return (context.Mold).ToList();
        }

    }
}
