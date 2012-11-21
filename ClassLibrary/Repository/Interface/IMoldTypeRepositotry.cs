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
        /// add one mold type
        /// </summary>
        /// <param name="moldType">the instance of mold type</param>
        void Add(MoldType moldType);

        /// <summary>
        /// add mold types in the form of list
        /// </summary>
        /// <param name="moldTypes">the list of mold types</param>
        void Add(List<MoldType> moldTypes);

       /// <summary>
       /// get one mold type by its id
       /// </summary>
       /// <param name="moldTypeId">the NR of mold type</param>
       /// <returns>the instance of mold type</returns>
        MoldType GetById(string moldTypeId);

        /// <summary>
        /// get mold type by mold id
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <returns>the instance of mold type</returns>
        MoldType GetByMoldNR(string moldNR);

        /// <summary>
        /// delete the mold type by its id
        /// </summary>
        /// <param name="moldTypeId">the NR of mold type</param>
        void DeleteById(string moldTypeId);

       /// <summary>
       /// get all mold types in the form of list
       /// </summary>
       /// <returns>the list of mold types</returns>
        List<MoldType> All();
    }
}
