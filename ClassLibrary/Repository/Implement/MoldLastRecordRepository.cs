using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Implement
{
    public class MoldLastRecordRepository : IMoldLastRecordRepository
    {
        ToolManDataContext context;

        public MoldLastRecordRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }
        /// <summary>
        /// add the last apply record
        /// </summary>
        /// <param name="record"></param>
        public void Add(MoldLastRecord record)
        {
            context.MoldLastRecord.InsertOnSubmit(record);
        }

        /// <summary>
        /// get the mold lastest apply record by mold id
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <returns>the instance of MoldLastRecord</returns>
        public MoldLastRecord GetByMoldNR(string moldNR)
        {
            MoldLastRecord MoldLastRecord = context.MoldLastRecord.Single(m => m.MoldNR.Equals(moldNR));
            return MoldLastRecord;
        }

        /// <summary>
        /// check wheathe mold has been in store , avoid reistore
        /// </summary>
        /// <param name="moldNR"></param>
        /// <returns></returns>
        public bool MoldInStored(string moldNR)
        {
            return context.MoldLastRecord.Where(ml => ml.MoldNR.Equals(moldNR)).ToList().Count > 0;
        }
    }
}
