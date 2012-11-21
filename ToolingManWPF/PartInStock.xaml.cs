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
using ToolingManWPF.StorageManageServiceReference;

namespace ToolingManWPF
{
    /// <summary>
    /// PartInStock.xaml 的交互逻辑
    /// </summary>
    public partial class PartInStock : Window
    {
        public PartInStock()
        {
            InitializeComponent();
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PartNRTB.Text) || string.IsNullOrWhiteSpace(QuantityTB.Text)
                || string.IsNullOrWhiteSpace(WarehouseNRTB.Text) || string.IsNullOrWhiteSpace(PositionNRTB.Text))
            {
                MessageBox.Show("请将数据填写完整");
            }
            else
            {
                StorageManageServiceClient client = new StorageManageServiceClient();
                Message msg= client.PartInStore(PartNRTB.Text, "", int.Parse(QuantityTB.Text),
                    string.IsNullOrEmpty(FIFODP.Text) ? DateTime.Parse(DateTime.Now.ToShortDateString()) : DateTime.Parse(FIFODP.Text), WarehouseNRTB.Text, PositionNRTB.Text);
                MessageBox.Show(msg.Content);
            }
        }
    }
}
