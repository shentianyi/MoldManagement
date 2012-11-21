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
using ToolingManWPF.Data;

namespace ToolingManWPF
{
    /// <summary>
    /// InOutInfo.xaml 的交互逻辑
    /// </summary>
    public partial class InOutInfo : Page
    {
        private string moldNR;
        public InOutInfo(string moldNR)
        {
            this.moldNR = moldNR;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void QueryBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(moldNR))
            {
                ConditionServiceClient conditionclient = new ConditionServiceClient();
                BasicMessage msg = new BasicMessage();

                if (!conditionclient.MoldExist(moldNR))
                {
                    msg.Result = false;
                    msg.MsgContent.Add("模具");
                }
                if (applicantNRTB.Text.Length != 0&&!conditionclient.EmpExist(applicantNRTB.Text))
                {
                    msg.Result = false;
                    msg.MsgContent.Add("员工");
                }
                if (msg.Result == false)
                {
                    MessageBox.Show(msg.MsgText+" 不存在，请重新输入");
                    return;
                }
                MoldPartInfoServiceClient client = new MoldPartInfoServiceClient();
                DateTime? startDT = null;
                DateTime? endDT = null;
                startDT = startDateDP.Text.Length == 0 ? startDT : DateTime.Parse(startDateDP.Text);
                endDT = endDateDP.Text.Length == 0 ? endDT : DateTime.Parse(endDateDP.Text);
                List<StorageRecord> storageRecord = client.GetMoldApplyRecords(moldNR, applicantNRTB.Text, startDT, endDT);
                MoldInOutInfoDG.ItemsSource = storageRecord;
            }
        }
        
    }
}
