using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Interface
{
    public interface IUniqStorageRepository
    {
        /// <summary>
        /// add one uniqStorage
        /// </summary>
        /// <param name="uniqStorage">the instance of uniqStorage</param>
        void Add(UniqStorage uniqStorage);

        ///// <summary>
        ///// add more than one uniqStorages 
        ///// </summary>
        ///// <param name="uniqStorages">list of uniqStorage</param>
        //void Add(List<UniqStorage> uniqStorages);

        ///// <summary>
        ///// delete storage record by its nr
        ///// </summary>
        ///// <param name="storageNR"></param>
        //void DeleteByStorageNR(string storageNR);

        /// <summary>
        /// delete the uniq storage by the mold nr
        /// </summary>
        /// <param name="moldNr"></param>
        void DeleteByMoldNr(string moldNr);
        /// <summary>
        /// get the list of uniqStorage by its partGroup id
        /// </summary>
        /// <param name="partGroupNR">the NR of partGroup</param>
        /// <returns>the list of uniqStorageview</returns>
       // List<UniqStorageView> GetByPartGroupNR(int partGroupNR);

        /// <summary>
        /// get the list of uniqStorage by its part id
        /// </summary>
        /// <param name="partId">the id of part</param>
        /// <returns>the list of uniqStorageview</returns>
       // List<UniqStorageView> GetByPartId(string partId);

        /// <summary>
        /// get the list of uniqStorage by its warehouse id
        /// </summary>
        /// <param name="partGroupNR">the id of warehouse</param>
        /// <returns>the list of uniqStorageview</returns>
        //List<UniqStorageView> GetByWarehouseId(int warehouseId);

        /// <summary>
        /// get the list of uniqStorage by its position id
        /// </summary>
        /// <param name="partGroupNR">the id of position</param>
        /// <returns>the list of uniqStorageview</returns>
        //List<UniqStorageView> GetByPositionId(int positionId);

        /// <summary>
        /// get the list of uniqStorage by its FIFO range
        /// </summary>
        /// <param name="startFIFO">the FIFO of the uniqStorage</param>
        /// <param name="endFIFO">the FIFO of the uniqStorage</param>
        /// <returns>the list of uniqStorageview</returns>
       // List<UniqStorageView> GetByFIFO(DateTime startFIFO, DateTime endFIFO);
    }
}
