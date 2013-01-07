using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Interface
{
    public interface IProjectRepository
    {
        ///<summary>
        /// 新建成本中心
        /// </summary>
        /// <param name="project">成本中心</param>
        void Add(Project project);

        /// <summary>
        /// 新建多个成本中心
        /// </summary>
        /// <param name="projects">成本中心列表</param>
        void Add(List<Project> projects);

        /// <summary>
        ///  根据成本中心号获得成本中心
        /// </summary>
        /// <param name="projectId">成本中心号</param>
        /// <returns>成本中心</returns>
        Project GetProjectById(string projectId);

        /// <summary>
        /// 根据成本中心号获得成本中心
        /// </summary>
        /// <param name="projectId">成本中心号</param>
        void DeleteById(string projectId);

        /// <summary>
        /// 获得全部成本中心
        /// </summary>
        /// <returns>成本中心列表</returns>
        List<Project> All();
    }
}
