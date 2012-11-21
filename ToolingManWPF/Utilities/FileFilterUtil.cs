using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolingManWPF.Utilities
{
  public static class FileFilterUtil
    {
      /// <summary>
      /// get image filter
      /// </summary>
      /// <returns></returns>
      public static List<string> GetImageFilters()
      {
         List<string> imageFilter=new List<string> ();
          string[] img = new ConfigUtil("IMAGE").GetAllNodeValue();
          foreach (string s in img)
          {
              imageFilter.Add(s);
          }
          return imageFilter;
      }

      /// <summary>
      /// get document filter
      /// </summary>
      /// <returns></returns>
      public static List<string> GetDocumentFilters()
      {
          List<string> documentFilter = new List<string>();
          string[] doc = (new ConfigUtil("DOCUMENT")).GetAllNodeValue();
          foreach (string s in doc)
          {
              documentFilter.Add(s);
          }
          return documentFilter;
      }

      public static string GetFiltersString()
      {
          string[] img = new ConfigUtil("IMAGE").GetAllNodeValue();
          string[] doc = (new ConfigUtil("DOCUMENT")).GetAllNodeValue();
          string filter = string.Empty;
          if (img.Length > 0)
          {
              foreach (string s in img)
              {
                  filter += "*" + s + ";";
              }
              filter.TrimEnd(';');
          }

          if (doc.Length > 0)
          {
              foreach (string s in doc)
              {
                  filter += "*" + s + ";";
              }
              filter.TrimEnd(';');
          }
          return filter;
      }
    }
}
