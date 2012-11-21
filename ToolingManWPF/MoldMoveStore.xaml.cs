using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToolingManWPF.MoldPartInfoServiceReference;
using ToolingManWPF.StorageManageServiceReference;
using ToolingManWPF.Properties;

namespace ToolingManWPF
{
    /// <summary>
    /// MoldMoveStore.xaml 的交互逻辑
    /// </summary>
    public partial class MoldMoveStore : Window
    {
        public MoldMoveStore(string moldNr,string moldPosi)
        {
            InitializeComponent();
            MoldNrLab.Content = moldNr;
            CurrentPosiLab.Content = moldPosi;
            OKBtn.IsEnabled = false;
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            StorageManageServiceClient client = new StorageManageServiceClient();
            Message msg = client.MoldMoveStore(MoldNrLab.Content.ToString(), Settings.Default.WarehouseNr, CurrentPosiLab.Content.ToString(), DesiPosiNRTB.Text);
             MessageBox.Show(msg.Content);
             if (msg.MsgType == MsgType.OK)
             {
                 this.Close();
             }
        }

        private void CheckMoldBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DesiPosiNRTB.Text.Length > 0)
            {
                MoldPartInfoServiceClient client = new MoldPartInfoServiceClient();
                string moldNr = client.GetMoldNrByPosiNr(DesiPosiNRTB.Text);
                if (moldNr.Length > 0)
                {
                    DesiPosiMoldNrLab.Content = moldNr + "        >>移库将调换两个模具库位";
                }
                else {
                    DesiPosiMoldNrLab.Content = "目标位置不存在模具";
                }
                OKBtn.IsEnabled = true;
            }
        }

        private void DesiPosiNRTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            OKBtn.IsEnabled = false;
        }
    }
}
