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
using ToolingManWPF.ConditionServiceReference;
using ToolingManWPF.Data;
using ToolingManWPF.MoldPartInfoServiceReference;

namespace ToolingManWPF
{
    /// <summary>
    /// MoldInStore.xaml 的交互逻辑
    /// </summary>
    public partial class MoldInStore : Window
    { 
        /// <summary>
        /// 实例化窗体
        /// </summary>
        private MoldInStore()
        {
            InitializeComponent();
            MoldNRTB.Focus();

        }

        /// <summary>
        /// 实例化窗体
        /// </summary>
        /// <param name="moldNR">模具号</param>
        public MoldInStore(string moldNR)
        {
            InitializeComponent();
            MoldNRTB.Text = moldNR;
            OperatorTB.Focus();
            ShowMoldPosition();
        }

        private static MoldInStore instance = null;

        static readonly object padlock = new object();
        public static MoldInStore Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null) { instance = new MoldInStore(); }
                    return instance;
                }
            }
        }

        /// <summary>
        /// 按钮点击事件-模具入库
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>
        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MoldNRTB.Text) || string.IsNullOrWhiteSpace(OperatorTB.Text) )
            {
                MessageBox.Show("请将 模具号，操作员号 填写完整");
            }
            else
            {
                ConditionServiceClient conditionclient = new ConditionServiceClient();
                BasicMessage bmsg = new BasicMessage();

                if (!conditionclient.MoldExist(MoldNRTB.Text))
                {
                    bmsg.Result = false;
                    bmsg.MsgContent.Add("模具");
                }
                if (OperatorTB.Text.Length != 0 && !conditionclient.EmpExist(OperatorTB.Text))
                {
                    bmsg.Result = false;
                    bmsg.MsgContent.Add("操作员");
                }
                if (bmsg.Result == false)
                {
                    MessageBox.Show(bmsg.MsgText + " 不存在，请重新输入");
                    return;
                }
                StorageManageServiceClient client = new StorageManageServiceClient();
               // Message msg = client.MoldInStore(MoldNRTB.Text, OperatorTB.Text, WarehouseNRTB.Text, PositionNRTB.Text);
                Message msg = client.ReturnMoldInPosition(MoldNRTB.Text, OperatorTB.Text, RemarkTB.Text);
                MessageBoxResult result = MessageBox.Show(msg.Content);
                if (result == MessageBoxResult.OK)
                {
                    this.Close();
                }
               
            }
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

        /// <summary>
        /// 键盘释放事件-聚焦操作员输入框
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>
        private void MoldNRTB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                    OperatorTB.Focus();
        }
        /// <summary>
        /// 键盘释放事件-聚焦备注输入框
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>
        private void OperatorTB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                RemarkTB.Focus();

        }
        /// <summary>
        /// 键盘释放事件-聚焦入库按钮
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>
        private void RemarkTB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                OKBtn.Focus();
        }

        /// <summary>
        /// 光标失焦事件-显示模具位置
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>
        private void MoldNRTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (MoldNRTB.Text.Length > 0)
                ShowMoldPosition();
        }

        /// <summary>
        /// 显示模具位置
        /// </summary>
        private void ShowMoldPosition()
        {
            ConditionServiceClient conditionclient = new ConditionServiceClient();
            BasicMessage bmsg = new BasicMessage();
            if (!conditionclient.MoldExist(MoldNRTB.Text))
            {
                bmsg.Result = false;
                MoldPosiNRTB.Text = string.Empty;
            }
            if (bmsg.Result == false)
            {

                return;
            }
            MoldPartInfoServiceClient moldpartClient = new MoldPartInfoServiceClient();
            MoldPosiNRTB.Text = moldpartClient.GetMoldPositionByNr(MoldNRTB.Text).PositionNR;
        }

    }
}
