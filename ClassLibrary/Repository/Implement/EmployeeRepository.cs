using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Implement
{ 
    /// <summary>
    /// 员工仓库
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        ToolManDataContext context;

        /// <summary>
        /// 实例化员工仓库
        /// </summary>
        /// <param name="_context"></param>
        public EmployeeRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }
        /// <summary>
        /// 新建单个职工
        /// </summary>
        /// <param name="employee">职工</param>
        public void Add(Data.Employee employee)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 新建多个职工
        /// </summary>
        /// <param name="employees">职工列表</param>
        public void Add(List<Data.Employee> employees)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 根据职工工号，判断职工是否错在
        /// </summary>
        /// <param name="empNr">职工工号</param>
        /// <returns>判断结果</returns>
        public bool Exist(string empNr)
        {
            if (context.Employee.Where(e => e.EmployeeID.Equals(empNr)).Count() > 0)
                return true;
            return false;
        }
    }
}
