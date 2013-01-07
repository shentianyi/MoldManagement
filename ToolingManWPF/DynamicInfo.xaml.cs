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
using ToolingManWPF.Helper;

namespace ToolingManWPF
{
    /// <summary>
    /// DynamicInfo.xaml 的交互逻辑
    /// </summary>
    public partial class DynamicInfo : Page
    {

        private string moldNR;

        /// <summary>
        /// 实例化页面
        /// </summary>
        /// <param name="moldNR">模具号</param>
        public DynamicInfo(string moldNR )
        {
            this.moldNR = moldNR;

            InitializeComponent();
        }

        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>
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
                MoldDynamicInfo moldDynamicInfo = client.GetMoldDynamicInfoByMoldNR(moldNR);
                MoldDynamicInfoGrid.DataContext = moldDynamicInfo;

            }
        }


    }
}
