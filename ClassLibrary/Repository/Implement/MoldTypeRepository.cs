using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Implement
{

    /// <summary>
    /// 模具类别仓库
    /// </summary>
    public class MoldTypeRepository : IMoldTypeRepositotry
    {
        private ToolManDataContext context;

        /// <summary>
        /// 实例化模具类别仓库
        /// </summary>
        /// <param name="_context"></param>
        public MoldTypeRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }

        /// <summary>
        /// 新建模具型号
        /// </summary>
        /// <param name="moldType">模具型号</param>
        public void Add(MoldType moldType)
        {
            context.MoldType.InsertOnSubmit(moldType);
        }

        /// <summary>
        /// 新建多个模具型号
        /// </summary>
        /// <param name="moldTypes">模具型号列表</param>
        public void Add(List<MoldType> moldTypes)
        {
            context.MoldType.InsertAllOnSubmit(moldTypes);
        }

        /// <summary>
        /// 根据模具号获得模具型号
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <returns>模具型号</returns>
        public MoldType GetByMoldNR(string moldNR)
        {
            MoldType moldType = (from m in context.Mold
                                 where m.MoldNR.Equals(moldNR)
                                 select m.MoldType).Single();
            return moldType;
        }

        /// <summary>
        /// 根据模具型号号删除模具型号
        /// </summary>
        /// <param name="moldTypeId">模具型号号</param>
        public void DeleteById(string moldTypeId)
        {
            MoldType moldtype = GetById(moldTypeId);
            context.MoldType.DeleteOnSubmit(moldtype);
        }

        /// <summary>
        /// 根据模具型号号获得模具型号
        /// </summary>
        /// <param name="moldTypeId">模具型号号</param>
        /// <returns>模具型号</returns>
        public MoldType GetById(string moldTypeId)
        {
            MoldType moldtype = context.MoldType.Single(type => type.MoldTypeID.Equals(moldTypeId));
            return moldtype;
        }

        /// <summary>
        /// 获得全部模具型号
        /// </summary>
        /// <returns>模具型号列表</returns>
        public List<MoldType> All()
        {
            List<MoldType> moldTypes = (context.MoldType).ToList();
            return moldTypes;
        }
    }
}
