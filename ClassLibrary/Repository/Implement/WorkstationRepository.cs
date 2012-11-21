using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Implement
{
    public class WorkstationRepository : IWorkstationRepository
    {
          private ToolManDataContext context;

          public WorkstationRepository(IUnitOfWork _context)
          {
              this.context = _context as ToolManDataContext;
          }
          /// <summary>
          /// get one one workstations by id
          /// </summary>
          /// <param name="workstationId">the id of workstation</param>
          /// <returns>the instance of workstation</returns>
          public Workstation GetById(string workstationId)
          {
              Workstation workstation = context.Workstation.Single(w => w.WorkstationID.Equals(workstationId));
              return workstation;
          }

        /// <summary>
        /// return if the workstation has over applied the mold
        /// </summary>
        /// <param name="workstationId"></param>
        /// <returns></returns>
          public bool OverAppliedMold(string workstationId)
          {
              Workstation workstation=GetById(workstationId);

              return workstation.CurrentMoldCount < workstation.MaxMoldCount;
          }

          public bool Exist(string workstationNR)
          {
              if (context.Workstation.Where(e => e.WorkstationID.Equals(workstationNR)).Count() > 0)
                  return true;
              return false;
          }
    }
}
