using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToolingWCF.DataModel;
using ClassLibrary.Data;
using ClassLibrary.ENUM;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Repository.Implement;
using ClassLibrary.Utility;
using ToolingWCF.Properties;
using System.ServiceModel;
using System.IO;
using ToolingWCF.Utilities;
using System.Transactions;

namespace ToolingWCF
{
    public class StorageManageService : IStorageManageService
    {
        /// <summary>
        /// apply mold
        /// </summary>
        /// <param name="moldUseType"></param>
        /// <param name="moldNR"></param>
        /// <param name="applicantNR"></param>
        /// <param name="operatorNR"></param>
        /// <param name="workstationNR"></param>
        /// <returns></returns>
        public Message ApplyMold(MoldUseType moldUseType, string moldNR, string applicantNR, string operatorNR, string workstationNR)
        {
            try
            {
                using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
                {
                    IWorkstationRepository workstationRep = new WorkstationRepository(unitwork);
                    IMoldRepository moldRep = new MoldRepository(unitwork);
                    Mold mold = moldRep.GetById(moldNR);
                    // check mold is available for apply
                    if (mold.State == MoldStateType.NotReturned)
                        return new Message() { MsgType = MsgType.Warn, Content = "模具处于不可借状态！" };

                    // check workstaition reach the max mold apply number
                    if (workstationRep.OverAppliedMold(workstationNR) == false && Settings.Default.AllowOverApply == false)
                        return new Message() { MsgType = MsgType.Warn, Content = "申领已到达上限！" };


              
                    IPositionRepository positionRep = new PositionRepository(unitwork);
                    Position position = positionRep.GetByFacilictyNR(moldNR);

                    //set value of storage record
                    IStorageRecordRepository recordRep = new StorageRecordRepository(unitwork);
                    StorageRecord storageRecord = new StorageRecord();
                    storageRecord.StorageRecordNR = GuidUtil.GenerateGUID();
                    storageRecord.PositionId = position.PositionID;
                    storageRecord.Source = position.PositionNR;
                    storageRecord.Destination = workstationNR;
                    storageRecord.OperatorId = operatorNR;
                    storageRecord.ApplicantId = applicantNR;
                    storageRecord.Quantity = 1;
                    storageRecord.Date = DateTime.Now;
                    storageRecord.TargetNR = moldNR;
                    storageRecord.RecordType = (StorageRecordType)moldUseType;

                    // add new storage record
                    recordRep.Add(storageRecord);

                    // update mold last apply storage record nr
                    IMoldLastRecordRepository lastRecordRep = new MoldLastRecordRepository(unitwork);
                    MoldLastRecord lastRecord = lastRecordRep.GetByMoldNR(moldNR);
                    lastRecord.StorageRecordNR = storageRecord.StorageRecordNR;


                    // update mold state
                    mold = moldRep.GetById(storageRecord.TargetNR);
                    mold.State = MoldStateType.NotReturned;

                    // update the workstation current mold count
                    Workstation workstation = workstationRep.GetById(storageRecord.Destination);
                    workstation.CurrentMoldCount++;

                    unitwork.Submit();
                    return new Message() { MsgType = MsgType.OK, Content = "申领成功" };
                }
            }
            catch (Exception ex)
            {
                LogUtil.log.Error(ex.ToString());
                return new Message() { MsgType = MsgType.Error, Content = "请核实所填数据的准确性" };
            }
        }

