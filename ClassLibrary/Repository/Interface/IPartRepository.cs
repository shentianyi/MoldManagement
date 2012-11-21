using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;
using ClassLibrary.ENUM;

namespace ClassLibrary.Repository.Interface
{
    public interface IPartRepository
    {
        /// <summary>
        /// add one part
        /// </summary>
        /// <param name="part">the instance of part</param>
        void Add(Part part);

        ///// <summary>
        ///// add more than one part
        ///// </summary>
        ///// <param name="parts">the list of part</param>
        //void Add(List<Part> parts);

        ///// <summary>
        ///// delete one part by its id
        ///// </summary>
        ///// <param name="partId">the id of part</param>
        //void DeleteById(string partId);

        ///// <summary>
        ///// delete parts by its partGroup
        ///// </summary>
        ///// <param name="partGroupNR">the NR of partGroup</param>
        //void DeleteByPartGroupNR(int partGroupNR);

        ///// <summary>
        ///// get one part by its id
        ///// </summary>
        ///// <param name="partId">the id of part</param>
        ///// <returns>one instance of part</returns>
        //Part GetById(string partId);

        /// <summary>
        /// get the list of part by the mold id 
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <returns>the list of part</returns>
        List<Part >GetByMoldNR(string moldNR);

        ///// <summary>
        ///// get the list of part
        ///// </summary>
        ///// <param name="partGroupNR">the NR of partGroup</param>
        ///// <returns>the list of part</returns>
        //List<Part> GetByPartGroupNR(int partGroupNR);

        /// <summary>
        /// get the list of part by partGroup id
        /// </summary>
        /// <param name="partGroupNR">the NR of partGroup</param>
        /// <returns>the list of part</returns>
        List<Part> GetByPartGroupNR(string partGroupNR);

        /// <summary>
        /// get the part by mold id  and  of the partgroup id
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        /// <param name="partGroupNR"></param>
        /// <returns>the instance of part</returns>
        Part GetByMoldNRAndPartGroupNR(string moldNR, string partGroupNR);

        /// <summary>
        /// get part nr by mold nr and part group nr
        /// </summary>
        /// <param name="moldNR">the number of mold</param>
        /// <param name="partGroupNR">the number of part group</param>
        /// <returns></returns>
        string GetPartNRByMoldNRAndPartProupNR(string moldNR, string partGroupNR);
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
        List<PartView> GetByMutiConditions(string partGroupNR,string partNR,string warehouseNR,string postionNR,DateTime? startFIFO,DateTime? endFIFO);

        /// <summary>
        /// get all parts
        /// </summary>
        /// <returns>the list of all parts</returns>
        List<Part> All();
    }
}
