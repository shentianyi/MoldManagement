using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ToolingManWPF.Utilities
{
    public class FileUtil
    {
        /// <summary>
        /// sava the data as file in the file path
        /// </summary>
        /// <param name="filepath">file save path</param>
        /// <param name="data">file data in form of byte[]</param>
        /// <returns></returns>
        public static bool SavaFIle(string filepath, byte[] data)
        {
            try
            {
                using (FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write))
                {
                    using (Stream stream = new MemoryStream(data))
                    {
                        const int bufferLenth = 4096;
                        byte[] buffer = new byte[bufferLenth];
                        int readcount = 0;
                        do
                        {
                            readcount = stream.Read(buffer, 0, bufferLenth);
                            if (readcount > 0)
                            {
                                fs.Write(buffer, 0, readcount);
                            }
                        } while (readcount > 0);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
