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
        /// add one employee
        /// </summary>
        /// <param name="employee">the instance of class employee</param>
        void Add(Employee employee);

        /// <summary>
        /// add more than one employees 
        /// </summary>
        /// <param name="employees">the list of employees</param>
        void Add(List<Employee> employees);

        /// <summary>
        /// delete one employee by its id
        /// </summary>
        /// <param name="employeeId">the id of employee</param>
        void DeleteById(string employeeId);

        /*
        /// <summary>
        /// delete employees by its employee type id 
        /// </summary>
        /// <param name="employeeTypeId">he id of employee's employee type id</param>
        void DeleteByEmployeeTypeId(int employeeTypeId);
         */

        /// <summary>
        /// get one employee by its id
        /// </summary>
        /// <param name="employeeId">the id of employee</param>
        /// <returns>one instance of class employee</returns>
        Employee GetById(string employeeId);

        /*
        /// <summary>
        /// get the list of employees by the employee type id
        /// </summary>
        /// <param name="employeeTypeId">the id of employee's employee type id</param>
        /// <returns>the list of employees</returns>
        List<Employee> GetByEmployeeTypeId(int employeeTypeId);
        */

        /// <summary>
        /// get all employees in the form of list
        /// </summary>
        /// <returns>the list of employees</returns>
        List<Employee> All();

        bool Exist(string empNr);
    }
}
