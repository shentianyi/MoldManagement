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
    interface ISmartDeviceApi
    {
        [OperationContract]
        MoldBaseInfo GetMoldBaseInfoByNR(string moldNR);

        [OperationContract]
        MoldDynamicInfo GetMoldDynamicInfoByMoldNR(string moldNR);

        [OperationContract]
        Message MoldMoveWorkStation(string moldNR, string operatorNR, string targetWStationNR);
    }
}
