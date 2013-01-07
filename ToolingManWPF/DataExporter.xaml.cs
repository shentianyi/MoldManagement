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
using ToolingManWPF.MoldPartInfoServiceReference;
using Microsoft.Win32;
using ToolingManWPF.Helper;

namespace ToolingManWPF
{
    /// <summary>
    /// DataExporter.xaml 的交互逻辑
    /// </summary>
    public partial class DataExporter : Window
    {

        private static DataExporter instance = null;
        SaveFileDialog saveFileDialog = null;

        static readonly object padlock = new object();
        public static DataExporter Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null) { instance = new DataExporter(); }
                    return instance;
                }
            }
        }

        public DataExporter()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

        private void LabDataQueryBtn_Click(object sender, RoutedEventArgs e)
        {
            LabDataDG.ItemsSource = GetReportView();       
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        { 
            saveFileDialog = new SaveFileDialog();         
            saveFileDialog.Filter = "导出Excel (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.Title = "导出文件保存路径";

            ConditionServiceClient conditionClient = new ConditionServiceClient();
            List<EnumItem> types = conditionClient.GetEnumItems(typeof(ToolingManWPF.ConditionServiceReference.ReportType).ToString());
            ReportTypeCB.ItemsSource = types;
            ReportTypeCB.SelectedIndex = 0;

            List<EnumItem> storeTypes = new List<EnumItem>() { new EnumItem() { Value = "0", Description = "申领记录" },
                new EnumItem() { Value = "1", Description = "归还记录" },  new EnumItem() { Value = "2", Description = "移动工作台记录" } };
            StorageRecordTypeCB.ItemsSource = storeTypes;
            StorageRecordTypeCB.SelectedIndex = 0;
        }

        private void LabDataExportBtn_Click(object sender, RoutedEventArgs e)
        {
            saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            if ((bool)saveFileDialog.ShowDialog())
            {

                List<ReportView> items = GetReportView();
                string[] headers = { "员工工号", "姓名", "模具号", "操作时间" };
                string[] pathes = { "OperatorID", "Name", "MoldID", "Date" };
                if (items.Count > 0)
                    if (GenExcelHelper.GenExcel<ReportView>(saveFileDialog.FileName, ((EnumItem)ReportTypeCB.SelectedItem).Description + "数据", headers, pathes, items))
                        MessageBox.Show("数据导出成功，请到保存路径查看");
                    else
                        MessageBox.Show("数据导出失败(可能指定文件已经打开)，请重试");
                else
                    MessageBox.Show("不存在记录数据");
            }
        }
         

        private void StoreDataQueryBtn_Click(object sender, RoutedEventArgs e)
        {
            StoreDataDG.ItemsSource = GetStoreRecordView();
        }

        private void StoreDataExportBtn_Click(object sender, RoutedEventArgs e)
        {
            saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            if ((bool)saveFileDialog.ShowDialog())
            {

                List<StorageRecord> items = GetStoreRecordView();
                string[] headers = { "员工工号", "操作员工号" ,"模具号","源位置", "目标位置","操作时间" };
                string[] pathes = { "ApplicantId", "OperatorId", "TargetNR", "Source", "Destination", "Date" };
                if (items.Count > 0)
                    if (GenExcelHelper.GenExcel<StorageRecord>(saveFileDialog.FileName, ((EnumItem)StorageRecordTypeCB.SelectedItem).Description + "数据", headers, pathes, items))
                        MessageBox.Show("数据导出成功，请到保存路径查看");
                    else
                        MessageBox.Show("数据导出失败(可能指定文件已经打开)，请重试");
                else
                    MessageBox.Show("不存在记录数据");
            }

        }


        private List<ReportView> GetReportView()
        {
            DateTime? startDate = null;
            DateTime? endDate = null;
            startDate = startDateDP.Text.Length == 0 ? startDate : DateTime.Parse(startDateDP.Text);
            endDate = endDateDP.Text.Length == 0 ? endDate : DateTime.Parse(endDateDP.Text);
            List<ReportView> items = (new MoldPartInfoServiceClient()).GetReportViewByDate(
                (ToolingManWPF.MoldPartInfoServiceReference.ReportType)(int.Parse(((EnumItem)ReportTypeCB.SelectedItem).Value)),
              startDate, endDate);
            return items;
        }


        private List<StorageRecord> GetStoreRecordView()
        {
            DateTime? startDate = null;
            DateTime? endDate = null;
            startDate = startDateDP.Text.Length == 0 ? startDate : DateTime.Parse(storeStartDateDP.Text);
            endDate = endDateDP.Text.Length == 0 ? endDate : DateTime.Parse(storeEndDateDP.Text);
            List<StorageRecord> items = (new MoldPartInfoServiceClient()).GetStoreRecordByDate(int.Parse(((EnumItem)StorageRecordTypeCB.SelectedItem).Value),
              startDate, endDate);
            return items;
        }
    }
}
