using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Interface
{
    public interface IMoldPartRepository
    {
        /// <summary>
        /// add one moldpart--the relationship of mold and part
        /// </summary>
        /// <param name="moldpart">the instance of moldpart</param>
        void Add(MoldPart moldpart);

        /// <summary>
        /// add more than one moldpart 
        /// </summary>
        /// <param name="moldparts">the list of moldpart</param>
        void Add(List<MoldPart> moldparts);

        /// <summary>
        /// dele one moldpart by its id
        /// </summary>
        /// <param name="moldpartId">the NR of moldpartr</param>
        void DeleteById(int moldpartId);
    }
}
