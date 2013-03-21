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
using ToolingWCF.Helper;

namespace ToolingWCF
{
    public class MoldPartInfoService : IMoldPartInfoService
    {
        /// <summary>
        /// 根据搜索条件获得模具基本信息
        /// </summary>
        /// <param name="conditions">搜索条件</param>
        /// <returns>模具基本信息列表</returns>
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
        /// 根据模具号获得模具基本信息
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <returns>模具基本信息</returns>
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
        /// 根据模具号获得模具动态信息
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <returns>模具基本信息</returns>
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
                    StateCN = EnumUtil.GetEnumDescriptionByEnumValue(moldview.State),
                    ProjectId = moldview.ProjectID,
                    ProjectName = moldview.ProjectName
                };
                return moldDynamicInfo;
            }
        }

        /// <summary>
        /// 根据模具号、操作员号、起止日期获得模具基本信息
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <returns>模具基本信息列表</returns>
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
        /// 根据模具号、操作员号、起止日期、页码信息获得模具基本信息
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页码数量</param>
        /// <returns>模具基本信息列表</returns>
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
        /// 根据模具号、操作员号、起止日期、页码信息获得模具放行信息
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <returns>模具放行信息列表</returns>
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
        /// 根据模具号、操作员号、起止日期、页码信息获得模具放行信息
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页码数量</param>
        /// <returns>模具放行信息列表</returns>
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
        /// 根据报告号获取附件
        /// </summary>
        /// <param name="reportId">报告号</param>
        /// <returns>附件列表</returns>
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
        /// 根据文件名获得文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>文件</returns>
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

        /// <summary>
        /// 根据模具警报类型获得模具警报信息
        /// </summary>
        /// <param name="type">模具警报类型</param>
        /// <returns>模具警报信息列表</returns>
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
                        Type = m.TypeName,
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
        /// 根据模具号获得库位
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <returns>库位</returns>
        public Position GetMoldPositionByNr(string moldNR)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IPositionRepository posiRep = new PositionRepository(unitwork);
                return posiRep.GetByFacilictyNR(moldNR);
            }
        }

        /// <summary>
        /// 根据模具号获得库位号
        /// </summary>
        /// <param name="posiNr">库位号</param>
        /// <returns>模具号</returns>
        public string GetMoldNrByPosiNr(string posiNr)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IMoldRepository moldRep = new MoldRepository(unitwork);
                return moldRep.GetMoldNrByPosiNr(posiNr);
            }
        }


        /// <summary>
        /// 根据模具号获得库位号
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <returns>库位号</returns>
        public string GetMoldCurrentPosition(string moldNR)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IMoldRepository moldRepostitory = new MoldRepository(unitwork);
                MoldView moldview = moldRepostitory.GetMoldViewByMoldNR(moldNR);
                return moldview.StorageRecordNR == null ? string.Empty : moldRepostitory.GetMoldCurrPosiByRecordNR((Guid)moldview.StorageRecordNR);
            }
        }


        public List<ReportView> GetReportViewByDate(ReportType type,DateTime? startDate, DateTime? endDate)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IReportRepository reportRep = new ReportRepository(unitwork);
                return reportRep.GetReportViewByDate(type,startDate, endDate);
            }
        }




        public List<StorageRecord> GetStoreRecordByDate(int type, DateTime? startDate, DateTime? endDate)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IStorageRecordRepository reportRep = new StorageRecordRepository(unitwork);
                return reportRep.GetByTypesDate(MoldRecordTypeHelper.GetApplyTypes(type), startDate, endDate);
            }
        }


        public bool DelAttachmentById(int attachId, string filePath)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                try
                {
                    IAttachmentRepository attachRep = new AttachmentRepository(unitwork);
                    attachRep.DeleteById(attachId);
                    string p = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Settings.Default.FileAttachmentPath);
                    string path = Path.Combine(p, filePath);
                    File.Delete(path);
                    unitwork.Submit();
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                    return false;
                }
                return true;
            }
        }
    }
}
