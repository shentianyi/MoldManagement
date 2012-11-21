using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Repository.Implement;
using ClassLibrary.Utilities;
using System.Configuration;
using Nini;
using Nini.Config;
using System.Reflection;
using ToolingWCF.Utilities;
using ToolingWCF.Properties;
using ClassLibrary.ENUM;
using ToolingWCF.DataModel;

namespace ToolingWCF
{
    public class ConditionService : IConditionService
    {
        
        /// <summary>
        /// get the list of mold type as the select condition
        /// </summary>
        /// <returns>the list of mold type</returns>
        public List<MoldCategory> GetMoldCates()
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                //IMoldTypeRepositotry moldTypeRepository = new MoldTypeRepository(unitwork);
                //List<MoldType> moldTypes = moldTypeRepository.All();
                //return moldTypes;
                IMoldCategoryRepository moldcateRep = new MoldCategoryRepository(unitwork);
                List<MoldCategory> moldCate = moldcateRep.All();
                return moldCate;
            }
        }

        public List<MoldType> GetMoldTypes()
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IMoldTypeRepositotry moldTypeRepository = new MoldTypeRepository(unitwork);
                List<MoldType> moldTypes = moldTypeRepository.All();
                return moldTypes;
            }
        }


        /// <summary>
        /// get the list of project as the select condition
        /// </summary>
        /// <returns>the list project</returns>
        public List<Project> GetProjects()
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IProjectRepository projectRepository = new ProjectRepository(unitwork);
                List<Project> projects = projectRepository.All();
                return projects;
            }
        }
       

        /// <summary>
        /// get the list of enum item 
        /// </summary>
        /// <param name="enumType">type of enum</param>
        /// <returns></returns>
        public List<EnumItem> GetEnumItems(string enumType)
        {
            List<EnumItem> items = EnumUtil.GetEnumItemList(enumType);
            return items;
        }
       

      

        /// <summary>
        /// get the warehouse by warehouse type
        /// </summary>
        /// <param name="warehouseType">the type of the warehouse</param>
        /// <returns></returns>
        public List<Warehouse> GetWarehousesByType(WarehouseType warehouseType)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IWarehouseRepository warehouseRep = new WarehouseRepository(unitwork);
                List<Warehouse> warehouses = warehouseRep.GetWarehouseByType(warehouseType);
                return warehouses;
            }
        }

        /// <summary>
        /// judege if mold exsit by mold nr
        /// </summary>
        /// <param name="moldNR"></param>
        /// <returns></returns>
        public bool MoldExist(string moldNR)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IMoldRepository moldRep = new MoldRepository(unitwork);
                return moldRep.MoldExsit(moldNR);
            }
        }

        public bool EmpExist(string empId)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IEmployeeRepository empRep = new EmployeeRepository(unitwork);
                return empRep.Exist(empId);
            }
        }

        public bool WorkstationExist(string stationId)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IWorkstationRepository workRep = new WorkstationRepository(unitwork);
                return workRep.Exist(stationId);
            }
        }



        public bool PositionExist(string posiNr)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IPositionRepository posiRep = new PositionRepository(unitwork);
              return  posiRep.PositionExsit(posiNr);
            }
        }
    }
}
