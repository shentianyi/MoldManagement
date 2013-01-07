using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Interface
{
    public interface IMoldCategoryRepository
    {
        /// <summary>
        /// 新建模具类别
        /// </summary>
        /// <param name="moldCate">模具类别</param>
        void Add(MoldCategory moldCate);

        /// <summary>
        /// 新建多个模具类别
        /// </summary>
        /// <param name="moldCates">模具类别列表</param>
        void Add(List<MoldCategory> moldCates);
        /// <summary>
        /// 根据模具类别号删除模具类别
        /// </summary>
        /// <param name="moldCateId">模具类别号</param>
        MoldCategory GetById(string moldCateId);

        /// <summary>
        /// 根据模具类别号获得单个模具类别
        /// </summary>
        /// <param name="moldCateId">模具类别号</param>
        /// <returns>模具类别实例</returns>
        void DeleteById(string moldCateId);

        /// <summary>
        /// 获得全部模具类别
        /// </summary>
        /// <returns>模具类别实例列表</returns>
        List<MoldCategory> All();

    }
}
