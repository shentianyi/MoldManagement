using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ClassLibrary.Data;
using ClassLibrary.Repository.Implement;
using ClassLibrary.Repository.Interface;
using ToolingWCF.DataModel;
using ClassLibrary.ENUM;
using ClassLibrary.Utilities;
using ToolingWCF.Utilities;
using ToolingWCF.Properties;
using System.IO;
using System.Transactions;
using ClassLibrary.Utility;

namespace ToolingWCF
{
    public class MoldPartInfoService : IMoldPartInfoService
    {
        /// <summary>
        /// get the molds in form of list by the selected conditions
        /// </summary>
        /// <param name="conditions">the instance of condition</param>
        /// <returns>the list of mold</returns>
        public List<MoldBaseInfo> GetMoldByMutiConditions(MoldSearchCondition conditions)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IMoldRepository moldRepostitory = new MoldRepository(unitwork);
                List<MoldView> molds = moldRepostitory.GetByMutiConditions(conditions);
                List<MoldBaseInfo> moldBaseInfos = new List<MoldBaseInfo>();

                foreach (MoldView m in molds)
                {
                    MoldBaseInfo moldBaseInfo = new MoldBaseInfo()
                    {
                        MoldNR = m.MoldNR,
                        Name = m.Name,
                        Type = m.TypeName,
                        State = m.State,
                        StateCN = m.StateCN,
                        ProjectId = m.ProjectID,
                        ProjectName = m.ProjectName,
                        CurrentPosition = m.StorageRecordNR.HasValue ? moldRepostitory.GetMoldCurrPosiByRecordNR((Guid)m.StorageRecordNR) : string.Empty
                    };
                    moldBaseInfos.Add(moldBaseInfo);
                }

