using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Implement
{

    /// <summary>
    /// 模具最新记录仓库
    /// </summary>
    public class MoldLastRecordRepository : IMoldLastRecordRepository
    {
        ToolManDataContext context;

        /// <summary>
        /// 实例化模具最新记录仓库
        /// </summary>
        /// <param name="_context"></param>
        public MoldLastRecordRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }
        /// <summary>
        /// 新建单个模具最新记录
        /// </summary>
        /// <param name="record">模具最新记录</param>
        public void Add(MoldLastRecord record)
        {
            context.MoldLastRecord.InsertOnSubmit(record);
        }

        /// <summary>
        /// 根据模具号获得模具最新记录
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <returns>模具最新记录实例</returns>
        public MoldLastRecord GetByMoldNR(string moldNR)
        {
            MoldLastRecord MoldLastRecord = context.MoldLastRecord.Single(m => m.MoldNR.Equals(moldNR));
            return MoldLastRecord;
        }

        /// <summary>
        /// 判断模具是否入库，避免重复入库
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <returns>判断结果</returns>
        public bool MoldInStored(string moldNR)
        {
            return context.MoldLastRecord.Where(ml => ml.MoldNR.Equals(moldNR)).ToList().Count > 0;
        }
    }
}
