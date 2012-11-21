using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ClassLibrary.Data;
using ClassLibrary.Utilities;
using ClassLibrary.ENUM;
using ToolingWCF.DataModel;

namespace ToolingWCF
{
    [ServiceContract]
    public interface IConditionService
    {
        

        /// <summary>
        /// get the list of mold type as the select condition
        /// </summary>
        /// <returns>the list of mold type</returns>
        [OperationContract]
        List<MoldType> GetMoldTypes();

          [OperationContract]
        List<MoldCategory> GetMoldCates();

        /// <summary>
        /// get the list of project as the select condition
        /// </summary>
        /// <returns>the list project</returns>
        [OperationContract]
        List<Project> GetProjects();

        /// <summary>
        /// get the list of enum item 
        /// </summary>
        /// <param name="enumType">type of enum</param>
        /// <returns></returns>
        [OperationContract]
        List<EnumItem> GetEnumItems(string enumType);

       

        
        /// <summary>
        /// get the warehouse by warehouse type
        /// </summary>
        /// <param name="warehouseType">the type of the warehouse</param>
        /// <returns></returns>
        [OperationContract]
        List<Warehouse> GetWarehousesByType(WarehouseType warehouseType);

        /// <summary>
        /// judege if mold exsit by mold nr
        /// </summary>
        /// <param name="moldNR"></param>
        /// <returns></returns>
       [OperationContract]
        bool MoldExist(string moldNR);

       [OperationContract]
       bool EmpExist(string emoId);

       [OperationContract]
       bool WorkstationExist(string stationNR);

       [OperationContract]
       bool PositionExist(string posiNr);
    }
}
