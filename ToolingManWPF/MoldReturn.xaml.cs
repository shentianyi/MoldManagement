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
using System.Windows.Threading;
using ToolingManWPF.Data;
using ToolingManWPF.MoldPartInfoServiceReference;

namespace ToolingManWPF
{

    public partial class MoldReturn : Window
    {

        /// <summary>
        /// 实例化窗体
        /// </summary>
        /// <param name="moldNr">模具号</param>
        public MoldReturn(string moldNR)
        {
            InitializeComponent();
            MoldNRTB.Text = moldNR;
            ApplicantNRTB.Focus();
        }

        /// <summary>
        /// 实例化窗体
        /// </summary>
        public MoldReturn()
        {
            InitializeComponent();
            MoldNRTB.Focus();
        }

        private delegate void LoadMoldReturnState();
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new LoadMoldReturnState(LoadMoldReturnStateCondition));

        }

        /// <summary>
        /// 加载模具归还状态条件
        /// </summary>
        private void LoadMoldReturnStateCondition()
        {
            ConditionServiceClient conditionClient = new ConditionServiceClient();
            List<EnumItem> moldReturnState = conditionClient.GetEnumItems(typeof(MoldReturnStateType).ToString());
         //  .ItemsSource = moldUseTypeItems;
            MoldSateCB.ItemsSource = moldReturnState;
        }

        /// <summary>
        /// 按钮点击事件-模具归还
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>
        private void ReturnMoldBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(MoldNRTB.Text) && !string.IsNullOrWhiteSpace(ApplicantNRTB.Text))
            {
                ConditionServiceClient conditionclient = new ConditionServiceClient();
                BasicMessage bmsg = new BasicMessage();

                if (!conditionclient.MoldExist(MoldNRTB.Text))
                {
                    bmsg.Result = false;
                    bmsg.MsgContent.Add("模具");
                }
                if (ApplicantNRTB.Text.Length != 0 && !conditionclient.EmpExist(ApplicantNRTB.Text))
                {
                    bmsg.Result = false;
                    bmsg.MsgContent.Add("退料员工");
                }
                if (bmsg.Result == false)
                {
                    MessageBox.Show(bmsg.MsgText + " 不存在，请重新输入");
                    return;
                }


                StorageManageServiceClient client = new StorageManageServiceClient();
                Message msg = client.ReturnMold(MoldNRTB.Text, ApplicantNRTB.Text, "", RemarkTB.Text, (MoldReturnStateType)int.Parse(MoldSateCB.SelectedValue.ToString()));
                MessageBox.Show(msg.Content);
                if (msg.MsgType == MsgType.OK)
                {
                    MoldPosiSP.Visibility = Visibility.Visible;
                    MoldPartInfoServiceClient moldpartClient = new MoldPartInfoServiceClient();
                    MoldPosiNRTB.Text = moldpartClient.GetMoldPositionByNr(MoldNRTB.Text).PositionNR;
                    MoldInStoreBtn.IsEnabled = true;
                }
            }
            else
            {
                MessageBox.Show("请将 模具号，退料员工号 填写完整");
            }
        }
        /// <summary>
        /// 按钮点击事件-模具入库
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>
        private void MoldInStoreBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MoldNRTB.Text) || string.IsNullOrWhiteSpace(ApplicantNRTB.Text))
            {
                MessageBox.Show("请将 模具号，退料员工号 填写完整");
            }
            else
            {
                BasicMessage bmsg = new BasicMessage();
                ConditionServiceClient conditionclient = new ConditionServiceClient();
                if (!conditionclient.MoldExist(MoldNRTB.Text))
                {
                    bmsg.Result = false;
                    bmsg.MsgContent.Add("模具");
                }
                if (ApplicantNRTB.Text.Length != 0 && !conditionclient.EmpExist(ApplicantNRTB.Text))
                {
                    bmsg.Result = false;
                    bmsg.MsgContent.Add("退料员工");
                }
                if (bmsg.Result == false)
                {
                    MessageBox.Show(bmsg.MsgText + " 不存在，请重新输入");
                    return;
                }

                StorageManageServiceClient client = new StorageManageServiceClient();
                // Message msg = client.MoldInStore(MoldNRTB.Text, OperatorTB.Text, WarehouseNRTB.Text, PositionNRTB.Text);
                Message msg = client.ReturnMoldInPosition(MoldNRTB.Text, ApplicantNRTB.Text, RemarkTB.Text);
                MessageBoxResult result = MessageBox.Show(msg.Content);
                if (result == MessageBoxResult.OK)
                {
                    this.Close();
                }
               
                if (msg.MsgType == MsgType.OK)
                    MoldInStoreBtn.IsEnabled = false;
            }
        }

        /// <summary>
        /// 键盘释放事件-聚焦申领员工输入框
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>
        private void MoldNRTB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ApplicantNRTB.Focus();
        }
        /// <summary>
        /// 键盘释放事件-聚焦备注输入框
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>
        private void ApplicantNRTB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                RemarkTB.Focus();
        }
        /// <summary>
        /// 键盘释放事件-聚焦归还按钮
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>
        private void RemarkTB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ReturnMoldBtn.Focus();
        }


    }
}
