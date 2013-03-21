using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ClassLibrary.Data;
using ToolingWCF.DataModel;
using ClassLibrary.ENUM;

namespace ToolingWCF
{
    [ServiceContract]
    public interface IMoldPartInfoService
    {
        /// <summary>
        /// get the molds in form of list by the selected conditions
        /// </summary>
        /// <param name="conditions">the instance of condition</param>
        /// <returns>the list of mold</returns>
        [OperationContract]
        List<MoldBaseInfo> GetMoldByMutiConditions(MoldSearchCondition conditions);

        /// <summary>
        /// get the mold  by mold id
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <returns>the instance of Mold</returns>
        [OperationContract]
        MoldBaseInfo GetMoldBaseInfoByNR(string moldNR);

   

        /// <summary>
        /// get the mold dynamic info
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <returns></returns>
        [OperationContract]
        MoldDynamicInfo GetMoldDynamicInfoByMoldNR(string moldNR);

        /// <summary>
        /// get the mold apply records by mold id 
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <param name="endDate"></param>
        /// <param name="startDate"></param>
        /// <returns>the list of the mold apply records</returns>
        [OperationContract]
        List<StorageRecord> GetMoldApplyRecords(string moldNR, string applicantId, DateTime? startDate, DateTime? endDate);


        /// <summary>
        /// get the mold apply records by mold id, page, pageSize
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <param name="pageIndex">the index of page</param>
        /// <param name="pageSize">the size of page</param>
        /// <returns></returns>
        [OperationContract]
        List<StorageRecord> GetMoldApplyRecordsInPages(string moldNR, int pageIndex, int pageSize, string applicantId, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// get the list of report by its mold id
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <returns>the list of report</returns>
        [OperationContract]
        List<MoldReleaseInfo> GetMoldReleaseInfoByMoldNR(string moldNR, string operatorId, DateTime? startDate, DateTime? endDate);


        /// <summary>
        /// get the reports by mold nr, page, pageSize
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <param name="pageIndex">the index of page</param>
        /// <param name="pageSize">the size of page</param>
        /// <returns></returns>
        [OperationContract]
        List<MoldReleaseInfo> GetMoldReleaseInfoByMoldNRInPage(string moldNR, int pageIndex, int pageSize, string operatorId, DateTime? startDate, DateTime? endDate);


        /// <summary>
        /// get file from server by file name
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] GetFileByName(string fileName);

        /// <summary>
        /// get mold by it warn type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [OperationContract]
        List<MoldWarnInfo> GetMoldWarnInfo(MoldWarnType type);

        /// <summary>
        /// get mold position by its mold nr
        /// </summary>
        /// <param name="moldNR"></param>
        /// <returns></returns>
        [OperationContract]
        Position GetMoldPositionByNr(string moldNR);

        /// <summary>
        /// get mold nr by position nr
        /// </summary>
        /// <param name="posiNr"></param>
        /// <returns></returns>
        [OperationContract]
        string GetMoldNrByPosiNr(string posiNr);

        /// <summary>
        /// get mold current position
        /// </summary>
        /// <param name="moldNr"></param>
        /// <returns></returns>
        [OperationContract]
        string GetMoldCurrentPosition(string moldNr);

        [OperationContract]
        List<ReportView> GetReportViewByDate(ReportType type, DateTime? startDate, DateTime? endDate);

        [OperationContract]
        List<StorageRecord> GetStoreRecordByDate(int type, DateTime? startDate, DateTime? endDate);

        [OperationContract]
        bool DelAttachmentById(int attachId,string filePath);
    }
}
