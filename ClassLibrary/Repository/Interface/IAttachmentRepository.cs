using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Data;

namespace ClassLibrary.Repository.Interface
{
  public  interface IAttachmentRepository
  {
      /// <summary>
      /// 新建多个附件 
      /// </summary>
      /// <param name="attachments">附件列表</param>
      void Add(List<Attachment> moldAttach);

      /// <summary>
      /// 根据附主号获得附件列表
      /// </summary>
      /// <param name="masterNR">附主号</param>
      List<Attachment> GetByMasterNR(string masterNR);


      /// <summary>
      /// 根据辅助号获得单个附件
      /// </summary>
      /// <param name="masterNR">附主号</param>
      /// <returns>附件实例</returns>
      Attachment GetSingleByMasterNR(string masterNR);

      void DeleteById(int id);

    }
}
