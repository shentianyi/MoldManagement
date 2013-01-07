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
    /// <summary>
    /// 移动终端API
    /// </summary>
    public class SmartDeviceApi : ISmartDeviceApi
    {
        /// <summary>
        /// 根据模具号获得模具动态信息
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <returns>模具动态信息</returns>
        public MoldDynamicInfo GetMoldDynamicInfoByMoldNR(string moldNR)
        {
            MoldPartInfoService moldsvc = new MoldPartInfoService();
            return moldsvc.GetMoldDynamicInfoByMoldNR(moldNR);
        }

        /// <summary>
        /// 根据模具号获得模具基本信息
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <returns>模具基本信息</returns>
        public MoldBaseInfo GetMoldBaseInfoByNR(string moldNR)
        {
            MoldPartInfoService moldsvc = new MoldPartInfoService();
            return moldsvc.GetMoldBaseInfoByNR(moldNR);
        }

        /// <summary>
        /// 模具移动工作台
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <param name="operatorNR">操作员工号</param>
        /// <param name="targetWStationNR">目标工作台号</param>
        /// <returns>移动信息</returns> 
        public Message MoldMoveWorkStation(string moldNR, string operatorNR, string targetWStationNR)
        {
            StorageManageService storageSvc = new StorageManageService();
            return storageSvc.MoldMoveWorkStation(moldNR, operatorNR, targetWStationNR);
        }

    }

}

    

