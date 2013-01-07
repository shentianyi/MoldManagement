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
using ToolingManWPF.ConditionServiceReference;
using ToolingManWPF.StorageManageServiceReference;

namespace ToolingManWPF
{
    /// <summary>
    /// MoldInitStore.xaml 的交互逻辑
    /// </summary>
    public partial class MoldInitStore : Window
    {
        /// <summary>
        /// 实例化窗体
        /// </summary>
        public MoldInitStore()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 按钮点击事件-模具添库
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>
        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MoldNRTB.Text) || string.IsNullOrWhiteSpace(WarehouseNRTB.Text) || string.IsNullOrWhiteSpace(PositionNRTB.Text))
            {
                MessageBox.Show("请将数据填写完整");
            }
            else
            {
                ConditionServiceClient conditionclient = new ConditionServiceClient();
                if (!conditionclient.MoldExist(MoldNRTB.Text))
                {
                    MessageBox.Show("此磨具不存在");
                    return;
                }
                StorageManageServiceClient client = new StorageManageServiceClient();
                Message msg = client.MoldInStore(MoldNRTB.Text, OperatorTB.Text, WarehouseNRTB.Text, PositionNRTB.Text);
                MessageBox.Show(msg.Content);
            }
        }
    }
}
