using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Implement
{

    /// <summary>
    /// 成本中心仓库
    /// </summary>
    public class ProjectRepository : IProjectRepository
    {
        private ToolManDataContext context;

        /// <summary>
        /// 实例化成本中心仓库
        /// </summary>
        /// <param name="_context"></param>
        public ProjectRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }

        ///<summary>
        /// 新建成本中心
        /// </summary>
        /// <param name="project">成本中心</param>
        public void Add(Project project)
        {
            context.Project.InsertOnSubmit(project);
        }

        /// <summary>
        /// 新建多个成本中心
        /// </summary>
        /// <param name="projects">成本中心列表</param>
        public void Add(List<Project> projects)
        {
            context.Project.InsertAllOnSubmit(projects);
        }

        /// <summary>
        ///  根据成本中心号获得成本中心
        /// </summary>
        /// <param name="projectId">成本中心号</param>
        /// <returns>成本中心</returns>
        public Project GetProjectById(string projectId)
        {
            Project project = context.Project.Single(pro => pro.ProjectID.Equals(projectId));
            return project;
        }

        /// <summary>
        /// 根据成本中心号获得成本中心
        /// </summary>
        /// <param name="projectId">成本中心号</param>
        public void DeleteById(string projectId)
        {
            context.Project.DeleteOnSubmit(GetProjectById(projectId));
        }

        /// <summary>
        /// 获得全部成本中心
        /// </summary>
        /// <returns>成本中心列表</returns>
        public List<Project> All()
        {
            List<Project> projects = context.Project.ToList();
            return projects;
        }
    }
}
