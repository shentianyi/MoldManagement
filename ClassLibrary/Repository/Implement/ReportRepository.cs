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
    /// 报告仓库
    /// </summary>
    public class ReportRepository : IReportRepository
    {
        ToolManDataContext context;

        /// <summary>
        /// 实例化报告仓库
        /// </summary>
        /// <param name="_context"></param>
        public ReportRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }

        /// <summary>
        /// 新建报告
        /// </summary>
        /// <param name="report">报告</param>
        public void Add(Report report)
        {
            context.Report.InsertOnSubmit(report);
        }

        /// <summary>
        /// 根据模具号获得报告视图
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <param name="operatorId">操作员号</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>报告视图列表</returns>
        public List<ReportView> GetReportViewByMoldNR(string moldNR, string operatorId, DateTime? startDate, DateTime? endDate)
        {
            List<ReportView> reports = context.ReportView.Where(v => v.MoldID.Equals(moldNR)
                 && (startDate == null ? true : v.Date >= startDate)
                  && (endDate == null ? true : v.Date <= endDate)
                  && (string.IsNullOrEmpty(operatorId) ? true : v.OperatorID.Equals(operatorId)))
                 .OrderByDescending(v => v.Date).ToList();
            return reports;
        }

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
        public List<ReportView> GetReportViewByMoldNR(string moldNR, int pageIndex, int pageSize, string operatorId, DateTime? startDate, DateTime? endDate)
        {
            List<ReportView> reports = context.ReportView.Where(v => v.MoldID.Equals(moldNR)
                 && (startDate == null ? true : v.Date >= startDate)
                  && (endDate == null ? true : v.Date <= endDate)
                  && (string.IsNullOrEmpty(operatorId) ? true : v.OperatorID.Equals(operatorId)))
                 .OrderByDescending(v => v.Date).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return reports;
        }

        /// <summary>
        /// 根据时间获得报告
        /// </summary>
        /// <param name="type">报告类型</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>报告视图列表</returns>
        public List<ReportView> GetReportViewByDate(ReportType type,DateTime? startDate, DateTime? endDate)
        {
           return context.ReportView.Where(v=> (v.ReportType==type)&&(startDate == null ? true : v.Date >= startDate)
                  && (endDate == null ? true : v.Date <= endDate))
                 .OrderBy(v => v.Date).ToList();
        }
    }
}
