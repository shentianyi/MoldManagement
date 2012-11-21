using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ToolingWCF.DataModel
{
    [DataContract]
    public class Message
    {
        private MsgType msgType;
        private string content;

        [DataMember]
        public MsgType MsgType
        {
            get { return msgType; }
            set { msgType = value; }
        }
        
        [DataMember]
        public string Content
        {
            get { return content; }
            set { content = value; }
        }
    }
}
