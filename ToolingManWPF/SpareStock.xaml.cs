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
using System.Windows.Threading;
using ToolingManWPF.MoldPartInfoServiceReference;

namespace ToolingManWPF
{
    /// <summary>
    /// SpareStock.xaml 的交互逻辑
    /// </summary>
    public partial class SpareStock : Window
    {
        public SpareStock()
        {
            InitializeComponent();
        }

        private delegate void LoadConditionDeleteagete();
            
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new LoadConditionDeleteagete(LoadConditions));
        }

        /// <summary>
        /// load conditions 
        /// </summary>
        private void LoadConditions()
        {
            ConditionServiceClient conditionClient = new ConditionServiceClient();
            List<PartGroup> partGroups = conditionClient.GetPartGroups();
            partGroups.Insert(0, new PartGroup() { PartGroupNR = "", Name = "--------全部-------" });
            PartGroupCB.ItemsSource = partGroups;

            List<Warehouse> warehouses = conditionClient.GetWarehousesByType(WarehouseType.PartWarehouse);
            warehouses.Insert(0, new Warehouse() { WarehouseNR = "", Name = "--------全部-------" });
            WarehouseCB.ItemsSource = warehouses;

        }

        /// <summary>
        /// change the part when part group changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PartGroupCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PartGroupCB.SelectedIndex > 0)
            {
                ConditionServiceClient conditionClient = new ConditionServiceClient();
                List<Part> parts = conditionClient.GetPartsByGroupNR(PartGroupCB.SelectedValue.ToString());
                parts.Insert(0, new Part() { PartNR = "--------全部-------", Name = "" });
                PartCB.ItemsSource = parts;
                PartCB.SelectedIndex = 0;
                PartCB.IsEnabled = true;
            }
            else
            {   
                PartCB.SelectedIndex = 0;
                PartCB.IsEnabled = false;
            }
        }

        /// <summary>
        /// query the part storage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryBtn_Click(object sender, RoutedEventArgs e)
        {
            MoldPartInfoServiceClient client = new MoldPartInfoServiceClient();
            DateTime? startDT = null;
            DateTime? endDT = null;
            startDT = startDateDP.Text.Length == 0 ? startDT : DateTime.Parse(startDateDP.Text);
            endDT = endDateDP.Text.Length == 0 ? endDT : DateTime.Parse(endDateDP.Text);
            string partGroupNR = PartGroupCB.SelectedIndex > 0 ? PartGroupCB.SelectedValue.ToString() : string.Empty;
            string partNR = PartCB.SelectedIndex > 0 ? PartCB.SelectedValue.ToString() : string.Empty;
            string warehouseNR = WarehouseCB.SelectedIndex > 0 ? WarehouseCB.SelectedValue.ToString() : string.Empty;
            string positionNR = PositionNRTB.Text;

            List<PartView> parts = client.GetPartsByMutiConditions(partGroupNR, partNR, warehouseNR, positionNR, startDT, endDT);
            PartStorageDG.ItemsSource = parts;

        }

        /// <summary>
        /// part out stock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PartOutBtn_Click(object sender, RoutedEventArgs e)
        {
            PartView pv = PartStorageDG.SelectedItem as  PartView;
            if (pv != null&&!string.IsNullOrEmpty(pv.PositionNR))
            {
                (new PartOutStock(pv)).ShowDialog();
            }
        }

        /// <summary>
        /// part in store
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PartInBtn_Click(object sender, RoutedEventArgs e)
        {
            (new PartInStock()).ShowDialog();
        }


    }
}
