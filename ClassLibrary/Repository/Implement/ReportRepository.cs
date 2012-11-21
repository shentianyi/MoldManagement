using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Implement
{
    public class ReportRepository : IReportRepository
    {
        ToolManDataContext context;

        public ReportRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }

        /// <summary>
        /// add one report
        /// </summary>
        /// <param name="report">the instance of report</param>
        public void Add(Report report)
        {
            context.Report.InsertOnSubmit(report);
        }

        /// <summary>
        /// get the Report Attachment by id of report
        /// </summary>
        /// <param name="reportId">id of report</param>
        /// <returns>the list of Report Attachment</returns>
        //public List<ReportAttachment> GetReportAttachmentById(int reportId)
        // {
        //     List<ReportAttachment> attaches = context.ReportAttachment.Where(r => r.ReportID == reportId).ToList();
        //     return attaches;
        //}

        /// <summary>
        /// get the list of report by its mold id
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <returns>the list of report</returns>
        public List<ReportView> GetReportViewByMoldNR(string moldNR, string operatorId, DateTime? startDate, DateTime? endDate)
        {
            List<ReportView> reports = context.ReportView.Where(v => v.MoldID.Equals(moldNR)
                 &&( startDate == null ? true : v.Date >= startDate)
                  &&(endDate == null ? true : v.Date <= endDate)
                  &&(string.IsNullOrEmpty(operatorId) ? true : v.OperatorID.Equals(operatorId)))
                 .OrderByDescending(v => v.Date).ToList();
            return reports;
        }

        /// <summary>
        /// get the reports by mold id, page, pageSize
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <param name="pageIndex">the index of page</param>
        /// <param name="pageSize">the size of page</param>
        /// <returns></returns>
        public List<ReportView> GetReportViewByMoldNR(string moldNR, int pageIndex, int pageSize, string operatorId, DateTime? startDate, DateTime? endDate)
        {
            List<ReportView> reports = context.ReportView.Where(v => v.MoldID.Equals(moldNR)
                 && (startDate == null ? true : v.Date >= startDate)
                  && (endDate == null ? true : v.Date <= endDate)
                  && (string.IsNullOrEmpty(operatorId) ? true : v.OperatorID.Equals(operatorId)))
                 .OrderByDescending(v => v.Date).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return reports;
        }

        ///// <summary>
        ///// get the list of report by its operator id
        ///// </summary>
        ///// <param name="operatorId">the id of operator</param>
        ///// <returns>the list of operator</returns>
        //List<Report> GetByOperatorId(string operatorId);

        ///// <summary>
        /////  get reports by the startdate and enddate when the report created
        ///// </summary>
        ///// <param name="startDate">the date set as the start condition</param>
        ///// <param name="endDate">the date set as the end condition</param>
        ///// <returns>the list storeage record</returns>
        //List<Report> GetByReportDate(DateTime startDate, DateTime endDate);

        ///// <summary>
        ///// delete none report by its id
        ///// </summary>
        ///// <param name="reportId">the id of report</param>
        //void DeleteById(int reportId);

    }
}
