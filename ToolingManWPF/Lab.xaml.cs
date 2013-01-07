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
using Microsoft.Win32;
using System.Collections;
using ToolingManWPF.Utilities;
using System.Data;
using ToolingManWPF.StorageManageServiceReference;
using ToolingManWPF.Data;
using System.IO;
using System.Windows.Threading;
using ToolingManWPF.ConditionServiceReference;
using ToolingManWPF.MoldPartInfoServiceReference;

namespace ToolingManWPF
{
    /// <summary>
    /// Lab.xaml 的交互逻辑
    /// </summary>
    public partial class Lab : Window
    {
        private OpenFileDialog fileDialog;
        private List<string> documentFilter;
        private List<string> imageFilter;
        private List<string> fileControl;

        private static Lab instance = null;

        static readonly object padlock = new object();
        public static Lab Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null) { instance = new Lab(); }
                    return instance;
                }
            }
        }

        /// <summary>
        /// 实例化窗体
        /// </summary>
        public Lab()
        {
            fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Filter = " 可选文件 |" + FileFilterUtil.GetFiltersString();
            fileControl = new List<string>();
            //UserListBoxItem
            InitializeComponent();
        }


        private delegate void LoadConditionDelegate();

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new LoadConditionDelegate(LoadConditions));
            // LoadConditions();
        }

        /// <summary>
        /// 按钮点击事件-显示浏览文件窗口
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>
        private void ChooseFileBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)fileDialog.ShowDialog())
            {
                string[] files = fileDialog.FileNames;

                if (fileDialog.FileNames.Length > 0)
                {
                    foreach (string path in files)
                    {
                        if (!fileControl.Contains(path))
                        {
                            UserListBoxItem item =
                                 new UserListBoxItem()
                                 {
                                     Display = path.Substring(path.LastIndexOf('\\') + 1, path.Length - path.LastIndexOf('\\') - 1),
                                     Value = path
                                 };
                            FileNameList.Items.Add(item);
                            fileControl.Add(path);
                        }
                    }

                    FileNameList.DisplayMemberPath = "Display";
                    FileNameList.SelectedValuePath = "Value";
                }
            }
        }

        /// <summary>
        /// 按钮点击事件-移除文件
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>
        private void RemoveFileBtn_Click(object sender, RoutedEventArgs e)
        {
            if (FileNameList.SelectedIndex > -1)
                FileNameList.Items.RemoveAt(FileNameList.SelectedIndex);
        }

        /// <summary>
        /// 按钮点击事件-上传报告
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>
        private void UpReportBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MoldNRTB.Text.Length > 0 && OperatorNRTB.Text.Length > 0)
            {
                StorageManageServiceClient client = new StorageManageServiceClient();
                ConditionServiceClient conditionclient = new ConditionServiceClient();
                BasicMessage bmsg = new BasicMessage();
                if (!conditionclient.MoldExist(MoldNRTB.Text))
                {
                    bmsg.Result = false;
                    bmsg.MsgContent.Add("模具");
                }
                if (OperatorNRTB.Text.Length != 0 && !conditionclient.EmpExist(OperatorNRTB.Text))
                {
                    bmsg.Result = false;
                    bmsg.MsgContent.Add("试验员");
                }
                if (bmsg.Result == false)
                {
                    MessageBox.Show(bmsg.MsgText + " 不存在，请重新输入");
                    return;
                }
                // MoldPartInfoServiceClient bclient = new MoldPartInfoServiceClient();
                //MoldBaseInfo moldBaseInfo = bclient.GetMoldBaseInfoByNR(MoldNRTB.Text);
                //if (moldBaseInfo.State == ToolingManWPF.MoldPartInfoServiceReference.MoldStateType.NotReturned)
                //{
                //    MessageBox.Show("模具还未归还！");
                //    return;
                //}

                List<FileUP> files = null;
                long fileTotalLength = 0;
                if (FileNameList.Items.Count > 0)
                {
                    files = new List<FileUP>();
                    UserListBoxItem userListBoxItem;

                    imageFilter = FileFilterUtil.GetImageFilters();
                    documentFilter = FileFilterUtil.GetDocumentFilters();
                   

                    foreach (object item in FileNameList.Items)
                    {
                        userListBoxItem = item as UserListBoxItem;
                        string path = userListBoxItem.Value;

                        if (File.Exists(path))
                        {
                            string fileType = path.Substring(path.LastIndexOf('.'), path.Length - path.LastIndexOf('.'));
                            string fileName = userListBoxItem.Display;

                            Stream stream = File.Open(path, FileMode.Open, FileAccess.Read);
                            byte[] data = new byte[stream.Length];
                            stream.Read(data, 0, data.Length);
                            stream.Seek(0, SeekOrigin.Begin);

                            fileTotalLength += stream.Length;

                            ToolingManWPF.StorageManageServiceReference.AttachmentType attachType = ToolingManWPF.StorageManageServiceReference.AttachmentType.PICTURE;

                            if (imageFilter.Contains(fileType))
                            {
                                attachType = ToolingManWPF.StorageManageServiceReference.AttachmentType.PICTURE;
                            }
                            else if (documentFilter.Contains(fileType))
                            {
                                attachType = ToolingManWPF.StorageManageServiceReference.AttachmentType.DOCUMENT;
                            }
                            files.Add(new FileUP() { Name = fileName, FileType = fileType, Type = attachType, Data = data });
                        }
                    }
                  
                }

                if (fileTotalLength < long.Parse((new ConfigUtil("MAXFILELENGTH")).Get("MAXLENGTH")))
                {
                    Message msg = new Message();
                    switch ((ToolingManWPF.MoldPartInfoServiceReference.ReportType)int.Parse(MaintainTypeCB.SelectedValue.ToString()))
                    {
                        case ToolingManWPF.MoldPartInfoServiceReference.ReportType.MaintainReport:
                           // msg = client.MoldMaintain(MoldNRTB.Text, OperatorNRTB.Text, files, (bool)MoldStateCheckBox.IsChecked);
                            msg = client.MoldMaintain(MoldNRTB.Text, OperatorNRTB.Text, files, false);
                            break;
                        case ToolingManWPF.MoldPartInfoServiceReference.ReportType.TestReport:
                            int c = 0;
                            //int.TryParse(CurrentCutTimeTB.Text,out c);
                            msg = client.MoldTest(MoldNRTB.Text, OperatorNRTB.Text, files, c, false);
                            //msg = client.MoldTest(MoldNRTB.Text, OperatorNRTB.Text, files, c, (bool)MoldStateCheckBox.IsChecked);
                            break;
                    }
                  
                    MessageBox.Show(msg.Content);
                }
                else
                    MessageBox.Show("一次上传文件大小不可大于50M", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// 加载选择条件
        /// </summary>
        private void LoadConditions()
        {
            ConditionServiceClient conditionClient = new ConditionServiceClient();

            ConfigUtil config = new ConfigUtil("partgroup");
            // load the select conditions

            List<EnumItem> reportTypes = conditionClient.GetEnumItems(typeof(ToolingManWPF.MoldPartInfoServiceReference.ReportType).ToString());
            MaintainTypeCB.ItemsSource = reportTypes;
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
    }
}
