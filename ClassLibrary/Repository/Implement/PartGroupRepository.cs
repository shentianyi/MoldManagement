using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Implement
{
    public class PartGroupRepository : IPartGroupRepository
    {
        ToolManDataContext context;

        public PartGroupRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }

        /// <summary>
        /// add one partGroup to database
        /// </summary>
        /// <param name="partGroup">an instance of class PartGroup</param>
        public void Add(PartGroup partGroup)
        {
            context.PartGroup.InsertOnSubmit(partGroup);
        }

        /// <summary>
        /// add more than one partGroups to database
        /// </summary>
        /// <param name="partGroups">the list of partGroups</param>
        public void Add(List<PartGroup> partGroups)
        {
            context.PartGroup.InsertAllOnSubmit(partGroups);
        }

        /// <summary>
        /// get one partGroup by its's id
        /// </summary>
        /// <param name="partGroupNR">the id of the partGroup</param>
        /// <returns>the </returns>
        public PartGroup GetById(string partGroupNR)
        {
            PartGroup partGroup = context.PartGroup.Single(a => a.PartGroupNR.Equals( partGroupNR));
            return partGroup;
        }

        /// <summary>
        /// delete one partGroup by it's id
        /// </summary>
        /// <param name="partGroupNR">return one project which one's id is passed</param>
        public void DeleteById(string partGroupNR)
        {
            context.PartGroup.DeleteOnSubmit(GetById(partGroupNR));
        }

        /// <summary>
        /// get all partGroups in the form of list
        /// </summary>
        /// <returns></returns>
        public List<PartGroup> All()
        {
            return (context.PartGroup).ToList();
        }
    }
}
