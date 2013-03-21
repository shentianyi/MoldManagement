using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Repository.Interface;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Implement
{
    /// <summary>
    /// 附件仓库
    /// </summary>
    public class AttachmentRepository : IAttachmentRepository
    {
        ToolManDataContext context;

        /// <summary>
        /// 实例化附件仓库
        /// </summary>
        /// <param name="_context"></param>
        public AttachmentRepository(IUnitOfWork _context)
        {
            this.context = _context as ToolManDataContext;
        }
        /// <summary>
        /// 新建多个附件 
        /// </summary>
        /// <param name="attachments">附件列表</param>
        public void Add(List<Attachment> attachments)
        {
            context.Attachment.InsertAllOnSubmit(attachments);
        }

        /// <summary>
        /// 根据附主号获得附件列表
        /// </summary>
        /// <param name="masterNR">附主号</param>
        public List<Attachment> GetByMasterNR(string masterNR)
        {
            List<Attachment> attaches = context.Attachment.Where(mc => mc.MasterNR.Equals(masterNR)).ToList();
            return attaches;
        }

        /// <summary>
        /// 根据辅助号获得单个附件
        /// </summary>
        /// <param name="masterNR">附主号</param>
        /// <returns>附件实例</returns>
        public Attachment GetSingleByMasterNR(string masterNR)
        {
            Attachment attach = context.Attachment.Single(mc => mc.MasterNR.Equals(masterNR));
            return attach;
        }


        public void DeleteById(int id)
        {
            Attachment attach = context.Attachment.SingleOrDefault(a => a.AttachmentId == id);
            context.Attachment.DeleteOnSubmit(attach);
        }
    }
}
