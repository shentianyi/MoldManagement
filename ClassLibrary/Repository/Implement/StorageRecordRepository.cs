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
    /// 库存记录仓库
    /// </summary>
    /// <param name="_context"></param>
    public class StorageRecordRepository : IStorageRecordRepository
    {
        private ToolManDataContext context;

        /// <summary>
        /// 实例化库存记录仓库
        /// </summary>
        /// <param name="_context"></param>
        public StorageRecordRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }
        /// <summary>
        /// 新建库存记录
        /// </summary>
        /// <param name="storageRecord">库存记录</param>
        public void Add(StorageRecord storageRecord)
        {
            context.StorageRecord.InsertOnSubmit(storageRecord);
        }

        /// <summary>
        /// 根据库存记录号获得库存记录
        /// </summary>
        /// <param name="storageNR">库存记录号</param>
        /// <returns>库存记录</returns>
        public StorageRecord GetByStorageNR(Guid storageNR)
        {
            StorageRecord storageRecord = context.StorageRecord.Single(sr => sr.StorageRecordNR.Equals(storageNR));
            return storageRecord;
        }

        /// <summary>
        /// 根据操作人员号获得库存记录
        /// </summary>
        /// <param name="operatorId">操作人员号</param>
        /// <returns>库存记录列表</returns>
        public List<StorageRecord> GetByOperatorId(string operatorId)
        {
            List<StorageRecord> records = context.StorageRecord.Where(record => record.OperatorId == operatorId).ToList();
            return records;
        }

        /// <summary>
        /// 根据库存记录类型获得库存记录
        /// </summary>
        /// <param name="recordType">库存记录类型</param>
        /// <returns>库存记录列表</returns>
        public List<StorageRecord> GetByRecordType(StorageRecordType storageType)
        {
            List<StorageRecord> records = context.StorageRecord.Where(record => record.RecordType == storageType).ToList();
            return records;
        }

        /// <summary>
        /// 根据起止日期获得库存记录
        /// </summary>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>库存记录列表</returns>
        public List<StorageRecord> GetByRecordDate(DateTime startDate, DateTime endDate)
        {
            List<StorageRecord> records = context.StorageRecord.Where(record => record.Date <= startDate && record.Date >= endDate).ToList();
            return records;
        }

        /// <summary>
        /// 根据模具号、操作人员号、起止日期获得库存记录
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>库存记录列表</returns>
        public List<StorageRecord> GetMoldApplyRecordsByMoldNR(string moldNR, string applicantId, DateTime? startDate, DateTime? endDate)
        {
            List<StorageRecord> records = context.StorageRecord
                .Where(v => (v.TargetNR.Equals(moldNR))
                && (startDate == null ? true : v.Date >= startDate)
                && (endDate == null ? true : v.Date <= endDate)
                && (string.IsNullOrEmpty(applicantId) ? true : v.ApplicantId.Equals(applicantId)))
                .OrderByDescending(v => v.Date).ToList();
            return records;
        }

        /// <summary>
        /// 根据模具号、操作人员号、起止日期获得库存记录，进行分页
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页数量</param>
        /// <returns>库存记录列表</returns>
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
        /// 根据库存记录目标号获得库存记录
        /// </summary>
        /// <param name="targetNR">目标号</param>
        /// <returns>库存记录</returns>
        public StorageRecord GetStorageRecordByTargerNR(string targetNR)
        {
            StorageRecord storageRecord = context.StorageRecord.Where(sr => sr.TargetNR.Equals(targetNR)).OrderByDescending(sr => sr.Date).First();
            return storageRecord;
        }


        /// <summary>
        /// 根据库存记录删除
        /// </summary>
        /// <param name="records">库存记录列表</param>
        public void Delete(List<StorageRecord> records)
        {
            context.StorageRecord.DeleteAllOnSubmit(records);
        }
        /// <summary>
        /// 获得全部库存记录
        /// </summary>
        /// <returns>库存记录列表</returns>
        public List<StorageRecord> All()
        {
            List<StorageRecord> records = context.StorageRecord.ToList();
            return records;
        }

        /// <summary>
        /// 根据类型，时间获得记录
        /// </summary>
        /// <param name="types">类型</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>库存记录列表</returns>
        public List<StorageRecord> GetByTypesDate(StorageRecordType[] types, DateTime? startDate, DateTime? endDate)
        {

            List<StorageRecord> records = context.StorageRecord.Where(v =>(types.Contains(v.RecordType))&&(startDate == null ? true : v.Date >= startDate)
               && (endDate == null ? true : v.Date <= endDate))
               .OrderBy(v => v.Date).ToList();
            return records;
        }
    }
}
