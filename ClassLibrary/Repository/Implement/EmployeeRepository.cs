using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Implement
{
  public  class EmployeeRepository : IEmployeeRepository
    {
        ToolManDataContext context;

        public EmployeeRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }

        public void Add(Data.Employee employee)
        {
            throw new NotImplementedException();
        }

        public void Add(List<Data.Employee> employees)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(string employeeId)
        {
            throw new NotImplementedException();
        }

        public Data.Employee GetById(string employeeId)
        {
            throw new NotImplementedException();
        }

        public List<Data.Employee> All()
        {
            throw new NotImplementedException();
        }

        public bool Exist(string empNr)
        {
            if (context.Employee.Where(e => e.EmployeeID.Equals(empNr)).Count() > 0)
                return true;
            return false;
        }
    }
}
