using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Interface
{
   public interface IMoldLastRecordRepository
    {

       /// <summary>
       /// add the last apply record
       /// </summary>
       /// <param name="record"></param>
       void Add(MoldLastRecord record);

       /// <summary>
       /// get the mold lastest apply record by mold id
       /// </summary>
       /// <param name="moldNR">the NR of mold</param>
        /// <returns>the instance of MoldLastApplyRecord</returns>
       MoldLastRecord GetByMoldNR(string moldNR);

       /// <summary>
       /// check wheathe mold has been in store , avoid reistore
       /// </summary>
       /// <param name="moldNR"></param>
       /// <returns></returns>
       bool MoldInStored(string moldNR);
    }
}
