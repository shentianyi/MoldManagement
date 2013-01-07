using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Interface
{
    public interface IUniqStorageRepository
    {
        /// <summary>
        /// 添加唯一件库存
        /// </summary>
        /// <param name="uniqStorage">唯一件库存</param>
        void Add(UniqStorage uniqStorage);

        /// <summary>
        /// 根据模具号删除唯一件库存
        /// </summary>
        /// <param name="moldNr">模具号</param>
        void DeleteByMoldNr(string moldNr);
        
    }
}
