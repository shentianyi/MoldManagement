using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.ENUM;

namespace ToolingWCF.Helper
{
    public static class MoldRecordTypeHelper
    {
        static StorageRecordType[] applyTypes = null;
        static StorageRecordType[] produceTypes = null;
        static StorageRecordType[] mantainTypes = null;
        static StorageRecordType[] testTypes = null;
        static StorageRecordType[] returnTypes = null;
        static StorageRecordType[] moveTypes = null;

        static MoldRecordTypeHelper()
        {
            applyTypes = new StorageRecordType[] { StorageRecordType.Produce, StorageRecordType.Mantain, StorageRecordType.Test };
            produceTypes = new StorageRecordType[] { StorageRecordType.Produce };
            mantainTypes = new StorageRecordType[] { StorageRecordType.Mantain };
            testTypes = new StorageRecordType[] { StorageRecordType.Test };
            returnTypes = new StorageRecordType[] { StorageRecordType.Return };
            moveTypes = new StorageRecordType[] { StorageRecordType.MoveWStation };
        }
        public static StorageRecordType[] GetApplyTypes(int index)
        {
            switch (index)
            {
                case 0:
                    return applyTypes;
                case 1:
                    return returnTypes;
                case 2:
                    return moveTypes;
                case 3:
                    return produceTypes;
                case 4:
                    return mantainTypes;
                case 5:
                    return testTypes;
                default:
                    return null;
            }
        }
    }
}
