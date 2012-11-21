using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Diagnostics.Contracts;
using System.Runtime.Remoting.Services;
using ToolingWCF;

namespace ToolingManHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost condititionHost=null;
            ServiceHost moldpartInfoHost=null;
            ServiceHost storageHost=null;

            try
            {
                condititionHost = new ServiceHost(typeof(ConditionService));
                moldpartInfoHost = new ServiceHost(typeof(MoldPartInfoService));
                storageHost = new ServiceHost(typeof(StorageManageService));
                condititionHost.Open();
                moldpartInfoHost.Open();
                storageHost.Open();
                Console.WriteLine("服务已经启动，请保持其运行...");
                Console.WriteLine("退出请按 Q ，并回车");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                if (condititionHost != null && condititionHost.State == CommunicationState.Opened)
                    condititionHost.Close();
                if (moldpartInfoHost != null && moldpartInfoHost.State == CommunicationState.Opened)
                    moldpartInfoHost.Close();
                if (storageHost != null && storageHost.State == CommunicationState.Opened)
                    storageHost.Close();
            }
          string quit=Console.ReadLine();
          while (true)
          {
              if (quit.Equals("Q"))
              {
                  if (condititionHost != null && condititionHost.State == CommunicationState.Opened)
                      condititionHost.Close();
                  if (moldpartInfoHost != null && moldpartInfoHost.State == CommunicationState.Opened)
                      moldpartInfoHost.Close();
                  if (storageHost != null && storageHost.State == CommunicationState.Opened)
                      storageHost.Close();
                  break;
              }
              quit = Console.ReadLine();
          }
        }
    }
}
