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
        /// 新建单个模具最新记录
        /// </summary>
        /// <param name="record">模具最新记录</param>
       void Add(MoldLastRecord record);

       /// <summary>
       /// 根据模具号获得模具最新记录
       /// </summary>
       /// <param name="moldNR">模具号</param>
       /// <returns>模具最新记录实例</returns>
       MoldLastRecord GetByMoldNR(string moldNR);

       /// <summary>
       /// 判断模具是否入库，避免重复入库
       /// </summary>
       /// <param name="moldNR">模具号</param>
       /// <returns>判断结果</returns>
       bool MoldInStored(string moldNR);
    }
}
