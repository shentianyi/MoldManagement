using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;
using ClassLibrary.ENUM;

namespace ClassLibrary.Repository.Interface
{
    public interface IStorageRecordRepository
    {
       /// <summary>
       /// add one storage record
       /// </summary>
       /// <param name="storageRecord">the instance of stroage</param>
        void Add(StorageRecord storageRecord);

        /// <summary>
        /// get storage record by the storage nr
        /// </summary>
        /// <param name="storageNR">nr of storage record's storage's nr</param>
        /// <returns>storage record</returns>
      StorageRecord GetByStorageNR(Guid storageNR);

        /// <summary>
        /// get storage records by the operator id
        /// </summary>
        /// <param name="operatorId">the id of operator</param>
        /// <returns>the list of record</returns>
        List<StorageRecord> GetByOperatorId(string operatorId);

        /// <summary>
        /// get storage records by the record type
        /// </summary>
        /// <param name="recordType">the enum tpey param of record type</param>
        /// <returns>the lisf of record</returns>
        List<StorageRecord> GetByRecordType(StorageRecordType recordType);

        /// <summary>
        ///  get storage records by the startdate and enddate when the record recorded
        /// </summary>
        /// <param name="startDate">the date set as the start condition</param>
        /// <param name="endDate">the date set as the end condition</param>
        /// <returns>the list storeage record</returns>
        List<StorageRecord> GetByRecordDate(DateTime startDate, DateTime endDate);

         /// <summary>
        /// get the mold apply records by mold id 
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <param name="endDate"></param>
        /// <param name="startDate"></param>
        /// <returns>the list of the mold apply records</returns>
        List<StorageRecord> GetMoldApplyRecordsByMoldNR(string moldNR, string applicantId,DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// get the mold apply records by mold id, page, pageSize
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <param name="pageIndex">the index of page</param>
        /// <param name="pageSize">the size of page</param>
        /// <returns></returns>
      List<StorageRecord> GetMoldApplyRecordsByMoldNR(string moldNR,int pageIndex,int pageSize, string applicantId, DateTime? startDate, DateTime? endDate);
       
         /// <summary>
        /// get storage record by target nr
        /// </summary>
        /// <param name="targetNR">nr of target</param>
        /// <returns></returns>
       StorageRecord GetStorageRecordByTargerNR(string targetNR);

        /// <summary>
        /// delete the records
        /// </summary>
        /// <param name="records">the list of records</param>
        void Delete(List<StorageRecord> records);

        // <summary>
        /// get all storage records
        /// </summary>
        /// <returns>the list of all storage records</returns>
        List<StorageRecord> All();

    }
}
