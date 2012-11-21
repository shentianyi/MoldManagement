using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.ENUM;
using ClassLibrary.Utilities;

namespace ToolingWCF.Utilities
{
    public class PrefixUtil
    {
        /// <summary>
        /// get the prefix of the mold position
        /// </summary>
        /// <param name="sotrageRecordType">type of apply the mold</param>
        /// <returns>the prefix in chinese</returns>
        public static string GetMoldPositionPrefix(StorageRecordType sotrageRecordType)
        {
            string prefix = string.Empty;

            switch (sotrageRecordType)
            {
                case StorageRecordType.Produce:
                    prefix = EnumUtil.GetEnumDescriptionByEnumValue(PositionPrefixType.Workstation);
                    break;
                case StorageRecordType.Mantain:
                    prefix = EnumUtil.GetEnumDescriptionByEnumValue(PositionPrefixType.Mantainstation);
                    break;
                case StorageRecordType.Test:
                    prefix = EnumUtil.GetEnumDescriptionByEnumValue(PositionPrefixType.Teststation);
                    break;
                case StorageRecordType.OutStore:
                    prefix = EnumUtil.GetEnumDescriptionByEnumValue(PositionPrefixType.InPosition);
                    break;
            }
            return prefix;
        }
    }
}
