using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;
using ClassLibrary.ENUM;

namespace ClassLibrary.Repository.Interface
{
   public  class PositionRepository:IPositionRepository
    {
           ToolManDataContext context;

        public PositionRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }
        ///// <summary>
        /////  add one position into database
        ///// </summary>
        // /// <param name="position">the instance of position</param>
        //void Add(Position position);

        ///// <summary>
        ///// add positions into the database
        ///// </summary>
        ///// <param name="positions">the list of positions</param>
        //void Add(List<Position> positions);

        ///// <summary>
        ///// get one position by its id, the id is uniqueidentifier
        ///// </summary>
        ///// <param name="positionId">the id of position</param>
        ///// <returns>the instance of position</returns>
        //Position GetById(Guid positionId);

        /// <summary>
        /// get position by facility nr
        /// </summary>
        /// <param name="facilictyNR"></param>
        /// <returns></returns>
       public Position GetByFacilictyNR(string facilictyNR)
       {
           return context.UniqStorage.Single(u => u.UniqNR.Equals(facilictyNR)).Position;
       }


       /// <summary>
       /// get position by warehouse nr and position nr
       /// </summary>
       /// <param name="warehouseNR"></param>
       /// <param name="positionNR"></param>
       /// <returns></returns>
       public Position GetByWarehouseNRAndPositionNR(string warehouseNR, string positionNR)
       {
           return context.Position.Single(p => p.WarehouseNR.Equals(warehouseNR) && p.PositionNR.Equals(positionNR));
       }

        /// <summary>
       /// check position available
       /// </summary>
       /// <param name="warehouseNR"></param>
       /// <param name="positionNR"></param>
       /// <returns></returns>
       public bool CheckPositionAvailable(string warehouseNR, string positionNR, int quantity)
       {
           int count=quantity;
           Position position = context.Position.Single(p => p.WarehouseNR.Equals(warehouseNR) && p.PositionNR.Equals(positionNR));

           switch (context.Warehouse.Single(w => w.WarehouseNR.Equals(warehouseNR)).WarehouseType)
           {
               case WarehouseType.UniqWarehouse:
                   List<UniqStorage> up = context.UniqStorage.Where(u => u.PositionId.Equals(position.PositionID)).ToList();
                   if(up!=null&&up.Count>0)
                      count+=up.Sum(u => u.Quantity);
                   break;
            
           }
           return count <= position.Capicity;
       }

        ///// <summary>
        ///// get positions by warehouse id
        ///// </summary>
        ///// <param name="wareHouseId">the id of warehouse</param>
        ///// <returns>the list of positions</returns>
        //List<Position> GetByWarehouseId(int wareHouseId);

        ///// <summary>
        /////  delete one position by its id
        ///// </summary>
        ///// <param name="positionId">the id of the position</param>
        //void DeleteById(Guid positionId);

        ///// <summary>
        ///// delete the position by its warehouse id
        ///// </summary>
        ///// <param name="warehouseId">the id of the warehouse</param>
        //void DeleteByWarehouseId(int warehouseId);

        ///// <summary>
        ///// get all the positions
        ///// </summary>
        ///// <returns>the list of all positions</returns>
        //List<Position> All();


       public Position GetPartPoolPosition(string poolPosiNR)
       {
           return context.Position.SingleOrDefault(p => p.PositionNR.Equals(poolPosiNR));
       }


       /// <summary>
       /// check position exsits
       /// </summary>
       /// <param name="posiNr"></param>
       /// <returns></returns>
       public bool PositionExsit(string posiNr)
       {
           if (context.Position.SingleOrDefault(p => p.PositionNR.Equals(posiNr)) != null)
               return true;
           return false;
       }
    }
}
