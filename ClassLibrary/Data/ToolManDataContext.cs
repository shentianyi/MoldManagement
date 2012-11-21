using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary.Data
{
    public partial class ToolManDataContext : IUnitOfWork
    {
        public void Submit()
        {
            this.SubmitChanges();
        }
    }
}
