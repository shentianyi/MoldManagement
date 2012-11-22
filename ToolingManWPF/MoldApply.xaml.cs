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
    /// <summary>
    /// MoldApply.xaml 的交互逻辑
    /// </summary>
    public partial class MoldApply : Window
    {
        MoldBaseInfo moldBaseInfo = null;
        /// <summary>
        /// apply mold from search UI
        /// </summary>
        /// <param name="moldNR"></param>
        public MoldApply(MoldBaseInfo baseInfo)
        {
            InitializeComponent();
            MoldNRTB.Text = baseInfo.MoldNR;
            MoldPosiTB.Text = baseInfo.CurrentPosition;
            moldBaseInfo = baseInfo;
            ApplicantNRTB.Focus();
        }

        public MoldApply()
        {
            InitializeComponent();
            ApplicantNRTB.Focus();
        }


        private delegate void LoadMoldUseType();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,new  LoadMoldUseType(LoadMoldUseTypeCondition));
        }

        /// <summary>
        /// apply mold
        /// </summary>
        /// <param name="gsender"></param>
        /// <param name="e"></param>
        private void MoldApplyBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((ToolingManWPF.ConditionServiceReference.MoldStateType)moldBaseInfo.State != ToolingManWPF.ConditionServiceReference.MoldStateType.Normal
                && (MoldUseType)int.Parse(MoldUseWayCB.SelectedValue.ToString()) == MoldUseType.Produce)
            {
                MessageBox.Show("模具目前状态不可用作正产！");
                return;
            }
            if (!string.IsNullOrWhiteSpace(EnsureMoldNRTB.Text) && !string.IsNullOrWhiteSpace(ApplicantNRTB.Text) && !string.IsNullOrWhiteSpace(WorkstationNRTB.Text))
            {
                if (MoldNRTB.Text.Equals(EnsureMoldNRTB.Text))
                {
                    ConditionServiceClient conditionclient = new ConditionServiceClient();
                    BasicMessage bmsg = new BasicMessage();
                    if (!conditionclient.EmpExist(ApplicantNRTB.Text))
                    {
                        bmsg.Result = false;
                        bmsg.MsgContent.Add("申领员工");
                    }
                    if (!conditionclient.WorkstationExist(WorkstationNRTB.Text))
                    {
                        bmsg.Result = false;
                        bmsg.MsgContent.Add("压接机");
                    }
                    if (bmsg.Result == false)
                    {
                        MessageBox.Show(bmsg.MsgText + " 不存在，请重新输入");
                        return;
                    }

                    StorageManageServiceClient client = new StorageManageServiceClient();
                    Message msg = client.ApplyMold((MoldUseType)int.Parse(MoldUseWayCB.SelectedValue.ToString()), MoldNRTB.Text, ApplicantNRTB.Text, "", WorkstationNRTB.Text);
                    MessageBoxResult result = MessageBox.Show(msg.Content);
                    if (result == MessageBoxResult.OK)
                    {
                        this.Close();
                    }
               
                }
                else
                {
                    MessageBox.Show("确认模具号 与 模具号不一致");
                }
            }
            else
            {
                MessageBox.Show("请完整填写 申领员工号,压接机号，确认模具号");
            }
        }

        /// <summary>
        /// initial mold use way for choose
        /// </summary>
        private void LoadMoldUseTypeCondition()
        {
            ConditionServiceClient conditionClient = new ConditionServiceClient();
            List<EnumItem> moldUseTypeItems = conditionClient.GetEnumItems(typeof(MoldUseType).ToString());
            MoldUseWayCB.ItemsSource = moldUseTypeItems;
        }

        private void ApplicantNRTB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                WorkstationNRTB.Focus();
        }

        private void WorkstationNRTB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                EnsureMoldNRTB.Focus();
        }

        private void EnsureMoldNRTB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                MoldApplyBtn.Focus();
        }

    }
}
