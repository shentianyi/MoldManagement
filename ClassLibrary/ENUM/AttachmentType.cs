using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClassLibrary.ENUM
{
    public enum AttachmentType
    {
        [Description("图片")]
        PICTURE = 0, // the attachment is picture, like .jpg, .gif
        [Description("文件")]
        DOCUMENT = 1 // the attachment is document, like .pdf, .word
    }
}
