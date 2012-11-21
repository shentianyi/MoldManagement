using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Interface
{
    public interface IReportRepository
    {

        /// <summary>
        /// add one report
        /// </summary>
        /// <param name="report">the instance of report</param>
        void Add(Report report);



        /// <summary>
        /// get the list of report by its mold id
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <returns>the list of report</returns>
        List<ReportView> GetReportViewByMoldNR(string moldNR, string operatorId, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// get the reports by mold id, page, pageSize
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <param name="pageIndex">the index of page</param>
        /// <param name="pageSize">the size of page</param>
        /// <returns></returns>
        List<ReportView> GetReportViewByMoldNR(string moldNR, int pageIndex, int pageSize, string operatorId, DateTime? startDate, DateTime? endDate);
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