        /// <summary>
        /// return the mold in the MoldPool
        /// </summary>
        /// <param name="moldNR"></param>
        /// <param name="applicantNR"></param>
        /// <param name="operatorNR"></param>
        /// <param name="workstationNR"></param>
        /// <param name="moldState"></param>
        /// <returns></returns>
        public Message ReturnMold(string moldNR, string applicantNR, string operatorNR, string remark, MoldReturnStateType moldState)
        {
            try
            {
                using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
                {
                    IMoldRepository moldRep = new MoldRepository(unitwork);
                    Mold mold = moldRep.GetById(moldNR);

                    if (mold.State != MoldStateType.NotReturned)
                        return new Message() {MsgType= MsgType.Warn, Content = "模具已归还！" };

                    StorageRecord storageRecord = new StorageRecord();

                    //set value of storage record 

                    IPositionRepository positionRep = new PositionRepository(unitwork);
                    //Position position = positionRep.GetByFacilictyNR(moldNR);
                    Position position = positionRep.GetPartPoolPosition(Settings.Default.MoldPoolPosiNr);
                    storageRecord.PositionId = position.PositionID;
                    storageRecord.StorageRecordNR = GuidUtil.GenerateGUID();
                    storageRecord.Source = moldRep.GetMoldCurrPosiByMoldNR(moldNR);
                    storageRecord.Destination = position.PositionNR;
                    storageRecord.OperatorId = operatorNR;
                    storageRecord.ApplicantId = applicantNR;
                    storageRecord.Date = DateTime.Now;
                    storageRecord.Quantity = 1;
                    storageRecord.TargetNR = moldNR;
                    storageRecord.Remark = remark;
                    storageRecord.RecordType = StorageRecordType.Return;

                    // add new storage record
                    IStorageRecordRepository recordRep = new StorageRecordRepository(unitwork);
                    recordRep.Add(storageRecord);

                    // update mold last apply storage record nr
                    IMoldLastRecordRepository lastRecordRep = new MoldLastRecordRepository(unitwork);
                    MoldLastRecord lastRecord = lastRecordRep.GetByMoldNR(moldNR);
                    lastRecord.StorageRecordNR = storageRecord.StorageRecordNR;

                    // update mold state
                    mold = moldRep.GetById(storageRecord.TargetNR);
                    mold.State =(MoldStateType)moldState;

                    // update workstation mold count
                    IWorkstationRepository workstationRep = new WorkstationRepository(unitwork);
                    Workstation workstation = workstationRep.GetById(storageRecord.Source);
                    workstation.CurrentMoldCount--;


                    unitwork.Submit();
                    return new Message() { MsgType = MsgType.OK, Content = "归还成功" };
                }
            }
            catch (Exception ex)
            {
                  LogUtil.log.Error(ex.ToString());
                return new Message() { MsgType = MsgType.Error, Content = ex.Message };
            }
        }

        /// <summary>
        /// move the mold from MoldPool in position
        /// </summary>
        /// <param name="moldNR"></param>
        /// <param name="operatorNR"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public Message ReturnMoldInPosition(string moldNR, string operatorNR,string remark)
        {
            try
            {
                using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
                {
                    IMoldRepository moldRep = new MoldRepository(unitwork);
                    Mold mold = moldRep.GetById(moldNR);

                    if (mold.State == MoldStateType.NotReturned)
                        return new Message() { MsgType = MsgType.Warn, Content = "模具还未归还，请归还后再入库！" };

                    string currentPosi = moldRep.GetMoldCurrPosiByMoldNR(moldNR);

                    if(!currentPosi.Equals(GetPartPoolPosiNr()))
                        return new Message() { MsgType = MsgType.Warn, Content = "模具已经入库！" };
                    StorageRecord storageRecord = new StorageRecord();

                    //set value of storage record 

                    IPositionRepository positionRep = new PositionRepository(unitwork);
                    Position position = positionRep.GetByFacilictyNR(moldNR);
                   // Position position = positionRep.GetPartPoolPosition(Settings.Default.MoldPoolPosiNr);
                    storageRecord.PositionId = position.PositionID;
                    storageRecord.StorageRecordNR = GuidUtil.GenerateGUID();
                    storageRecord.Source = currentPosi;
                    storageRecord.Destination = position.PositionNR;
                    storageRecord.OperatorId =operatorNR==null?string.Empty: operatorNR.ToString();
                    storageRecord.Date = DateTime.Now;
                    storageRecord.Quantity = 1;
                    storageRecord.TargetNR = moldNR;
                    storageRecord.Remark = remark==null?string.Empty:remark.ToString();
                    storageRecord.RecordType = StorageRecordType.MoveStore;

                    // add new storage record
                    IStorageRecordRepository recordRep = new StorageRecordRepository(unitwork);
                    recordRep.Add(storageRecord);

                    // update mold last apply storage record nr
                    IMoldLastRecordRepository lastRecordRep = new MoldLastRecordRepository(unitwork);
                    MoldLastRecord lastRecord = lastRecordRep.GetByMoldNR(moldNR);
                    lastRecord.StorageRecordNR = storageRecord.StorageRecordNR;

                    // update mold state
                    //mold = moldRep.GetById(storageRecord.TargetNR);
                    //mold.State = (MoldStateType)moldState;

                    // update workstation mold count
                    //IWorkstationRepository workstationRep = new WorkstationRepository(unitwork);
                    //Workstation workstation = workstationRep.GetById(storageRecord.Source);
                    //workstation.CurrentMoldCount--;


                    unitwork.Submit();
                    return new Message() { MsgType = MsgType.OK, Content = "入库成功" };
                }
            }
            catch (Exception ex)
            {
                LogUtil.log.Error(ex.ToString());
                return new Message() { MsgType = MsgType.Error, Content = ex.Message };
            }
        }

