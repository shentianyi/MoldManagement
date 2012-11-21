using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Interface
{
    public interface IMoldCategoryRepository
    {
        // add one mold category
        void Add(MoldCategory moldCate);

        // add mold categories
        void Add(List<MoldCategory> moldCates);

        // get mold category by moldCateID
        MoldCategory GetById(string moldCateId);

        // delete mold category by moldNR
        void DeleteById(string moldCateId);

        // get all mold category --MoldCateId , Name
        List<MoldCategory> All();

    }
}
