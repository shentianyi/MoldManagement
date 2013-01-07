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
        /// 获取模具类别列表
        /// </summary>
        /// <returns>模具类别列表</returns>
        public List<MoldCategory> GetMoldCates()
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IMoldCategoryRepository moldcateRep = new MoldCategoryRepository(unitwork);
                List<MoldCategory> moldCate = moldcateRep.All();
                return moldCate;
            }
        }

        /// <summary>
        /// 获取模具型号列表
        /// </summary>
        /// <returns>模具型号列表</returns>
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
        /// 获取成本中心列表
        /// </summary>
        /// <returns>成本中心列表</returns>
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
        ///  根据枚举类型获得枚举列表
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns>枚举列表</returns>
        public List<EnumItem> GetEnumItems(string enumType)
        {
            List<EnumItem> items = EnumUtil.GetEnumItemList(enumType);
            return items;
        }
       

      

        /// <summary>
        /// 根据仓库类型获取仓库列表
        /// </summary>
        /// <param name="warehouseType">仓库类型</param>
        /// <returns>仓库列表</returns>
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
        /// 根据模具号判断模具是否存在
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <returns>判断结果</returns>
        public bool MoldExist(string moldNR)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IMoldRepository moldRep = new MoldRepository(unitwork);
                return moldRep.MoldExsit(moldNR);
            }
        }

        /// <summary>
        /// 根据员工号判断员工是否存在
        /// </summary>
        /// <param name="empId">员工号</param>
        /// <returns>判断结果</returns>
        public bool EmpExist(string empId)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IEmployeeRepository empRep = new EmployeeRepository(unitwork);
                return empRep.Exist(empId);
            }
        }

        /// <summary>
        /// 根据工作台号判断工作台是否存在
        /// </summary>
        /// <param name="stationId">工作台号</param>
        /// <returns>判断结果</returns>
        public bool WorkstationExist(string stationId)
        {
            using (IUnitOfWork unitwork = MSSqlHelper.DataContext())
            {
                IWorkstationRepository workRep = new WorkstationRepository(unitwork);
                return workRep.Exist(stationId);
            }
        }


        /// <summary>
        /// 根据库存号判断库存是否存在
        /// </summary>
        /// <param name="posiNr">库存号</param>
        /// <returns>判断结果</returns>
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
