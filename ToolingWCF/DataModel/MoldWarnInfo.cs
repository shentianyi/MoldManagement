using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ToolingWCF.DataModel
{
    [DataContract]
    public class MoldWarnInfo : MoldBaseInfo
    {
        [DataMember]
        public double MaxLendHour { get; set; }
        [DataMember]
        public DateTime LendTime { get; set; }
        [DataMember]
        public DateTime ShouldReTime { get { return this.LendTime.AddHours(this.MaxLendHour); } set { } }
        [DataMember]
        public double DisMinute
        {
            get
            {
                TimeSpan ts = new TimeSpan(this.LendTime.AddHours(this.MaxLendHour).Ticks - DateTime.Now.Ticks);
                return ts.TotalMinutes;
            }
            set { }
        }
        [DataMember]
        public string DisTimeText
        {
            get
            {
                return ((int)this.DisMinute / 60).ToString() + "小时" + ((int)Math.Abs(this.DisMinute)%60) + "分";
            }
            set { }
        }
        [DataMember]
        public bool Minus { get { return this.DisMinute < 0; } set { } }
    }
}
