﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Data;
using ClassLibrary.ENUM;

namespace ClassLibrary.Repository.Implement
{
    public class WarehouseRepository : IWarehouseRepository
    {
          private ToolManDataContext context;

          public WarehouseRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }
          ///// <summary>
          ///// add one warehouse into database
          ///// </summary>
          ///// <param name="warehouse">the instance of warehouse</param>
          //void Add(Warehouse workstation);

          ///// <summary>
          ///// add more than one workstation categories into database
          ///// </summary>
          ///// <param name="warehouses">the list of insstances of warehouse</param>
          //void Add(List<Warehouse> warehouses);

          ///// <summary>
          ///// get one warehouse by its id
          ///// </summary>
          ///// <param name="warehouseId">the id of warehouse</param>
          ///// <returns>the warehouse got by id</returns>
          //Warehouse GetById(int warehouseId);

          /// <summary>
          /// get the warehouse by the type
          /// </summary>
          /// <param name="warehouseType">the type of the warehouse type</param>
          /// <returns></returns>
          public List<Warehouse> GetWarehouseByType(WarehouseType warehouseType)
          {
              List<Warehouse> warehouses=context.Warehouse.Where(w=>w.WarehouseType==warehouseType).ToList();
              return warehouses;
          }

        ///// <summary>
        ///// delete one warehouse by id
        ///// </summary>
        ///// <param name="warehouseId">the id of warehouse</param>
        //void DeleteById(int warehouseId);

        ///// <summary>
        ///// get all workstation categories
        ///// </summary>
        ///// <returns>the workstation categories in form of list</returns>
        //List<Warehouse> All();
    }
}