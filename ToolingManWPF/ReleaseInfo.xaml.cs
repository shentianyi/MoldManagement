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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToolingManWPF.ConditionServiceReference;
using ToolingManWPF.MoldPartInfoServiceReference;
using ToolingManWPF.Utilities;
using System.IO;
using System.Diagnostics;

namespace ToolingManWPF
{
    /// <summary>
    /// ReleaseInfo.xaml 的交互逻辑
    /// </summary>
    public partial class ReleaseInfo : Page
    {
        private string moldNR = string.Empty;
        /// <summary>
        /// 实例化页面
        /// </summary>
        /// <param name="moldNR">模具号</param>
        public ReleaseInfo(string moldNR)
        {
            this.moldNR = moldNR;

            InitializeComponent();
        }
        /// <summary>
        /// 按钮点击事件-查询放行信息
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>
        private void QueryBtn_Click(object sender, RoutedEventArgs e)
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
                DateTime? startDT = null;
                DateTime? endDT = null;
                startDT = startDateDP.Text.Length == 0 ? startDT : DateTime.Parse(startDateDP.Text);
                endDT = endDateDP.Text.Length == 0 ? endDT : DateTime.Parse(endDateDP.Text);
                List<MoldReleaseInfo> moldReleaseInfos = client.GetMoldReleaseInfoByMoldNR(moldNR, TesterNRTB.Text, startDT, endDT);
                MoldReleaseInfoDG.ItemsSource = moldReleaseInfos;
            }
            AttachmentTB.Inlines.Clear();
        }

        /// <summary>
        /// 选择行改变事件-查询放行信息附加文件
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>
        private void MoldReleaseInfoDG_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (MoldReleaseInfoDG.SelectedItem != null)
            {
                List<Attachment> attach = (MoldReleaseInfoDG.SelectedItem as MoldReleaseInfo).Attach;
                AttachmentTB.Inlines.Clear();

                if (attach != null && attach.Count > 0)
                {
                    foreach (Attachment at in attach)
                    {
                        Hyperlink link = new Hyperlink(new Run(at.Name));

                        link.Click += new RoutedEventHandler(Hyperlink_Click);
                        link.DataContext = at;
                        AttachmentTB.Inlines.Add(link);
                    }
                }
            }
        }

        /// <summary>
        /// 链接点击事件-查看文件
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>
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
                //if (FileUtil.SavaFIle(filepath, data))
                //    MessageBox.Show("文件保存成功,目录为：" + filepath);
                //else
                //    MessageBox.Show("文件保存失败");
            }
            if (File.Exists(filepath))
                Process.Start(filepath);
        }


    }
}
