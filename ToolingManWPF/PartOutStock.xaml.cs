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
using ToolingManWPF.MoldPartInfoServiceReference;


namespace ToolingManWPF
{
    /// <summary>
    /// PartOutStock.xaml 的交互逻辑
    /// </summary>
    public partial class PartOutStock : Window
    {
        public PartOutStock()
        {
            InitializeComponent();
        }

        private PartView partView;

        public PartOutStock(PartView partView)
        {
            this.partView = partView;

            InitializeComponent();

            InitialPartOutValue();
        }
        /// <summary>
        /// part out stock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PartNRTB.Text) || string.IsNullOrWhiteSpace(QuantityTB.Text)
                || string.IsNullOrWhiteSpace(WarehouseNRTB.Text) || string.IsNullOrWhiteSpace(PositionNRTB.Text) || string.IsNullOrWhiteSpace(FIFODP.Text))
            {
                MessageBox.Show("请将数据填写完整");
            }
            else
            {
                StorageManageServiceClient client = new StorageManageServiceClient();
                ToolingManWPF.StorageManageServiceReference.Message msg = client.PartOutStore(PartNRTB.Text, "", int.Parse(QuantityTB.Text), DateTime.Parse(FIFODP.Text), WarehouseNRTB.Text, PositionNRTB.Text);
                MessageBox.Show(msg.Content);
            }
        }

        private void InitialPartOutValue()
        {
            PartNRTB.Text = partView.PartNR;
            WarehouseNRTB.Text = partView.WarehouseNR;
            PositionNRTB.Text = partView.PositionNR;
            FIFODP.Text = partView.FIFO.ToString();

            PartNRTB.IsEnabled = false;
            WarehouseNRTB.IsEnabled = false;
            PositionNRTB.IsEnabled = false;
            FIFODP.IsEnabled = false;
        }
    }
}
