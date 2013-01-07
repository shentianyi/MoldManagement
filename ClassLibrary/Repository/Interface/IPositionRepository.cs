using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Interface
{
   public  interface IPositionRepository
    {
        /// <summary>
        /// 根据设备号获得库位
        /// </summary>
        /// <param name="facilictyNR">设备号</param>
        /// <returns>库位</returns>
        Position GetByFacilictyNR(string facilictyNR);

        /// <summary>
        /// 根据仓库号、库位号获得库位
        /// </summary>
        /// <param name="warehouseNR">仓库号</param>
        /// <param name="positionNR">库位号</param>
        /// <returns>库位</returns>
       Position GetByWarehouseNRAndPositionNR(string warehouseNR, string positionNR);

       /// <summary>
       /// 根据仓库号、库位号判断库位是否可用
       /// </summary>
       /// <param name="warehouseNR">仓库号</param>
       /// <param name="positionNR">库位号</param>
       /// <returns>判断结果</returns>
        bool CheckPositionAvailable(string warehouseNR, string positionNR, int quantity);

        /// <summary>
        /// 根据零件缓存区号获得库位
        /// </summary>
        /// <param name="poolPosiNR">缓存区号</param>
        /// <returns>库位</returns>
        Position GetPartPoolPosition(string poolPosiNR);

        /// <summary>
        /// 根据库位号判断库位是否存在
        /// </summary>
        /// <param name="posiNr">库位号</param>
        /// <returns>判断结果</returns>
        bool PositionExsit(string posiNr);
      
    }
}
