using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Interface
{
    public interface IPartStorageRepository
    {
        /// <summary>
        /// add one partStorage
        /// </summary>
        /// <param name="partStorage">the instance of partStorage</param>
        void Add(PartStorage partStorage);


       /// <summary>
        /// get  part storage
        /// </summary>
        /// <param name="warehouseNR"></param>
        /// <param name="positionNR"></param>
        /// <param name="partNR"></param>
        /// <returns></returns>
        PartStorage GetPartStorage(string warehouseNR,DateTime date, string positionNR, string partNR);


        /// <summary>
        /// delete part storage
        /// </summary>
        /// <param name="partStorage"></param>
        void Delete(PartStorage partStorage);

        /// <summary>
        /// check if part is in store
        /// </summary>
        /// <param name="partNR"></param>
        /// <returns></returns>
        bool CheckPartInStore(string partNR);

        ///// <summary>
        ///// add more than one partStorages 
        ///// </summary>
        ///// <param name="partStorages">list of partStorage</param>
        //void Add(List<PartStorage> partStorages);

        /// <summary>
        /// get the list of partStorage by its partGroup id
        /// </summary>
        /// <param name="partGroupNR">the NR of partGroup</param>
        /// <returns>the list of partStorageview</returns>
       // List<PartStorageView> GetByPartGroupNR(int partGroupNR);

        /// <summary>
        /// get the list of partStorage by its part id
        /// </summary>
        /// <param name="partId">the id of part</param>
        /// <returns>the list of partStorageview</returns>
       // List<PartStorageView> GetByPartId(string partId);

        /// <summary>
        /// get the list of partStorage by its warehouse id
        /// </summary>
        /// <param name="partGroupNR">the id of warehouse</param>
        /// <returns>the list of partStorageview</returns>
        //List<PartStorageView> GetByWarehouseId(int warehouseId);

        /// <summary>
        /// get the list of partStorage by its position id
        /// </summary>
        /// <param name="partGroupNR">the id of position</param>
        /// <returns>the list of partStorageview</returns>
        //List<PartStorageView> GetByPositionId(int positionId);

        /// <summary>
        /// get the list of partStorage by its FIFO range
        /// </summary>
        /// <param name="startFIFO">the FIFO of the partStorage</param>
        /// <param name="endFIFO">the FIFO of the partStorage</param>
        /// <returns>the list of partStorageview</returns>
       // List<PartStorageView> GetByFIFO(DateTime startFIFO, DateTime endFIFO);
    }
}