      /// <summary>
      /// move the mold
      /// </summary>
      /// <param name="moldNR"></param>
      /// <param name="sourcePosiNr"></param>
      /// <param name="desiPosiNr"></param>
      /// <returns></returns>
        public Message MoldMoveStore(string moldNR,string warehouseNR,string sourcePosiNr,string desiPosiNr)
        {
            try
            {
                using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
                {
                    IPositionRepository posiRep = new PositionRepository(unitwork);
                    if (!posiRep.PositionExsit(desiPosiNr))
                        return new Message() { MsgType = MsgType.Warn, Content = "目标库位不存在，请核实" };

                    IMoldRepository moldRep = new MoldRepository(unitwork);
                    if (moldRep.GetMoldNrByPosiNr(desiPosiNr).Length == 0)
                    {
                        // there is no mold in desitination position 
                        // add new uniqstorage
                        Position position = posiRep.GetByWarehouseNRAndPositionNR(warehouseNR, desiPosiNr);
                        // IUniqStorageRepository uniqStroageRep = new UniqStorageRepository(unitwork);

                        // uniqStroageRep.DeleteByMoldNr(moldNR);

                        // add new uniqstorage
                        IUniqStorageRepository uniqStroageRep = new UniqStorageRepository(unitwork);
                        uniqStroageRep.DeleteByMoldNr(moldNR);

                        UniqStorage uniqStorage = new UniqStorage()
                        {
                            UniqStorageId = GuidUtil.GenerateGUID(),
                            UniqNR = moldNR,
                            PositionId = position.PositionID,
                            Quantity = 1
                        };
                        uniqStroageRep.Add(uniqStorage);

                        //set value of storage record 
                        StorageRecord storageRecord = new StorageRecord();
                        storageRecord.PositionId = position.PositionID;
                        storageRecord.StorageRecordNR = GuidUtil.GenerateGUID();
                        storageRecord.Source = sourcePosiNr;
                        storageRecord.Destination = desiPosiNr;
                        storageRecord.Date = DateTime.Now;
                        storageRecord.Quantity = 1;
                        storageRecord.TargetNR = moldNR;
                        storageRecord.RecordType = StorageRecordType.MoveStore;

                        // add new storage record
                        IStorageRecordRepository recordRep = new StorageRecordRepository(unitwork);
                        recordRep.Add(storageRecord);

                        IMoldLastRecordRepository moldLastRecord = new MoldLastRecordRepository(unitwork);
                        moldLastRecord.GetByMoldNR(moldNR).StorageRecordNR = storageRecord.StorageRecordNR;

                    }
                    else
                    {
                        Position sourcePosi = posiRep.GetByWarehouseNRAndPositionNR(warehouseNR, sourcePosiNr);
                        Position desiPosi = posiRep.GetByWarehouseNRAndPositionNR(warehouseNR, desiPosiNr);
                        UniqStorage sourceU = sourcePosi.UniqStorage.First();
                        UniqStorage desiU = desiPosi.UniqStorage.First();

                        string desiMoldNr = desiU.UniqNR;
                        sourceU.UniqNR = desiMoldNr;
                        desiU.UniqNR = moldNR;
                        // add new storage record
                        IStorageRecordRepository recordRep = new StorageRecordRepository(unitwork);

                        StorageRecord sourcestorageRecord = new StorageRecord();
                        sourcestorageRecord.PositionId = sourcePosi.PositionID;
                        sourcestorageRecord.StorageRecordNR = GuidUtil.GenerateGUID();
                        sourcestorageRecord.Source = sourcePosiNr;
                        sourcestorageRecord.Destination = desiPosiNr;
                        sourcestorageRecord.Date = DateTime.Now;
                        sourcestorageRecord.Quantity = 1;
                        sourcestorageRecord.TargetNR = moldNR;
                        sourcestorageRecord.RecordType = StorageRecordType.MoveStore;


                        recordRep.Add(sourcestorageRecord);

                        StorageRecord desistorageRecord = new StorageRecord();
                        desistorageRecord.PositionId = desiPosi.PositionID;
                        desistorageRecord.StorageRecordNR = GuidUtil.GenerateGUID();
                        desistorageRecord.Source = desiPosiNr;
                        desistorageRecord.Destination = sourcePosiNr;
                        desistorageRecord.Date = DateTime.Now;
                        desistorageRecord.Quantity = 1;
                        desistorageRecord.TargetNR = desiMoldNr;
                        desistorageRecord.RecordType = StorageRecordType.MoveStore;


                        recordRep.Add(desistorageRecord);
                        IMoldLastRecordRepository moldLastRecord = new MoldLastRecordRepository(unitwork);
                        moldLastRecord.GetByMoldNR(moldNR).StorageRecordNR = sourcestorageRecord.StorageRecordNR;
                        moldLastRecord.GetByMoldNR(desiMoldNr).StorageRecordNR = desistorageRecord.StorageRecordNR;

                    }
                    
                    unitwork.Submit();
                    return new Message() { MsgType = MsgType.OK, Content = "移库成功" };
                }
            }
            catch (Exception ex)
            {
                
                LogUtil.log.Error(ex.ToString());
                return new Message() { MsgType = MsgType.Error, Content = ex.Message };
            }
        }
        /// <summary>
        /// store the mold in the mold position
        /// </summary>
        /// <param name="storageRecord">the storage record</param>
        /// <returns></returns>
        public Message MoldInStore(string moldNR, string operatorNR, string warehouseNR, string positionNR)
        {
            try
            {
                using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
                {
                    IPositionRepository positionRep = new PositionRepository(unitwork);
                    IMoldLastRecordRepository lastRecordRep = new MoldLastRecordRepository(unitwork);

                    // check if mold is reinstore again
                    if (lastRecordRep.MoldInStored(moldNR) == true)
                        return new Message() { MsgType = MsgType.Warn, Content = "此模具已经入库" };
                    //check if position is available and allow over instore
                    if (positionRep.CheckPositionAvailable(warehouseNR, positionNR, 1) == false && Settings.Default.AllowOverInStore == false)
                        return new Message() { MsgType = MsgType.Warn, Content = "库位容量已打上限" };

                    Position position = positionRep.GetByWarehouseNRAndPositionNR(warehouseNR, positionNR);

                    // add new uniqstorage
                    IUniqStorageRepository uniqStroageRep = new UniqStorageRepository(unitwork);
                    UniqStorage uniqStorage = new UniqStorage()
                    {
                        UniqStorageId = GuidUtil.GenerateGUID(),
                        UniqNR = moldNR,
                        PositionId = position.PositionID,
                        Quantity = 1
                    };
                    uniqStroageRep.Add(uniqStorage);

                    //set value of storage record 
                    StorageRecord storageRecord = new StorageRecord();
                    storageRecord.PositionId = position.PositionID;
                    storageRecord.StorageRecordNR = GuidUtil.GenerateGUID();
                    storageRecord.Source = moldNR;
                    storageRecord.Destination = position.PositionNR;
                    storageRecord.OperatorId = operatorNR;
                    storageRecord.Date = DateTime.Now;
                    storageRecord.Quantity = 1;
                    storageRecord.TargetNR = moldNR;
                    storageRecord.RecordType = StorageRecordType.InStore;

                    // add new storage record
                    IStorageRecordRepository recordRep = new StorageRecordRepository(unitwork);
                    recordRep.Add(storageRecord);

                    // add mold last apply
                    // add mold last apply storage record nr
                  
                    MoldLastRecord lastRecord = new MoldLastRecord()
                    {
                        MoldNR = moldNR,
                         StorageRecordNR = storageRecord.StorageRecordNR
                    };
                   // lastRecord.StroageRecordNR = storageRecord.StorageRecordNR;
                    lastRecordRep.Add(lastRecord);

                    unitwork.Submit();
                    return new Message() { MsgType = MsgType.OK, Content = "入库成功！" };
                }
            }

            catch (Exception ex)
            {
                  LogUtil.log.Error(ex.ToString());
                  return new Message() { MsgType = MsgType.Error, Content = "请核实所填数据的准确性" };
            }
        }


