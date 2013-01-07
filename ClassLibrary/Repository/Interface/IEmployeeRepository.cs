using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Interface
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// 新建单个职工
        /// </summary>
        /// <param name="employee">职工</param>
        void Add(Employee employee);

        /// <summary>
        /// 新建多个职工
        /// </summary>
        /// <param name="employees">职工列表</param>
        void Add(List<Employee> employees);

        /// <summary>
        /// 根据职工工号，判断职工是否错在
        /// </summary>
        /// <param name="empNr">职工工号</param>
        /// <returns>判断结果</returns>
        bool Exist(string empNr);
    }
}
