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
using System.Windows.Navigation;
using ToolingManWPF.Utilities;

namespace ToolingManWPF
{
    /// <summary>
    /// ToolInfo.xaml 的交互逻辑
    /// </summary>
    public partial class ToolInfo : Window
    {

        /// <summary>
        /// navigate from search UI
        /// </summary>
        /// <param name="moldNR"></param>
        public ToolInfo(string moldNR)
        {
            InitializeComponent(); 
            MoldNRTB.Text = moldNR;
        }

        private ToolInfo()
        {
            InitializeComponent();
        }
        private static ToolInfo instance = null;

        static readonly object padlock = new object();
        public static ToolInfo Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null) { instance = new ToolInfo(); }
                    return instance;
                }
            }
        }
     
        /// <summary>
        /// show basic info of mold
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BasicInfoSP_MouseUp(object sender, MouseButtonEventArgs e)
        {
           // ConfigUtil config = new ConfigUtil("partgroup");
            Info_Frame.Navigate(new BasiscInfo(MoldNRTB.Text));
        }

        /// <summary>
        /// show dynamic info of mold
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DynamicInfo_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Info_Frame.Navigate(new DynamicInfo(MoldNRTB.Text));
        }

        /// <summary>
        /// show in and out info of mold
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InOut_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Info_Frame.Navigate(new InOutInfo(MoldNRTB.Text));
        }

   

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

        private void MoldNRTB_KeyUp(object sender, KeyEventArgs e)
        {
            // ConfigUtil config = new ConfigUtil("partgroup");
            if (e.Key == Key.Enter)
                Info_Frame.Navigate(new BasiscInfo(MoldNRTB.Text));
        }

        private void MoldReleaseInfoSP_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Info_Frame.Navigate(new ReleaseInfo(MoldNRTB.Text));
        }

    }
}
