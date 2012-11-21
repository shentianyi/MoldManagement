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
      /// add mold attachments 
      /// </summary>
      /// <param name="attachments">the instances of mold attachment</param>
      void Add(List<Attachment> moldAttach);

      /// <summary>
      /// get the mold attachment by the mold nr
      /// </summary>
      /// <param name="moldNR">the NR of mold</param>
      List<Attachment> GetByMasterNR(string masterNR);

      
        /// <summary>
        /// get sigle attachment by master NR
        /// </summary>
        /// <param name="masterNR"></param>
        /// <returns></returns>
      Attachment GetSingleByMasterNR(string masterNR);

      /// <summary>
      /// delete one mold attachment by its id
      /// </summary>
      /// <param name="attachmentId">the NR of mold attachment</param>
      void DeleteById(int AttachmentId);

      /// <summary>
      /// delete the attachments by the mold id
      /// </summary>
      /// <param name="moldNR">the NR of mold</param>
      void DeleteByMoldNR(string reportId);
    }
}
