using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Data;
using ClassLibrary.ENUM;

namespace ClassLibrary.Repository.Implement
{

    /// <summary>
    /// 仓库仓库
    /// </summary>
    /// <param name="_context"></param>
    public class WarehouseRepository : IWarehouseRepository
    {
        private ToolManDataContext context;

        /// <summary>
        /// 实例化仓库仓库
        /// </summary>
        /// <param name="_context"></param>
        public WarehouseRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }

        /// <summary>
        /// 根据仓库类型获得仓库
        /// </summary>
        /// <param name="warehouseType">仓库类型</param>
        /// <returns>仓库列表</returns>
        public List<Warehouse> GetWarehouseByType(WarehouseType warehouseType)
        {
            List<Warehouse> warehouses = context.Warehouse.Where(w => w.WarehouseType == warehouseType).ToList();
            return warehouses;
        }
    }
}
