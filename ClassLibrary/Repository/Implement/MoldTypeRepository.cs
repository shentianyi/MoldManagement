using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Implement
{
    public class MoldTypeRepository : IMoldTypeRepositotry
    {
        private ToolManDataContext context;

        public MoldTypeRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }

        /// <summary>
        /// add one mold type
        /// </summary>
        /// <param name="moldType">the instance of mold type</param>
        public void Add(MoldType moldCate)
        {
            context.MoldType.InsertOnSubmit(moldCate);
        }

        /// <summary>
        /// add mold types in the form of list
        /// </summary>
        /// <param name="moldTypes">the list of mold types</param>
        public void Add(List<MoldType> moldTypes)
        {
            context.MoldType.InsertAllOnSubmit(moldTypes);
        }

        /// <summary>
        /// get mold type by mold id
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <returns>the instance of mold type</returns>
        public MoldType GetByMoldNR(string moldNR)
        {
            MoldType moldType = (from m in context.Mold
                                 where m.MoldNR.Equals( moldNR)
                                 select m.MoldType).Single();
            return moldType;
        }

        /// <summary>
        /// delete the mold type by its id
        /// </summary>
        /// <param name="moldTypeId">the NR of mold type</param>
        public void DeleteById(string moldTypeId)
        {
            MoldType moldtype = GetById(moldTypeId);
            context.MoldType.DeleteOnSubmit(moldtype);
        }

        /// <summary>
        /// get one mold type by its id
        /// </summary>
        /// <param name="moldTypeId">the NR of mold type</param>
        /// <returns>the instance of mold type</returns>
        public MoldType GetById(string moldTypeId)
        {
            MoldType moldtype = context.MoldType.Single(type => type.MoldTypeID.Equals( moldTypeId));
            return moldtype;
        }

        /// <summary>
        /// get all mold types in the form of list
        /// </summary>
        /// <returns>the list of mold types</returns>
        public List<MoldType> All()
        {
            List<MoldType> moldTypes = (context.MoldType).ToList();
            return moldTypes;
        }
    }
}
