using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.ENUM;
using ClassLibrary.Utilities;

namespace ClassLibrary.Data
{
    #region partial class Mold -- used to defined some special data field
    /// <summary>
    /// partial class mold
    /// </summary>
    public partial class Mold
    {
        private string stateCN;

        public string StateCN
        {
            get
            {
                this.stateCN = EnumUtil.GetEnumDescriptionByEnumValue(this.State);
                return this.stateCN;
            }
        }
  
    }
    #endregion
}
