using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Interface
{
    public interface IProjectRepository
    {
        /// <summary>
        /// add one project to database
        /// </summary>
        /// <param name="project">the instance of Project</param>
        void Add(Project project);

        /// <summary>
        ///  add more than one projects to database
        /// </summary>
        /// <param name="projects">the instances of Project</param>
        void Add(List<Project> projects);

        /// <summary>
        ///  get one project by project id
        /// </summary>
        /// <param name="projectId">the id of project</param>
        /// <returns>return one project which one's id is passed</returns>
        Project GetProjectById(string projectId);

        /// <summary>
        /// delete one project by project id
        /// </summary>
        /// <param name="projectId">the id of project</param>
        void DeleteById(string projectId);

        /// <summary>
        /// get all projects
        /// </summary>
        /// <returns>return the list of projects</returns>
        List<Project> All();
    }
}
