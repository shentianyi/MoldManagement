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
using System.Windows.Threading;
using ToolingManWPF.ConditionServiceReference;
using ToolingManWPF.MoldPartInfoServiceReference;

namespace ToolingManWPF
{
    /// <summary>
    /// MoldWarnInfos.xaml 的交互逻辑
    /// </summary>
    public partial class MoldWarnInfos : Window
    {
        private MoldWarnInfos()
        {
            InitializeComponent();
        }
        private static MoldWarnInfos instance = null;

        static readonly object padlock = new object();
        public static MoldWarnInfos Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null) { instance = new MoldWarnInfos(); }
                    return instance;
                }
            }
        }

        private delegate void LoadConditionDelegate();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new LoadConditionDelegate(LoadConditions));
            // LoadConditions();
        }
        /// <summary>
        /// load conditions 
        /// </summary>
        private void LoadConditions()
        {
            ConditionServiceClient conditionClient = new ConditionServiceClient();
            List<EnumItem> warnItems = conditionClient.GetEnumItems(typeof(MoldWarnType).ToString());
            WarnCB.ItemsSource = warnItems;

        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            MoldPartInfoServiceClient client = new MoldPartInfoServiceClient();
            List<MoldWarnInfo> warnInfos=client.GetMoldWarnInfo((MoldWarnType)(int.Parse(WarnCB.SelectedValue.ToString())));
            MoldBaseInfoDG.ItemsSource = warnInfos;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

      
    }
}