        /// <summary>
        /// part out store
        /// </summary>
        /// <param name="partNR"></param>
        /// <param name="operatorNR"></param>
        /// <param name="quantity"></param>
        /// <param name="warehouseNR"></param>
        /// <param name="positionNR"></param>
        /// <returns></returns>


        /// <summary>
        /// generate test report for mold
        /// </summary>
        /// <param name="moldNR"></param>
        /// <param name="operatorNR"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public Message  MoldTest(string moldNR, string operatorNR, FileUP[] files,int currentCutTimes,bool moldNormal)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
                    {
                        IReportRepository reportRep = new ReportRepository(unitwork);

                        Report report = new Report();
                        report.ReportId = GuidUtil.GenerateGUID();
                        report.MoldID = moldNR;
                        report.ReportType = ReportType.TestReport;
                        report.OperatorID = operatorNR;
                        report.Date = DateTime.Now;

                        //upload files 
                        FileUpLoad(files, report.ReportId.ToString());

                        reportRep.Add(report);

                        // update the last released date
                        IMoldRepository moldRep = new MoldRepository(unitwork);
                        Mold mold = moldRep.GetById(moldNR);
                        mold.LastReleasedDate = report.Date;
                        mold.Cuttedtimes += mold.CurrentCuttimes;
                        mold.CurrentCuttimes = 0;
                        if (moldNormal)
                            mold.State = MoldStateType.Normal;
                        unitwork.Submit();

                        ts.Complete();

                        return new Message() { MsgType = MsgType.OK, Content = "实验报告上传成功！" };
                    }
                }
            }
            catch (Exception ex)
            {
                  LogUtil.log.Error(ex.ToString());
                return new Message() { MsgType = MsgType.Error, Content = ex.Message };
            }
        }

        /// <summary>
        /// generate maintain report for mold
        /// </summary>
        /// <param name="moldNR"></param>
        /// <param name="operatorNR"></param>
        /// <returns></returns>
        public Message MoldMaintain(string moldNR, string operatorNR, FileUP[] files, bool moldNormal)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
                    {
                        IReportRepository reportRep = new ReportRepository(unitwork);

                        Report report = new Report();
                        report.ReportId = GuidUtil.GenerateGUID();
                        report.MoldID = moldNR;
                        report.ReportType = ReportType.MaintainReport;
                        report.OperatorID = operatorNR;
                        report.Date = DateTime.Now;

                        //upload files 
                        FileUpLoad(files, report.ReportId.ToString());

                        reportRep.Add(report);

                        // update the last released date
                        IMoldRepository moldRep = new MoldRepository(unitwork);
                        Mold mold = moldRep.GetById(moldNR);
                        mold.LastMainedDate = report.Date;
                        if (moldNormal)
                            mold.State = MoldStateType.Normal;
                        unitwork.Submit();

                        ts.Complete();

                        return new Message() { MsgType = MsgType.OK, Content = "维护报告上传成功！" };
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.log.Error(ex.ToString());
                return new Message() { MsgType = MsgType.Error, Content = ex.Message };
            }
        }


        /// <summary>
        /// upload files
        /// </summary>
        /// <param name="files"></param>
        /// <param name="masterNR"></param>
        /// <returns></returns>
        public Message FileUpLoad(FileUP[] files,string masterNR)
        {
            try
            {
                if (files != null && files.Length > 0)
                {
                    using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
                    {
                        string p = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Settings.Default.FileAttachmentPath);
                        if (!Directory.Exists(p))
                        {
                            Directory.CreateDirectory(p);
                        }
                        List<Attachment> reportAttaches = new List<Attachment>();
                        string type = string.Empty;
                        const int bufferLength = 4096;

                        foreach (FileUP file in files)
                        {
                            // type = file.Name.Substring(file.Name.LastIndexOf('.'), file.Name.Length - file.Name.LastIndexOf('.'));
                            string servername = GuidUtil.GenerateGUID().ToString() + file.FileType;

                            string filePath = Path.Combine(p, servername);

                            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                            {
                                using (Stream stream = new MemoryStream(file.Data))
                                {
                                    byte[] buffer = new byte[bufferLength];
                                    int readcount = 0;
                                    do
                                    {
                                        readcount = stream.Read(buffer, 0, bufferLength);
                                        if (readcount > 0)
                                        {
                                            fs.Write(buffer, 0, readcount);
                                        }
                                    } while (readcount > 0);
                                }
                            }

                            Attachment attachment = new Attachment()
                            {
                                MasterNR = masterNR,
                                Name = file.Name,
                                Path = servername,
                                Date = DateTime.Now
                            };
                            reportAttaches.Add(attachment);
                        }

                        IAttachmentRepository attachRep = new AttachmentRepository(unitwork);
                        attachRep.Add(reportAttaches);
                        unitwork.Submit();                          

                        return new Message() { MsgType = MsgType.OK, Content = "文件上传成功！" };
                    }
                }
                return new Message() { MsgType = MsgType.Warn, Content = "文件为空！" };
            }
            catch(Exception ex)
            {
                LogUtil.log.Error(ex.ToString());
                return new Message() { MsgType = MsgType.Error, Content = "请核实所填数据的准确性" };
            }
        }

        public string GetPartPoolPosiNr()
        {
            return Settings.Default.MoldPoolPosiNr;
        }

        /// <summary>
        /// move mold work station
        /// </summary>
        /// <param name="moldNR"></param>
        /// <param name="operatorNR"></param>
        /// <param name="targetWStationNR"></param>
        /// <returns></returns> 
        public Message MoldMoveWorkStation(string moldNR, string operatorNR, string targetWStationNR)
        {
            try
            {
                using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
                {
                    IWorkstationRepository workstationRep = new WorkstationRepository(unitwork);
                    IMoldRepository moldRep = new MoldRepository(unitwork);
                    Mold mold = moldRep.GetById(moldNR);
                    MoldView moldview = moldRep.GetMoldViewByMoldNR(moldNR);


                    // check mold is available for apply
                    if (mold.State != MoldStateType.Normal)
                        return new Message() { MsgType = MsgType.Warn, Content = "模具未被借用，请先借用！" };

                    // check workstaition reach the max mold apply number
                    if (workstationRep.OverAppliedMold(targetWStationNR) == false && Settings.Default.AllowOverApply == false)
                        return new Message() { MsgType = MsgType.Warn, Content = "目标工作台已经到达模具使用上限！" };



                    IPositionRepository positionRep = new PositionRepository(unitwork);
                    Position position = positionRep.GetByFacilictyNR(moldNR);

                    //set value of storage record
                    IStorageRecordRepository recordRep = new StorageRecordRepository(unitwork);
                    StorageRecord storageRecord = new StorageRecord();
                    storageRecord.StorageRecordNR = GuidUtil.GenerateGUID();
                    storageRecord.PositionId = position.PositionID;
                    storageRecord.Source = moldview.StorageRecordNR == null ? string.Empty : moldRep.GetMoldCurrPosiByRecordNR((Guid)moldview.StorageRecordNR);
                    storageRecord.Destination = targetWStationNR;
                    storageRecord.OperatorId = operatorNR;
                    storageRecord.Quantity = 1;
                    storageRecord.Date = DateTime.Now;
                    storageRecord.TargetNR = moldNR;
                    storageRecord.RecordType = StorageRecordType.MoveWStation;

                    // add new storage record
                    recordRep.Add(storageRecord);

                    // update mold last apply storage record nr
                    IMoldLastRecordRepository lastRecordRep = new MoldLastRecordRepository(unitwork);
                    MoldLastRecord lastRecord = lastRecordRep.GetByMoldNR(moldNR);
                    lastRecord.StorageRecordNR = storageRecord.StorageRecordNR;

                    // update the workstation current mold count
                    Workstation tworkstation = workstationRep.GetById(storageRecord.Destination);
                    tworkstation.CurrentMoldCount++;
                    Workstation sworkstation = workstationRep.GetById(storageRecord.Source);
                    sworkstation.CurrentMoldCount--;

                    unitwork.Submit();
                    return new Message() { MsgType = MsgType.OK, Content = "移动工作台成功！" };
                }
            }
            catch (Exception ex)
            {
                LogUtil.log.Error(ex.ToString());
                return new Message() { MsgType = MsgType.Error, Content = "请核实所填数据的准确性" };
            }
        }
    }
}
