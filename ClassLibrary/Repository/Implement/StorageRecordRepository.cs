using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Data;
using ClassLibrary.ENUM;

namespace ClassLibrary.Repository.Implement
{
    public class StorageRecordRepository : IStorageRecordRepository
    {
        private ToolManDataContext context;

        public StorageRecordRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }
        /// <summary>
        /// add one storage record
        /// </summary>
        /// <param name="storageRecord">the instance of stroage</param>
        public void Add(StorageRecord storageRecord)
        {
            context.StorageRecord.InsertOnSubmit(storageRecord);
        }

        /// <summary>
        /// get storage record by the storage nr
        /// </summary>
        /// <param name="storageNR">nr of storage record's storage's nr</param>
        /// <returns>storage record</returns>
      public  StorageRecord GetByStorageNR(Guid storageNR)
        {
            StorageRecord storageRecord = context.StorageRecord.Single(sr => sr.StorageRecordNR.Equals(storageNR));
            return storageRecord;
      }

        /// <summary>
        /// get storage records by the operator id
        /// </summary>
        /// <param name="operatorId">the id of operator</param>
        /// <returns>the list of record</returns>
        public List<StorageRecord> GetByOperatorId(string operatorId)
        {
            List<StorageRecord> records = context.StorageRecord.Where(record => record.OperatorId== operatorId).ToList();
            return records;
        }

        /// <summary>
        /// get storage records by the record type --IN or OUT
        /// </summary>
        /// <param name="recordType">the enum tpey param of record type</param>
        /// <returns>the lisf of record</returns>
        public List<StorageRecord> GetByRecordType(StorageRecordType storageType)
        {
            List<StorageRecord> records = context.StorageRecord.Where(record => record.RecordType == storageType).ToList();
            return records;
        }

        /// <summary>
        ///  get storage records by the startdate and enddate when the record recorded
        /// </summary>
        /// <param name="startDate">the date set as the start condition</param>
        /// <param name="endDate">the date set as the end condition</param>
        /// <returns>the list storeage record</returns>
        public List<StorageRecord> GetByRecordDate(DateTime startDate, DateTime endDate)
        {
            List<StorageRecord> records = context.StorageRecord.Where(record => record.Date <= startDate && record.Date >= endDate).ToList();
            return records;
        }

        /// <summary>
        /// get the mold apply records by mold id 
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <param name="endDate"></param>
        /// <param name="startDate"></param>
        /// <returns>the list of the mold apply records</returns>
        public List<StorageRecord> GetMoldApplyRecordsByMoldNR(string moldNR, string applicantId, DateTime? startDate, DateTime? endDate)
        {
            List<StorageRecord> records = context.StorageRecord
                .Where(v =>( v.TargetNR.Equals(moldNR))
                && (startDate == null ? true : v.Date >= startDate)
                && (endDate == null ? true : v.Date <= endDate)
                &&( string.IsNullOrEmpty(applicantId) ? true : v.ApplicantId.Equals(applicantId)))
                .OrderByDescending(v => v.Date).ToList();
            return records;
        }

        /// <summary>
        /// get the mold apply records by mold id, page, pageSize
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <param name="pageIndex">the index of page</param>
        /// <param name="pageSize">the size of page</param>
        /// <returns></returns>
        public List<StorageRecord> GetMoldApplyRecordsByMoldNR(string moldNR, int pageIndex, int pageSize, string applicantId, DateTime? startDate, DateTime? endDate)
        {
            List<StorageRecord> records = context.StorageRecord.Where(v => (v.TargetNR.Equals(moldNR))
                && (startDate == null ? true : v.Date >= startDate)
                && (endDate == null ? true : v.Date <= endDate)
                && (string.IsNullOrEmpty(applicantId) ? true : v.ApplicantId.Equals(applicantId)))
                .OrderByDescending(v => v.Date).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return records;
        }

        /// <summary>
        /// get storage record by target nr
        /// </summary>
        /// <param name="targetNR">nr of target</param>
        /// <returns></returns>
        public StorageRecord GetStorageRecordByTargerNR(string targetNR)
        {
            StorageRecord storageRecord = context.StorageRecord.Where(sr => sr.TargetNR.Equals(targetNR)).OrderByDescending(sr => sr.Date).First();
            return storageRecord;
        }


        /// <summary>
        /// delete the records
        /// </summary>
        /// <param name="records">the list of records</param>
        public void Delete(List<StorageRecord> records)
        {
            context.StorageRecord.DeleteAllOnSubmit(records);
        }
        /// <summary>
        /// get all storage records
        /// </summary>
        /// <returns>the list of all storage records</returns>
        public List<StorageRecord> All()
        {
            List<StorageRecord> records = context.StorageRecord.ToList();
            return records;
        }
    }
}
