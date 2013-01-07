using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;
using ClassLibrary.ENUM;

namespace ClassLibrary.Repository.Interface
{
    public interface IMoldRepository
    {
        /// <summary>
        /// 新建模具
        /// </summary>
        /// <param name="mold">模具</param>
        void Add(Mold mold);
        /// <summary>
        /// 新建多个模具
        /// </summary>
        /// <param name="molds">模具列表</param>
        void Add(List<Mold> molds);
        /// <summary>
        /// 根据模具号删除模具
        /// </summary>
        /// <param name="moldNR">模具号</param>
        void DeleteById(string id);

        /// <summary>
        /// 根据模具型号号删除模具
        /// </summary>
        /// <param name="moldTypeId">模具型号号</param>
        void DeleteByMoldTypeId(string moldTypeId);

        /// 根据模具号获得模具
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <returns>模具实例</returns>
        Mold GetById(string id);
        /// <summary>
        /// 根据模具型号号获得模具
        /// </summary>
        /// <param name="moldTypeId">模具型号号</param>
        /// <returns>模具列表</returns>
        List<Mold> GetByMoldTypeId(string moldTypeId);
        /// <summary>
        /// 根据项目号获得模具
        /// </summary>
        /// <param name="projectId">项目号</param>
        /// <returns>模具列表</returns>
        List<Mold> GetByProjectId(string projectId);

        /// <summary>
        /// 根据模具状态获得模具
        /// </summary>
        /// <param name="moldState">模具状态</param>
        /// <returns>模具列表</returns>
        List<Mold> GetByState(MoldStateType moldState);

        /// <summary>
        ///  根据模具号获得模具视图
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <returns>模具视图</returns>
        MoldView GetMoldViewByMoldNR(string moldNR);
        ///  <summary>
        /// 根据多条件获得模具
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>模具列表</returns>
        List<MoldView> GetByMutiConditions(MoldSearchCondition condition);

        /// <summary>
        /// 根据模具警报状态获得模具视图
        /// </summary>
        /// <param name="type">模具警报状态</param>
        /// <returns>模具视图列表</returns>
        List<MoldView> GetByWarnType(MoldWarnType type);

        /// <summary>
        /// 根据库存记录号获得模具当前位置
        /// </summary>
        /// <param name="storageRecordNR">库存记录号</param>
        /// <returns>模具当前位置</returns>
        string GetMoldCurrPosiByRecordNR(Guid storageRecordNR);

        /// <summary>
        /// 根据模具号获得模具当前位置
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <returns>模具当前位置</returns>
        string GetMoldCurrPosiByMoldNR(string moldNR);

        /// <summary>
        /// 根据模具号判断模具是否存在
        /// </summary>
        /// <param name="moldNR">模具号</param>
        /// <returns>判断结果</returns>
        bool MoldExsit(string moldNR);


        /// 根据库存位置号获得模具号
        /// </summary>
        /// <param name="posiNr">库存位置号</param>
        /// <returns>模具号</returns>
        string GetMoldNrByPosiNr(string posiNr);

        /// <summary>
        /// 获得全部模具
        /// </summary>
        /// <returns>模具列表</returns>
        List<Mold> All();
    }
}
