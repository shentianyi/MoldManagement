using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Implement
{

    /// <summary>
    /// 操作台仓库
    /// </summary>
    /// <param name="_context"></param>
    public class WorkstationRepository : IWorkstationRepository
    {
        private ToolManDataContext context;

        /// <summary>
        /// 实例化操作台仓库
        /// </summary>
        /// <param name="_context"></param>
        public WorkstationRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }
        /// <summary>
        /// 根据操作台号获取操作台
        /// </summary>
        /// <param name="workstationId">操作台号</param>
        /// <returns>操作台</returns>
        public Workstation GetById(string workstationId)
        {
            Workstation workstation = context.Workstation.SingleOrDefault(w => w.WorkstationID.Equals(workstationId));
            return workstation;
        }

        /// <summary>
        /// 根据操作台号，判断操作台是否超借
        /// </summary>
        /// <param name="workstationId">操作台号</param>
        /// <returns>判断结果</returns>
        public bool OverAppliedMold(string workstationId)
        {
            Workstation workstation = GetById(workstationId);

            return workstation.CurrentMoldCount < workstation.MaxMoldCount;
        }

        /// <summary>
        /// 根据操作台号，判断是否存在
        /// </summary>
        /// <param name="workstationNR">操作台号</param>
        /// <returns>判断结果</returns>
        public bool Exist(string workstationNR)
        {
            if (context.Workstation.Where(e => e.WorkstationID.Equals(workstationNR)).Count() > 0)
                return true;
            return false;
        }
    }
}
