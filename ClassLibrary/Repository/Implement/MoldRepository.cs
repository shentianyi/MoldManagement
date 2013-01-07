using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Data;
using ClassLibrary.ENUM;

namespace ClassLibrary.Repository.Implement
{

    /// <summary>
    /// 模具仓库
    /// </summary>
    public class MoldRepository : IMoldRepository
    {
        ToolManDataContext context;

        /// <summary>
        /// 实例化模具仓库
        /// </summary>
        /// <param name="_context"></param>
        public MoldRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }

        /// <summary>
        /// 新建模具
        /// </summary>
        /// <param name="mold">模具</param>
        public void Add(Mold mold)
        {
            context.Mold.InsertOnSubmit(mold);
        }

        /// <summary>
        /// 新建多个模具
        /// </summary>
        /// <param name="molds">模具列表</param>
        public void Add(List<Mold> molds)
        {
            context.Mold.InsertAllOnSubmit(molds);
        }

        /// <summary>
        /// 根据模具号删除模具
        /// </summary>
        /// <param name="moldNR">模具号</param>
        public void DeleteById(string moldNR)
        {
            Mold mold = GetById(moldNR);
            context.Mold.DeleteOnSubmit(mold);
        }

        /// <summary>
        /// 根据模具型号号删除模具
        /// </summary>
        /// <param name="moldTypeId">模具型号号</param>
        public void DeleteByMoldTypeId(string moldTypeId)
        {
            List<Mold> molds = GetByMoldTypeId(moldTypeId);
            context.Mold.DeleteAllOnSubmit(molds);
        }

        /// <summary>
        /// 根据模具号获得模具
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <returns>模具实例</returns>
        public Mold GetById(string moldNR)
        {
            Mold mold = context.Mold.SingleOrDefault(m => m.MoldNR.Equals(moldNR));
            return mold;
        }

        /// <summary>
        /// 根据模具型号号获得模具
        /// </summary>
        /// <param name="moldTypeId">模具型号号</param>
        /// <returns>模具列表</returns>
        public List<Mold> GetByMoldTypeId(string moldTypeId)
        {
            List<Mold> molds = context.Mold.Where(m => m.MoldTypeID.Equals(moldTypeId)).ToList();
            return molds;
        }



        /// <summary>
        /// 根据项目号获得模具
        /// </summary>
        /// <param name="projectId">项目号</param>
        /// <returns>模具列表</returns>
        public List<Mold> GetByProjectId(string projectId)
        {
            List<Mold> molds = (context.Mold.Where(m => m.ProjectID.Equals(projectId))).ToList();
            return molds;
        }

        /// <summary>
        /// 根据模具状态获得模具
        /// </summary>
        /// <param name="moldState">模具状态</param>
        /// <returns>模具列表</returns>
        public List<Mold> GetByState(MoldStateType moldState)
        {
            List<Mold> molds = (context.Mold.Where(m => m.State == moldState)).ToList();
            return molds;
        }

        /// <summary>
        ///  根据模具号获得模具视图
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <returns>模具视图</returns>
        public MoldView GetMoldViewByMoldNR(string moldNR)
        {
            MoldView moldview = context.MoldView.Where(mv => mv.MoldNR.Equals(moldNR)).First();
            return moldview;
        }

        ///  <summary>
        /// 根据多条件获得模具
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>模具列表</returns>
        public List<MoldView> GetByMutiConditions(MoldSearchCondition condition)
        {
            List<MoldView> molds = !string.IsNullOrEmpty(condition.MoldNR) ?
                 context.MoldView.Where(m => m.MoldNR.Equals(condition.MoldNR))
                 .ToList() :
                 context.MoldView
                 .Where(m => (string.IsNullOrEmpty(condition.MoldTypeId) ? true : m.MoldTypeID.Equals(condition.MoldTypeId))
                      && (string.IsNullOrEmpty(condition.ProjectId) ? true : m.ProjectID.Equals(condition.ProjectId))
                     && (condition.State == MoldStateType.NULL ? true : m.State == condition.State))
                    .ToList();
            molds = molds.Distinct().ToList();

            return molds;
        }


        /// <summary>
        /// 根据模具警报状态获得模具视图
        /// </summary>
        /// <param name="type">模具警报状态</param>
        /// <returns>模具视图列表</returns>
        public List<MoldView> GetByWarnType(MoldWarnType type)
        {
            List<MoldView> molds = type == MoldWarnType.OutTime ?
                (context.MoldView.Where(m => (m.State == MoldStateType.NotReturned) && (m.LastRecordDate.Value.AddHours((double)m.MaxLendHour) < DateTime.Now)).ToList())
                : (context.MoldView.Where(m => (m.State == MoldStateType.NotReturned) && (m.LastRecordDate.Value.AddHours((double)m.MaxLendHour) >= DateTime.Now)).ToList());
            return molds.Distinct().ToList();
        }

        /// <summary>
        /// 根据库存记录号获得模具当前位置
        /// </summary>
        /// <param name="storageRecordNR">库存记录号</param>
        /// <returns>模具当前位置</returns>
        public string GetMoldCurrPosiByRecordNR(Guid storageRecordNR)
        {
            string position = string.Empty;
            return context.StorageRecord.Single(sr => sr.StorageRecordNR.Equals(storageRecordNR)).Destination;
        }

        /// <summary>
        /// 根据模具号获得模具当前位置
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <returns>模具当前位置</returns>
        public string GetMoldCurrPosiByMoldNR(string moldNR)
        {
            Guid storageRecordNR = context.MoldLastRecord.Single(r => r.MoldNR.Equals(moldNR)).StorageRecordNR;

            return GetMoldCurrPosiByRecordNR(storageRecordNR);
        }

        /// <summary>
        /// 根据模具号判断模具是否存在
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <returns>判断结果</returns>
        public bool MoldExsit(string moldNR)
        {
            if (context.Mold.Where(m => m.MoldNR.Equals(moldNR)).Count() > 0)
                return true;
            return false;
        }

        /// <summary>
        /// 根据库存位置号获得模具号
        /// </summary>
        /// <param name="posiNr">库存位置号</param>
        /// <returns>模具号</returns>
        public string GetMoldNrByPosiNr(string posiNr)
        {
            UniqStorage s = context.UniqStorage.SingleOrDefault(u => u.Position.PositionNR.Equals(posiNr));
            return s == null ? "" : s.UniqNR;
        }

        /// <summary>
        /// 获得全部模具
        /// </summary>
        /// <returns>模具列表</returns>
        public List<Mold> All()
        {
            return (context.Mold).ToList();
        }

    }
}
