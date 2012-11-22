using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ToolingWCF.DataModel;
using ClassLibrary.ENUM;
using ClassLibrary.Data;

namespace ToolingWCF
{
    [ServiceContract]
    public interface IStorageManageService
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
        [OperationContract]
        Message ApplyMold(MoldUseType moldUseType, string moldNR, string applicantNR, string operatorNR, string workstationNR);


        /// <summary>
        /// return mold
        /// </summary>
        /// <param name="moldNR"></param>
        /// <param name="applicantNR"></param>
        /// <param name="operatorNR"></param>
        /// <param name="workstationNR"></param>
        /// <param name="moldState"></param>
        /// <returns></returns>
        [OperationContract]
        Message ReturnMold(string moldNR, string applicantNR, string operatorNR, string remark, MoldReturnStateType moldState);

          /// <summary>
        /// move the mold from MoldPool in position
        /// </summary>
        /// <param name="moldNR"></param>
        /// <param name="operatorNR"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        [OperationContract]
        Message ReturnMoldInPosition(string moldNR, string operatorNR, string remark);
        /// <summary>
        /// store the mold in the mold position
        /// </summary>
        /// <param name="storageRecord">the storage record</param>
        /// <returns></returns>
        [OperationContract]
        Message MoldInStore(string moldNR, string operatorNR, string warehouesNR, string positionNR);


        /// <summary>
        /// move the mold
        /// </summary>
        /// <param name="moldNR"></param>
        /// <param name="sourcePosiNr"></param>
        /// <param name="desiPosiNr"></param>
        /// <returns></returns>
        [OperationContract]
        Message MoldMoveStore(string moldNR, string warehouseNR, string sourcePosiNr, string desiPosiNr);

     
        /// <summary>
        /// generate test report for mold
        /// </summary>
        /// <param name="moldNR"></param>
        /// <param name="operatorNR"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        [OperationContract]
        Message MoldTest(string moldNR, string operatorNR, FileUP[] files, int currentCutTimes,bool moldNormal);

        /// <summary>
        /// generate maintain report for mold
        /// </summary>
        /// <param name="moldNR"></param>
        /// <param name="operatorNR"></param>
        /// <returns></returns>
        [OperationContract]
        Message MoldMaintain(string moldNR, string operatorNR,FileUP[] files,bool moldNormal);

        /// <summary>
        /// upload files
        /// </summary>
        /// <param name="files"></param>
        /// <param name="masterNR"></param>
        /// <returns></returns>
        [OperationContract]
        Message FileUpLoad(FileUP[] files, string masterNR);

        //[OperationContract]
        //Message MoldAddAttachs(string moldNR, FileUP[] files);

        [OperationContract]
        string GetPartPoolPosiNr();

        /// <summary>
        /// move mold work station
        /// </summary>
        /// <param name="moldNR"></param>
        /// <param name="operatorNR"></param>
        /// <param name="targetWStationNR"></param>
        /// <returns></returns>
        [OperationContract]
        Message MoldMoveWorkStation(string moldNR, string operatorNR, string targetWStationNR);
    }
}
