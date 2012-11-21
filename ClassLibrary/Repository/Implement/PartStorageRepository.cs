using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Interface
{
    public class PartStorageRepository : IPartStorageRepository
    {

        ToolManDataContext context;

        public PartStorageRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }
        /// <summary>
        /// add one uniqStorage
        /// </summary>
        /// <param name="uniqStorage">the instance of uniqStorage</param>
        public void Add(PartStorage uniqStorage)
        {
            context.PartStorage.InsertOnSubmit(uniqStorage);
        }

        /// <summary>
        /// get  part storage
        /// </summary>
        /// <param name="warehouseNR"></param>
        /// <param name="positionNR"></param>
        /// <param name="partNR"></param>
        /// <returns></returns>
        public PartStorage GetPartStorage(string warehouseNR,DateTime date, string positionNR, string partNR)
        {
            PartStorage partStorage = context.Position
                .Single(p => p.WarehouseNR.Equals(warehouseNR) && p.PositionNR.Equals(positionNR))
                .PartStorage.Where(ps => ps.PartNR.Equals(partNR)&&ps.FIFO.Equals(date)).Take(1).Single();
            //List<PartStorage> partStorage = context.Position.Single(p => p.WarehouseNR.Equals(warehouseNR) && p.PositionNR.Equals(positionNR)).PartStorage.ToList();
           
            return partStorage;
        }

         /// <summary>
        /// delete part storage
        /// </summary>
        /// <param name="partStorage"></param>
       public void Delete(PartStorage partStorage)
        {
            context.PartStorage.DeleteOnSubmit(partStorage);
       }

       /// <summary>
       /// check if part is in store
       /// </summary>
       /// <param name="partNR"></param>
       /// <returns></returns>
       public bool CheckPartInStore(string partNR)
       {
           int count = context.PartStorage.Count(ps=>ps.PartNR.Equals(partNR));

           return count > 0;
       }

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
