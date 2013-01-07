using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;
using ClassLibrary.ENUM;

namespace ClassLibrary.Repository.Interface
{

    /// <summary>
    /// 库位仓库
    /// </summary>
    public class PositionRepository : IPositionRepository
    {
        ToolManDataContext context;

        /// <summary>
        /// 实例化库位仓库
        /// </summary>
        /// <param name="_context"></param>
        public PositionRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }

        /// <summary>
        /// 根据设备号获得库位
        /// </summary>
        /// <param name="facilictyNR">设备号</param>
        /// <returns>库位</returns>
        public Position GetByFacilictyNR(string facilictyNR)
        {
            return context.UniqStorage.Single(u => u.UniqNR.Equals(facilictyNR)).Position;
        }


        /// <summary>
        /// 根据仓库号、库位号获得库位
        /// </summary>
        /// <param name="warehouseNR">仓库号</param>
        /// <param name="positionNR">库位号</param>
        /// <returns>库位</returns>
        public Position GetByWarehouseNRAndPositionNR(string warehouseNR, string positionNR)
        {
            return context.Position.Single(p => p.WarehouseNR.Equals(warehouseNR) && p.PositionNR.Equals(positionNR));
        }

        /// <summary>
        /// 根据仓库号、库位号判断库位是否可用
        /// </summary>
        /// <param name="warehouseNR">仓库号</param>
        /// <param name="positionNR">库位号</param>
        /// <returns>判断结果</returns>
        public bool CheckPositionAvailable(string warehouseNR, string positionNR, int quantity)
        {
            int count = quantity;
            Position position = context.Position.Single(p => p.WarehouseNR.Equals(warehouseNR) && p.PositionNR.Equals(positionNR));

            switch (context.Warehouse.Single(w => w.WarehouseNR.Equals(warehouseNR)).WarehouseType)
            {
                case WarehouseType.UniqWarehouse:
                    List<UniqStorage> up = context.UniqStorage.Where(u => u.PositionId.Equals(position.PositionID)).ToList();
                    if (up != null && up.Count > 0)
                        count += up.Sum(u => u.Quantity);
                    break;

            }
            return count <= position.Capicity;
        }


        /// <summary>
        /// 根据零件缓存区号获得库位
        /// </summary>
        /// <param name="poolPosiNR">缓存区号</param>
        /// <returns>库位</returns>
        public Position GetPartPoolPosition(string poolPosiNR)
        {
            return context.Position.SingleOrDefault(p => p.PositionNR.Equals(poolPosiNR));
        }


        /// <summary>
        /// 根据库位号判断库位是否存在
        /// </summary>
        /// <param name="posiNr">库位号</param>
        /// <returns>判断结果</returns>
        public bool PositionExsit(string posiNr)
        {
            if (context.Position.SingleOrDefault(p => p.PositionNR.Equals(posiNr)) != null)
                return true;
            return false;
        }
    }
}
