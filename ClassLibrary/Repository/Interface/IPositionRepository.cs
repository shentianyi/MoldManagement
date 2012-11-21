using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Interface
{
   public  interface IPositionRepository
    {
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
        Position GetByFacilictyNR(string facilictyNR);
       
       /// <summary>
       /// get position by warehouse nr and position nr
       /// </summary>
       /// <param name="warehouseNR"></param>
       /// <param name="positionNR"></param>
       /// <returns></returns>
       Position GetByWarehouseNRAndPositionNR(string warehouseNR, string positionNR);

       /// <summary>
       /// check position available
       /// </summary>
       /// <param name="warehouseNR"></param>
       /// <param name="positionNR"></param>
       /// <returns></returns>
        bool CheckPositionAvailable(string warehouseNR, string positionNR, int quantity);


        Position GetPartPoolPosition(string poolPosiNR);

       /// <summary>
       /// check position exsits
       /// </summary>
       /// <param name="posiNr"></param>
       /// <returns></returns>
        bool PositionExsit(string posiNr);
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
    }
}
