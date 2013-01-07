using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Interface
{

    /// <summary>
    /// 唯一件库存仓库
    /// </summary>
    /// <param name="_context"></param>
    public class UniqStorageRepository : IUniqStorageRepository
    {
        ToolManDataContext context;

        /// <summary>
        /// 实例化唯一件库存仓库
        /// </summary>
        /// <param name="_context"></param>
        public UniqStorageRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }
        /// <summary>
        /// 添加唯一件库存
        /// </summary>
        /// <param name="uniqStorage">唯一件库存</param>
        public void Add(UniqStorage uniqStorage)
        {
            context.UniqStorage.InsertOnSubmit(uniqStorage);
        }


        /// <summary>
        /// 根据模具号删除唯一件库存
        /// </summary>
        /// <param name="moldNr">模具号</param>
        public void DeleteByMoldNr(string moldNr)
        {
            context.UniqStorage.DeleteOnSubmit(context.UniqStorage.SingleOrDefault(u => u.UniqNR.Equals(moldNr)));
        }

    }
}
