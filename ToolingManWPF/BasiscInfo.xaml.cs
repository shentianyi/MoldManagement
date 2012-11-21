using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using ToolingManWPF.MoldPartInfoServiceReference;
using ToolingManWPF.Utilities;
using System.IO;
using ToolingManWPF.ConditionServiceReference;
using System.Diagnostics;
using ToolingManWPF.Data;
using Microsoft.Win32;
using ToolingManWPF.StorageManageServiceReference;
using System;



namespace ToolingManWPF
{
    /// <summary>
    /// BasiscInfo.xaml 的交互逻辑
    /// </summary>
    public partial class BasiscInfo : Page
    {
        private string moldNR = string.Empty;
        //private string knifeNR = string.Empty;
        //private string connectorNR = string.Empty;
        private OpenFileDialog fileDialog;
        private List<string> documentFilter;
        private List<string> imageFilter;
        private List<string> fileControl;


        public BasiscInfo(string moldNR)
        {
            fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Filter = " 可选文件 |" + FileFilterUtil.GetFiltersString();
            fileControl = new List<string>();

            this.moldNR = moldNR;
            //this.knifeNR = knifeNR;
            //this.connectorNR = connectorNR;

            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(moldNR))
            {
                ConditionServiceClient conditionclient = new ConditionServiceClient();
                if (!conditionclient.MoldExist(moldNR))
                {
                    MessageBox.Show("此磨具不存在");
                    return;
                }
                MoldPartInfoServiceClient client = new MoldPartInfoServiceClient();
                MoldBaseInfo moldBaseInfo = client.GetMoldBaseInfoByNR(moldNR);
                BasicInfoGrid.DataContext = moldBaseInfo;
                List<Attachment> attach = moldBaseInfo.Attach;
                foreach (Attachment at in attach)
                {
                    Hyperlink link = new Hyperlink(new Run(at.Name + "  "));

                    link.Click += new RoutedEventHandler(Hyperlink_Click);
                    link.DataContext = at;
                    AttachmentTB.Inlines.Add(link);
                }
            }
        }
        /// <summary>
        /// change the position of mold
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangePosiBtn_Click(object sender, RoutedEventArgs e)
        {
            MoldMoveStore moldMove = new MoldMoveStore(moldNR,MoldPosiContent.Content.ToString());
            moldMove.ShowDialog();
        }
         
        /// <summary>
        /// the evet of he hyperlink
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Attachment attach = (sender as Hyperlink).DataContext as Attachment;

            ConfigUtil config = new ConfigUtil("FILEPATH");
            string directory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, config.Get("FILEPATH"));
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            string filepath = System.IO.Path.Combine(directory, attach.Path);
            if (!File.Exists(filepath))
            {
                MoldPartInfoServiceClient client = new MoldPartInfoServiceClient();
                byte[] data = client.GetFileByName(attach.Path);
                if (data == null)
                {
                    MessageBox.Show("文件不存在");
                    return;
                }
                FileUtil.SavaFIle(filepath, data);
            }
            if (File.Exists(filepath))
                Process.Start(filepath);
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {

            List< FileUP> files = null;
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
                         ToolingManWPF.StorageManageServiceReference.AttachmentType attachType =  ToolingManWPF.StorageManageServiceReference.AttachmentType.PICTURE;

                        if (imageFilter.Contains(fileType))
                        {
                            attachType =  ToolingManWPF.StorageManageServiceReference.AttachmentType.PICTURE;
                        }
                        else if (documentFilter.Contains(fileType))
                        {
                            attachType =  ToolingManWPF.StorageManageServiceReference.AttachmentType.DOCUMENT;
                        }
                        files.Add(new ToolingManWPF.StorageManageServiceReference.FileUP() { Name = fileName, FileType = fileType, Type = attachType, Data = data });
                    }
                }

            }

            if (fileTotalLength < long.Parse((new ConfigUtil("MAXFILELENGTH")).Get("MAXLENGTH")))
            {
                StorageManageServiceClient client = new StorageManageServiceClient();
                Message msg = new Message();
               msg= client.FileUpLoad(files,moldNR);

                MessageBox.Show(msg.Content);
            }
            else
                MessageBox.Show("一次上传文件大小不可大于50M", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// show file dialog to choose file 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// remove file user choose
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveFileBtn_Click(object sender, RoutedEventArgs e)
        {
            if (FileNameList.SelectedIndex > -1)
                FileNameList.Items.RemoveAt(FileNameList.SelectedIndex);
        }

    }
}
