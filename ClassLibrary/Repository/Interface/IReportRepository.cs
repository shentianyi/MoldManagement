using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;
using ClassLibrary.ENUM;

namespace ClassLibrary.Repository.Interface
{
    public interface IReportRepository
    {

        /// <summary>
        /// 新建报告
        /// </summary>
        /// <param name="report">报告</param>
        void Add(Report report);

        /// <summary>
        /// 根据模具号获得报告视图
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <param name="operatorId">操作员号</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>报告视图列表</returns>
        List<ReportView> GetReportViewByMoldNR(string moldNR, string operatorId, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// 根据模具号获得报告，进行分页
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <param name="operatorId">操作员号</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页数量</param>
        /// <returns>报告视图列表</returns>
        List<ReportView> GetReportViewByMoldNR(string moldNR, int pageIndex, int pageSize, string operatorId, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// 根据时间获得报告
        /// </summary>
        /// <param name="type">报告类型</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>报告视图列表</returns>
        List<ReportView> GetReportViewByDate(ReportType type, DateTime? startDate, DateTime? endDate);     
    }
}
