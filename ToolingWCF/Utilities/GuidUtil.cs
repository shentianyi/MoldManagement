using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary.Utility
{
   public static class GuidUtil
    {
       static GuidUtil() { }

       public static Guid GenerateGUID()
       {
           Guid g = Guid.NewGuid();
           return g;
       }
    }
}
