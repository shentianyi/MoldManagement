using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Implement
{
    public class MoldCategoryRepository : IMoldCategoryRepository
    {

        /// <summary>
        /// 模具类别仓库
        /// </summary>
        private ToolManDataContext context;

        /// <summary>
        /// 实例化模具类别仓库
        /// </summary>
        /// <param name="_context"></param>
        public MoldCategoryRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }

        /// <summary>
        /// 新建模具类别
        /// </summary>
        /// <param name="moldCate">模具类别</param>
        public void Add(MoldCategory moldCate)
        {
            context.MoldCategory.InsertOnSubmit(moldCate);
        }

        /// <summary>
        /// 新建多个模具类别
        /// </summary>
        /// <param name="moldCates">模具类别列表</param>
        public void Add(List<MoldCategory> moldCates)
        {
            context.MoldCategory.InsertAllOnSubmit(moldCates);
        }

        /// <summary>
        /// 根据模具类别号删除模具类别
        /// </summary>
        /// <param name="moldCateId">模具类别号</param>
        public void DeleteById(string moldCateId)
        {
            MoldCategory moldcate = GetById(moldCateId);
            context.MoldCategory.DeleteOnSubmit(moldcate);
        }

        /// <summary>
        /// 根据模具类别号获得单个模具类别
        /// </summary>
        /// <param name="moldCateId">模具类别号</param>
        /// <returns>模具类别实例</returns>
        public MoldCategory GetById(string moldCateId)
        {
            MoldCategory moldcate = context.MoldCategory.Single(cate => cate.MoldCateID.Equals(moldCateId));
            return moldcate;
        }

        /// <summary>
        /// 获得全部模具类别
        /// </summary>
        /// <returns>模具类别实例列表</returns>
        public List<MoldCategory> All()
        {
            List<MoldCategory> moldCates = (context.MoldCategory).ToList();
            return moldCates;
        }
    }
}
