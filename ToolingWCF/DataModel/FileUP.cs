using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.ENUM;
using System.Runtime.Serialization;

namespace ToolingWCF.DataModel
{
    [DataContract]
    public class FileUP
    {
        private AttachmentType type;
        private string fileType;
        private string name;
        private byte[] data;

        [DataMember]
        public AttachmentType Type
        {
            get { return type; }
            set { type = value; }
        }

        [DataMember]
        public string FileType
        {
            get { return fileType; }
            set { fileType = value; }
        }

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public byte[] Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}
