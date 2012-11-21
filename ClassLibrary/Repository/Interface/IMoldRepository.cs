using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;
using ClassLibrary.ENUM;

namespace ClassLibrary.Repository.Interface
{
    public interface IMoldRepository
    {
        // add mold
        void Add(Mold mold);

        // add molds
        void Add(List<Mold> molds);

        // delete mold by mold id
        void DeleteById(string id);

        // delete molds by mold type d
        void DeleteByMoldTypeId(string moldTypeId);

        // get mold by mold id
        Mold GetById(string id);

        // get molds by mold type id
        List<Mold> GetByMoldTypeId(string moldTypeId);

        // get molds by project id
        List<Mold> GetByProjectId(string projectId);

        /// <summary>
        /// get molds by its state
        /// </summary>
        /// <param name="moldState">the state of mold</param>
        /// <returns>the list of molds</returns>
        List<Mold> GetByState(MoldStateType moldState);

        /// <summary>
        ///  get mold detail by mold nr
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <returns>the moldview</returns>
        MoldView GetMoldViewByMoldNR(string moldNR);

        /// <summary>
        /// get the list of mold view by the muti conditions
        /// </summary>
        /// <returns>the list of mold view</returns>
        List<MoldView> GetByMutiConditions(MoldSearchCondition conditions);


        List<MoldView> GetByWarnType(MoldWarnType type);
         /// <summary>
        /// get current position of mold by storage record nr
        /// </summary>
        /// <param name="storageRecordNR"></param>
        /// <returns></returns>
        string GetMoldCurrPosiByRecordNR(Guid storageRecordNR);

        /// <summary>
        /// get mold current position by mold nr
        /// </summary>
        /// <param name="moldNR"></param>
        /// <returns></returns>
        string GetMoldCurrPosiByMoldNR(string moldNR);

        /// <summary>
        /// judege if mold exsits by mold nr
        /// </summary>
        /// <param name="moldNR"></param>
        /// <returns></returns>
        bool MoldExsit(string moldNR);


        /// <summary>
        /// get mold nr by the position nr
        /// </summary>
        /// <param name="posiNr"></param>
        /// <returns></returns>
        string GetMoldNrByPosiNr(string posiNr);

        // get all molds
        List<Mold> All();
    }
}
