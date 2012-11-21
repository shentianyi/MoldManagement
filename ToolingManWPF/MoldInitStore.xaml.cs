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
        public MoldInitStore()
        {
            InitializeComponent();
        }
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