                return moldBaseInfos;
            }
        }

        /// <summary>
        /// get the mold by mold id
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <returns>the instance of Mold</returns>
        public MoldBaseInfo GetMoldBaseInfoByNR(string moldNR)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IMoldRepository moldRepostitory = new MoldRepository(unitwork);
                IAttachmentRepository attachRep = new AttachmentRepository(unitwork);
                IPositionRepository posiRep = new PositionRepository(unitwork);

                MoldView m = moldRepostitory.GetMoldViewByMoldNR(moldNR);
                if (m != null)
                {
                    MoldBaseInfo mb = new MoldBaseInfo()
                    {
                        MoldNR = m.MoldNR,
                        Name = m.Name,
                        Type = m.TypeName,
                        Position = posiRep.GetByFacilictyNR(moldNR).PositionNR,
                        Producer = m.Producer,
                        Material = m.Material,
                        Weight = m.Weight == null ? string.Empty : m.Weight.ToString(),
                        State = m.State,
                        StateCN = m.StateCN,
                        ProjectId = m.ProjectID,
                        ProjectName = m.ProjectName,
                        Attach = attachRep.GetByMasterNR(moldNR)
                    };
                    return mb;
                }
                return null;
            }
        }




        /// <summary>
        /// get the mold dynamic info
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <returns>the instance of mold dynamic info</returns>
        public MoldDynamicInfo GetMoldDynamicInfoByMoldNR(string moldNR)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IMoldRepository moldRepostitory = new MoldRepository(unitwork);

                MoldView moldview = moldRepostitory.GetMoldViewByMoldNR(moldNR);

                if (moldview == null)
                    return null;


                IStorageRecordRepository storageRep = new StorageRecordRepository(unitwork);
                StorageRecord storageRecord = null;
                if (moldview.StorageRecordNR != null)
                {
                    storageRecord = storageRep.GetByStorageNR((Guid)moldview.StorageRecordNR);
                }

                MoldDynamicInfo moldDynamicInfo = new MoldDynamicInfo()
                {
                    CurrentPosition = moldview.StorageRecordNR == null ? string.Empty : moldRepostitory.GetMoldCurrPosiByRecordNR((Guid)moldview.StorageRecordNR),
                    Operator = storageRecord == null ? string.Empty : storageRecord.OperatorId,
                    OperateTime = storageRecord == null ? string.Empty : storageRecord.Date.ToString(),
                    AllowedCuttedTime = moldview.MaxCuttimes,
                    CurrentCuttedTime = moldview.CurrentCuttimes,
                    ReleaseCycle = moldview.ReleaseCycle,
                    LastReleasedTime = moldview.LastReleasedDate.ToString(),
                    MantainCycle = moldview.MaintainCycle,
                    LastMantainTime = moldview.LastMainedDate.ToString(),
                    State = moldview.State,
                    StateCN = EnumUtil.GetEnumDescriptionByEnumValue(moldview.State)
                };
                return moldDynamicInfo;
            }
        }

        /// <summary>
        /// get the mold apply records by mold id 
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <returns>the list of the mold apply records</returns>
        public List<StorageRecord> GetMoldApplyRecords(string moldNR, string applicantId, DateTime? startDate, DateTime? endDate)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IStorageRecordRepository recordRepositroy = new StorageRecordRepository(unitwork);
                List<StorageRecord> records = recordRepositroy.GetMoldApplyRecordsByMoldNR(moldNR, applicantId, startDate, endDate);
                return records;
            }
        }


        /// <summary>
        /// get the mold apply records by mold id, page, pageSize
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <param name="pageIndex">the index of page</param>
        /// <param name="pageSize">the size of page</param>
        /// <returns></returns>
        public List<StorageRecord> GetMoldApplyRecordsInPages(string moldNR, int pageIndex, int pageSize, string applicantId, DateTime? startDate, DateTime? endDate)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IStorageRecordRepository recordRepositroy = new StorageRecordRepository(unitwork);
                List<StorageRecord> records =
                    recordRepositroy.GetMoldApplyRecordsByMoldNR(moldNR, pageIndex, pageSize, applicantId, startDate, endDate);
                return records;
            }
        }

        /// <summary>
        /// get the list of report by its mold nr
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <returns>the list of report</returns>
        public List<MoldReleaseInfo> GetMoldReleaseInfoByMoldNR(string moldNR, string operatorId, DateTime? startDate, DateTime? endDate)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IReportRepository reportRep = new ReportRepository(unitwork);
                List<ReportView> reports = reportRep.GetReportViewByMoldNR(moldNR, operatorId, startDate, endDate);
                IAttachmentRepository attachRep = new AttachmentRepository(unitwork);
                List<MoldReleaseInfo> moldReleaseInfos = new List<MoldReleaseInfo>();
                foreach (ReportView r in reports)
                {
                    MoldReleaseInfo moldReleaseInfo = new MoldReleaseInfo()
                    {
                        TesterName = r.Name,
                        TesterNR = r.OperatorID,
                        Date = r.Date,
                        TargetNR = r.MoldID,
                        ReportType = r.ReportType,
                        ReportTypeCN = r.ReportTypeCN,
                        Attach = attachRep.GetByMasterNR(r.ReportId.ToString())
                    };
                    moldReleaseInfos.Add(moldReleaseInfo);
                }
                return moldReleaseInfos;
            }
        }

        /// <summary>
        /// get the reports by mold nr, page, pageSize
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <param name="pageIndex">the index of page</param>
        /// <param name="pageSize">the size of page</param>
        /// <returns></returns>
        public List<MoldReleaseInfo> GetMoldReleaseInfoByMoldNRInPage(string moldNR, int pageIndex, int pageSize, string operatorId, DateTime? startDate, DateTime? endDate)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IReportRepository reportRep = new ReportRepository(unitwork);
                List<ReportView> reports = reportRep.GetReportViewByMoldNR(moldNR, pageIndex, pageSize, operatorId, startDate, endDate);
                IAttachmentRepository attachRep = new AttachmentRepository(unitwork);
                List<MoldReleaseInfo> moldReleaseInfos = new List<MoldReleaseInfo>();
                foreach (ReportView r in reports)
                {
                    MoldReleaseInfo moldReleaseInfo = new MoldReleaseInfo()
                    {
                        TesterName = r.Name,
                        TesterNR = r.OperatorID,
                        Date = r.Date,
                        TargetNR = r.MoldID,
                        ReportType = r.ReportType,
                        ReportTypeCN = r.ReportTypeCN,
                        Attach = attachRep.GetByMasterNR(r.ReportId.ToString())
                    };
                    moldReleaseInfos.Add(moldReleaseInfo);
                }
                return moldReleaseInfos;
            }
        }

        /// <summary>
        /// get the report attachments by the report id
        /// </summary>
        /// <param name="reportId">the id of report</param>
        /// <returns>the list of attachment</returns>
        public List<Attachment> GetAttachmentById(Guid reportId)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IAttachmentRepository reportAttachRep = new AttachmentRepository(unitwork);
                List<Attachment> attaches = reportAttachRep.GetByMasterNR(reportId.ToString());
                return attaches;
            }
        }




        /// <summary>
        /// get file from server by file name
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public byte[] GetFileByName(string fileName)
        {
            string p = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Settings.Default.FileAttachmentPath);
            string path = Path.Combine(p, fileName);
            byte[] data = null;

            if (File.Exists(path))
            {
                using (Stream stream = File.Open(path, FileMode.Open, FileAccess.Read))
                {
                    data = new byte[stream.Length];
                    stream.Read(data, 0, data.Length);
                }
            }
            return data;
        }


        public List<MoldWarnInfo> GetMoldWarnInfo(MoldWarnType type)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IMoldRepository moldRepostitory = new MoldRepository(unitwork);
                List<MoldView> molds = moldRepostitory.GetByWarnType(type);
                List<MoldWarnInfo> moldWarnInfos = new List<MoldWarnInfo>();

                foreach (MoldView m in molds)
                {
                    MoldWarnInfo moldWarnInfo = new MoldWarnInfo()
                    {
                        MoldNR = m.MoldNR,
                        //Name = m.Name,
                        Type = m.TypeName,
                        //State = m.State,
                        //StateCN = m.StateCN,
                        //ProjectId = m.ProjectID,
                        ProjectName = m.ProjectName,
                        MaxLendHour = (double)m.MaxLendHour,
                        LendTime = (DateTime)m.LastRecordDate,
                        CurrentPosition = m.StorageRecordNR.HasValue ? moldRepostitory.GetMoldCurrPosiByRecordNR((Guid)m.StorageRecordNR) : string.Empty
                    };
                    moldWarnInfos.Add(moldWarnInfo);
                }

                return moldWarnInfos;
            }
        }


        /// <summary>
        /// get mold position by its mold nr
        /// </summary>
        /// <param name="moldNR"></param>
        /// <returns></returns>
        public Position GetMoldPositionByNr(string moldNR)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IPositionRepository posiRep = new PositionRepository(unitwork);
                return posiRep.GetByFacilictyNR(moldNR);
            }
        }

        /// <summary>
        /// get mold nr by position nr
        /// </summary>
        /// <param name="posiNr"></param>
        /// <returns></returns>
        public string GetMoldNrByPosiNr(string posiNr)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IMoldRepository moldRep = new MoldRepository(unitwork);
                return moldRep.GetMoldNrByPosiNr(posiNr);
            }
        }


        /// <summary>
        /// get mold current position
        /// </summary>
        /// <param name="moldNR"></param>
        /// <returns></returns>
        public string GetMoldCurrentPosition(string moldNR)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IMoldRepository moldRepostitory = new MoldRepository(unitwork);
                MoldView moldview = moldRepostitory.GetMoldViewByMoldNR(moldNR);
                return moldview.StorageRecordNR == null ? string.Empty : moldRepostitory.GetMoldCurrPosiByRecordNR((Guid)moldview.StorageRecordNR);
            }
        }
    }
}
