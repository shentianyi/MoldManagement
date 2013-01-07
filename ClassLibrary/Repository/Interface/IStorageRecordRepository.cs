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
        /// 新建库存记录
        /// </summary>
        /// <param name="storageRecord">库存记录</param>
        void Add(StorageRecord storageRecord);

        /// <summary>
        /// 根据库存记录号获得库存记录
        /// </summary>
        /// <param name="storageNR">库存记录号</param>
        /// <returns>库存记录</returns>
        StorageRecord GetByStorageNR(Guid storageNR);

        /// <summary>
        /// 根据操作人员号获得库存记录
        /// </summary>
        /// <param name="operatorId">操作人员号</param>
        /// <returns>库存记录列表</returns>
        List<StorageRecord> GetByOperatorId(string operatorId);

        /// <summary>
        /// 根据起止日期获得库存记录
        /// </summary>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>库存记录列表</returns>
        List<StorageRecord> GetByRecordType(StorageRecordType recordType);

        /// <summary>
        /// 根据模具号、操作人员号、起止日期获得库存记录
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>库存记录列表</returns>
        List<StorageRecord> GetByRecordDate(DateTime startDate, DateTime endDate);

        /// <summary>
        /// 根据模具号、操作人员号、起止日期获得库存记录
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>库存记录列表</returns>
        List<StorageRecord> GetMoldApplyRecordsByMoldNR(string moldNR, string applicantId, DateTime? startDate, DateTime? endDate);


        /// <summary>
        /// 根据模具号、操作人员号、起止日期获得库存记录，进行分页
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页数量</param>
        /// <returns>库存记录列表</returns>
        List<StorageRecord> GetMoldApplyRecordsByMoldNR(string moldNR, int pageIndex, int pageSize, string applicantId, DateTime? startDate, DateTime? endDate);


        /// <summary>
        /// 根据库存记录目标号获得库存记录
        /// </summary>
        /// <param name="targetNR">目标号</param>
        /// <returns>库存记录</returns>
        StorageRecord GetStorageRecordByTargerNR(string targetNR);

        /// <summary>
        /// 根据库存记录删除
        /// </summary>
        /// <param name="records">库存记录列表</param>
        void Delete(List<StorageRecord> records);

        /// <summary>
        /// 获得全部库存记录
        /// </summary>
        /// <returns>库存记录列表</returns>
        List<StorageRecord> All();

        /// <summary>
        /// 根据类型，时间获得记录
        /// </summary>
        /// <param name="types">类型</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>库存记录列表</returns>
        List<StorageRecord> GetByTypesDate(StorageRecordType[] types, DateTime? startDate, DateTime? endDate);

    }
}
