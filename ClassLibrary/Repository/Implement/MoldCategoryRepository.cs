using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Implement
{
    public class MoldCategoryRepository : IMoldCategoryRepository
    {
        private ToolManDataContext context;

        public MoldCategoryRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }

        // add one mold category
        public void Add(MoldCategory moldCate)
        {
            context.MoldCategory.InsertOnSubmit(moldCate);
        }

        // add mold categories
        public void Add(List<MoldCategory> moldCates)
        {
            context.MoldCategory.InsertAllOnSubmit(moldCates);
        }

        // delete mold category by moldCateId
        public void DeleteById(string moldCateId)
        {
            MoldCategory moldcate = GetById(moldCateId);
            context.MoldCategory.DeleteOnSubmit(moldcate);
        }

        // get mold category by moldCateID
        public MoldCategory GetById(string moldCateId)
        {
            MoldCategory moldcate = context.MoldCategory.Single(cate => cate.MoldCateID.Equals( moldCateId));
            return moldcate;
        }

        // get all mold category --MoldCateID , Name
        public List<MoldCategory> All()
        {
            List<MoldCategory> moldCates = (context.MoldCategory).ToList();
            return moldCates;
        }
    }
}
