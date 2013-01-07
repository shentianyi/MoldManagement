using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;
using ClassLibrary.ENUM;

namespace ClassLibrary.Repository.Interface
{
   public interface IWarehouseRepository
    {
        /// <summary>
        /// 根据仓库类型获得仓库
        /// </summary>
        /// <param name="warehouseType">仓库类型</param>
        /// <returns>仓库列表</returns>
       List<Warehouse> GetWarehouseByType(WarehouseType warehouseType);
    }
}
