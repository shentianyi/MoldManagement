using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Implement
{
    public class AttachmentRepository : IAttachmentRepository
    {
        ToolManDataContext context;

        public AttachmentRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }
        /// <summary>
        /// add mold attachments 
        /// </summary>
        /// <param name="attachments">the instances of mold attachment</param>
        public void Add(List<Attachment> moldAttach)
        {
            context.Attachment.InsertAllOnSubmit(moldAttach);
        }

        /// <summary>
        /// get the mold attachment by the master id
        /// </summary>
        /// <param name="masterNR">the NR of master</param>
        public List<Attachment> GetByMasterNR(string masterNR)
        {
            List<Attachment> attaches = context.Attachment.Where(mc => mc.MasterNR.Equals(masterNR)).ToList();
            return attaches;
        }

        /// <summary>
        /// get sigle attachment by master NR
        /// </summary>
        /// <param name="masterNR"></param>
        /// <returns></returns>
        public Attachment GetSingleByMasterNR(string masterNR)
        {
            Attachment attach = context.Attachment.Single(mc => mc.MasterNR.Equals(masterNR));
            return attach;
        }
        /// <summary>
        /// delete one mold attachment by its id
        /// </summary>
        /// <param name="attachmentId">the NR of mold attachment</param>
        public void DeleteById(int AttachmentId)
        { }

        /// <summary>
        /// delete the attachments by the mold id
        /// </summary>
        /// <param name="moldNR">the NR of mold</param>
        public void DeleteByMoldNR(string reportId)
        { }
    }
}
