using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Implement
{
    public class PartRepository : IPartRepository
    {
        ToolManDataContext context;

        public PartRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }
        /// <summary>
        /// add one part
        /// </summary>
        /// <param name="part">the instance of part</param>
       public void Add(Part part)
        {
            context.Part.InsertOnSubmit(part);
       }
   
        /// <summary>
        /// get the list of part by the mold id 
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <returns>the list of part</returns>
       public List<Part> GetByMoldNR(string moldNR)
        {
            List<Part> parts = (from moldpart in context.MoldPart
                                where moldpart.MoldNR.Equals(moldNR)
                                select moldpart.Part).ToList();
            return parts;
       }
       /// <summary>
       /// get the list of part by partGroup id
       /// </summary>
       /// <param name="partGroupNR">the NR of partGroup</param>
       /// <returns>the list of part</returns>
       public List<Part> GetByPartGroupNR(string partGroupNR)
       {
           List<Part> parts = context.Part.Where(p => p.PartGroup.PartGroupNR.Equals(partGroupNR)).ToList();
             return parts;
       }

       /// <summary>
       /// get the part by mold id  and  of the partgroup id
       /// </summary>
       /// <param name="moldNR">the NR of mold</param>
       /// <param name="partGroupNR"></param>
       /// <returns>the instance of part</returns>
      public Part GetByMoldNRAndPartGroupNR(string moldNR, string partGroupNR)
       {
           Part part = (from mp in context.MoldPart
                        where (mp.Part.PartGroupNR.Equals(partGroupNR)
                        && mp.MoldNR.Equals(moldNR))
                        select mp.Part).Single();
           return part;
       }

      /// <summary>
      /// get part nr by mold nr and part group nr
      /// </summary>
      /// <param name="moldNR">the number of mold</param>
      /// <param name="partGroupNR">the number of part group</param>
      /// <returns></returns>
     public string GetPartNRByMoldNRAndPartProupNR(string moldNR, string partGroupNR)
      {
          string partNR = string.Empty;
         if(context.MoldPart.Count(mp=>mp.Part.PartGroupNR.Equals(partGroupNR)&& mp.MoldNR.Equals(moldNR))>0)
             partNR = (from mp in context.MoldPart
                       where (mp.Part.PartGroupNR.Equals(partGroupNR)
                       && mp.MoldNR.Equals(moldNR))
                       select mp.Part).Single().PartNR;
          return partNR;
      }

      /// <summary>
      /// get the parts by multiconditions
      /// </summary>
      /// <param name="partGroupNR"></param>
      /// <param name="partNR"></param>
      /// <param name="warehouseNR"></param>
      /// <param name="postionNR"></param>
      /// <param name="startFIFO"></param>
      /// <param name="endFIFO"></param>
      /// <returns></returns>
      public List<PartView> GetByMutiConditions(string partGroupNR, string partNR, string warehouseNR, string postionNR, DateTime? startFIFO, DateTime? endFIFO)
      {
          List<PartView> parts = context.PartView.Where(pv => (string.IsNullOrEmpty(partGroupNR) ? true : pv.PartGroupNR.Equals(partGroupNR))
             && (string.IsNullOrEmpty(partNR) ? true : pv.PartNR.Equals(partNR))
             && (string.IsNullOrEmpty(postionNR) ? true : pv.PositionNR.Equals(postionNR))
             &&(string.IsNullOrEmpty(warehouseNR)?true:pv.WarehouseNR.Equals(warehouseNR))
             && (startFIFO == null ? true : pv.FIFO >= startFIFO)
             && (endFIFO == null ? true : pv.FIFO <= endFIFO)).OrderBy(pv=>pv.FIFO).ToList();
          return parts;
      }

     /// <summary>
        /// get the list of part by partGroup name
        /// </summary>
        /// <param name="partGroupName">the name of partGroup</param>
        /// <returns>the list of part</returns>
        //public List<Part> GetByPartGroupName(string partGroupName)
        //{
        //    List<Part> parts = context.Part.Where(p => p.PartGroup.Name.Equals(partGroupName)).ToList();
        //    return parts;
        //}

       /// <summary>
       /// get the part by mold id  and the name of the partgroup
       /// </summary>
       /// <param name="moldNR">the NR of mold</param>
       /// <param name="partGroupName"></param>
       /// <returns>the instance of part</returns>
       //public Part GetByMoldNRAndPartGroupName(string moldNR, string partGroupName)
       //{
       //    Part part = (from mp in context.MoldPart
       //                 where (mp.Part.PartGroup.Name.Equals(partGroupName)
       //                 && mp.Mold.MoldID.Equals(moldNR))
       //                 select mp.Part).Single();
       //    return part;
       //}


      /// <summary>
      /// get all parts
      /// </summary>
      /// <returns>the list of all parts</returns>
      public List<Part> All()
      {
        List<Part> parts=context.Part.ToList();
        return parts;
      }
    }
}
