using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Interface
{
    public interface IWorkstationRepository
    {
        ///// <summary>
        ///// add one workstaion into database
        ///// </summary>
        ///// <param name="workstation">the instance of workstaion</param>
        //void Add(Workstation workstation);

        ///// <summary>
        ///// add one than one workstaions into database
        ///// </summary>
        ///// <param name="workstations">the instances of workstaion</param>
        //void Add(List<Workstation> workstations);

        ///// <summary>
        ///// delete workstation by its id
        ///// </summary>
        ///// <param name="workstationId">the id of workstation</param>
        //void DeleteById(int workstationId);

        ///// <summary>
        ///// delete one or more than one workstations by projectId
        ///// </summary>
        ///// <param name="projectId">the id of the project</param>
        //void DeleteByProjectId(int projectId);

        /// <summary>
        /// get one one workstations by id
        /// </summary>
        /// <param name="workstationId">the id of workstation</param>
        /// <returns>the instance of workstation</returns>
        Workstation GetById(string workstationId);

        /// <summary>
        /// return if the workstation has over applied the mold
        /// </summary>
        /// <param name="workstationId"></param>
        /// <returns></returns>
        bool OverAppliedMold(string workstationId);

        ///// <summary>
        ///// get the workstaions by project id in the form of list
        ///// </summary>
        ///// <param name="projectId">the id of workstaion's project</param>
        ///// <returns>the list of workstation</returns>
        //List<Workstation> GetByProjectId(int projectId);

        ///// <summary>
        ///// get all workstaions
        ///// </summary>
        ///// <returns>the workstations in the form of list</returns>
        //List<Workstation> All();
        
        bool Exist(string workstationNr);
      
    }
}
