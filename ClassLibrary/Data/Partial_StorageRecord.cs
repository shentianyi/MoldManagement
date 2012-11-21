using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.ENUM;
using ClassLibrary.Utilities;

namespace ClassLibrary.Data
{
    #region partial class ApplyRecord -- used to defined some special data field
    /// <summary>
    /// partial class applyrecord
    /// </summary>
   public partial class StorageRecord
    {
        private string typeCN;

        public string TypeCN
        {
            get
            {
                this.typeCN = EnumUtil.GetEnumDescriptionByEnumValue(this.RecordType);
                return this.typeCN;
            }
        }
    }
    #endregion
}
