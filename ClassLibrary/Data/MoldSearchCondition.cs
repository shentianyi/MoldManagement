using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.ENUM;

namespace ClassLibrary.Data
{
    #region class MoldSearchCondition
    /// <summary>
    /// the mold search condition for searhing the mold
    /// </summary>
    public class MoldSearchCondition
    {
        private string moldNR;
        private string moldTypeId;
        private string projectId;
        private MoldStateType state = MoldStateType.NULL;
        private MoldWarnType warn = MoldWarnType.NULL;


        public string MoldNR
        {
            get { return moldNR; }
            set { moldNR = value; }
        }

        public string MoldTypeId
        {
            get { return moldTypeId; }
            set { moldTypeId = value; }
        }

        public string ProjectId
        {
            get { return projectId; }
            set { projectId = value; }
        }

        public MoldStateType State
        {
            get { return state; }
            set { state = value; }
        }


        public MoldWarnType Warn
        {
            get { return warn; }
            set { warn = value; }
        }

    }
    #endregion
}