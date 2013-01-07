using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Interface
{
    public interface IMoldTypeRepositotry
    {
        /// <summary>
        /// 新建模具型号
        /// </summary>
        /// <param name="moldType">模具型号</param>
        void Add(MoldType moldType);

        /// <summary>
        /// 新建多个模具型号
        /// </summary>
        /// <param name="moldTypes">模具型号列表</param>
        void Add(List<MoldType> moldTypes);

        /// <summary>
        /// 根据模具型号号获得模具型号
        /// </summary>
        /// <param name="moldTypeId">模具型号号</param>
        /// <returns>模具型号</returns>
        MoldType GetById(string moldTypeId);

        /// <summary>
        /// 根据模具号获得模具型号
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <returns>模具型号</returns>
        MoldType GetByMoldNR(string moldNR);

        /// <summary>
        /// 根据模具型号号删除模具型号
        /// </summary>
        /// <param name="moldTypeId">模具型号号</param>
        void DeleteById(string moldTypeId);


        /// <summary>
        /// 获得全部模具型号
        /// </summary>
        /// <returns>模具型号列表</returns>
        List<MoldType> All();
    }
}
