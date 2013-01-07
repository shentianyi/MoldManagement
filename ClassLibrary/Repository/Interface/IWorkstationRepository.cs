using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Interface
{
    public interface IWorkstationRepository
    {
        /// <summary>
        /// 根据操作台号获取操作台
        /// </summary>
        /// <param name="workstationId">操作台号</param>
        /// <returns>操作台</returns>
        Workstation GetById(string workstationId);

        /// <summary>
        /// 根据操作台号，判断操作台是否超借
        /// </summary>
        /// <param name="workstationId">操作台号</param>
        /// <returns>判断结果</returns>
        bool OverAppliedMold(string workstationId);

        /// <summary>
        /// 根据操作台号，判断是否存在
        /// </summary>
        /// <param name="workstationNR">操作台号</param>
        /// <returns>判断结果</returns>
        bool Exist(string workstationNr);
      
    }
}
