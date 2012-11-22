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
  public  class SmartDeviceApi:ISmartDeviceApi 

    {
        public MoldDynamicInfo GetMoldDynamicInfoByMoldNR(string moldNR)
        {
           MoldPartInfoService moldsvc = new MoldPartInfoService() ;
           return moldsvc.GetMoldDynamicInfoByMoldNR(moldNR);
            }


        public MoldBaseInfo GetMoldBaseInfoByNR(string moldNR)
        {
            MoldPartInfoService moldsvc = new MoldPartInfoService();
            return moldsvc.GetMoldBaseInfoByNR(moldNR);
            }

        public Message MoldMoveWorkStation(string moldNR, string operatorNR, string targetWStationNR)
        {
            StorageManageService storageSvc = new StorageManageService();
            return storageSvc.MoldMoveWorkStation(moldNR,operatorNR,targetWStationNR );
        
        }

        }

        }

    

