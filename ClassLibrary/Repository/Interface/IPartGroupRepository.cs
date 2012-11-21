using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Interface
{
    public interface IPartGroupRepository
    {
        /// <summary>
        /// add one PartGroup to database
        /// </summary>
        /// <param name="partGroup">an instance of class PartGroup</param>
        void Add(PartGroup partGroup);

        /// <summary>
        /// add more than one partGroups to database
        /// </summary>
        /// <param name="partGroups">the list of partGroups</param>
        void Add(List<PartGroup> partGroups);

        /// <summary>
        /// delete PartGroup by its id
        /// </summary>
        /// <param name="partGroupNR">the id of the PartGroup</param>
        void DeleteById(string partGroupNR);

        /// <summary>
        /// get one PartGroup by its's id
        /// </summary>
        /// <param name="partGroupId">the id of the PartGroup</param>
        /// <returns>the instance of PartGroup</returns>
        PartGroup GetById(string partGroupId);

        /// <summary>
        /// get all partGroups in the form of list
        /// </summary>
        /// <returns>the list of partGroups</returns>
        List<PartGroup> All();

    }
}
