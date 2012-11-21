using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Implement
{
    public class ProjectRepository : IProjectRepository
    {
        private ToolManDataContext context;

        public ProjectRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }

        ///<summary>
        /// add one project to database
        /// </summary>
        /// <param name="project">the instance of Project</param>
        public void Add(Project project)
        {
            context.Project.InsertOnSubmit(project);
        }

        /// <summary>
        ///  add more than one projects to database
        /// </summary>
        /// <param name="projects">the instances of Project</param>
        public void Add(List<Project> projects)
        {
            context.Project.InsertAllOnSubmit(projects);
        }

        /// <summary>
        ///  get one project by project id
        /// </summary>
        /// <param name="projectId">the id of project</param>
        /// <returns>return one project which's id is passed one</returns>
        public Project GetProjectById(string projectId)
        {
            Project project = context.Project.Single(pro => pro.ProjectID.Equals( projectId));
            return project;
        }

        /// <summary>
        /// delete one project by project id
        /// </summary>
        /// <param name="projectId">the id of project</param>
        public void DeleteById(string projectId)
        {
            context.Project.DeleteOnSubmit(GetProjectById(projectId));
        }

        /// <summary>
        /// get all projects
        /// </summary>
        /// <returns>return the list of projects</returns>
        public  List<Project> All()
        {
            List<Project> projects = context.Project.ToList();
            return projects;
        }

    }
}
