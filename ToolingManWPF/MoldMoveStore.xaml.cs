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

        /// <summary>
        /// 实例化窗体
        /// </summary>
       /// <param name="moldNr">模具号</param>
       /// <param name="moldPosi">模具库位号</param>
        public MoldMoveStore(string moldNr,string moldPosi)
        {
            InitializeComponent();
            MoldNrLab.Content = moldNr;
            CurrentPosiLab.Content = moldPosi;
            OKBtn.IsEnabled = false;
        }
        /// <summary>
        /// 按钮点击事件-模具移库
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>
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

        /// <summary>
        /// 按钮点击事件-判断模具移库合法性
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>
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
