using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.ENUM;
using ClassLibrary.Utilities;

namespace ClassLibrary.Data
{
   public partial class MoldView:IEquatable<MoldView>
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


        public bool Equals(MoldView other)
        {
            if (object.ReferenceEquals(other, null)) return false;
            if (object.ReferenceEquals(this, other)) return true;
            return this.MoldNR.Equals(other.MoldNR);
        }

        public override int GetHashCode()
        {
            return this.MoldNR.GetHashCode();
            //return base.GetHashCode();
        }
    }
}
