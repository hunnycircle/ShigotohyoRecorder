using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

namespace sghr
{
    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class ToolWindow : Window
    {
        public ToolWindow()
        {
            if (!App.Init)
            {
                Left = Properties.Settings.Default.STARTUP_X;
                Top = Properties.Settings.Default.STARTUP_Y;
            }
            InitializeComponent();
        }

        public void WindowClosing(object sender, CancelEventArgs e)
        {
            App.AddRecord(Properties.Resources.STR_LOG_END);

            Properties.Settings.Default.STARTUP_X = Left;
            Properties.Settings.Default.STARTUP_Y = Top;
            Properties.Settings.Default.Save();
        }
        
        public void GridLoaded(object sender, RoutedEventArgs e)
        {
            App.AddRecord(Properties.Resources.STR_LOG_START);

            TaskListInit();
            TaskSelector.SelectedIndex = 0;
        }

        public void TaskSelectorChanged(object sender, RoutedEventArgs e)
        {
            if( App.AddRecord(TaskSelector.SelectedItem.ToString()) != 0 ) {
                Application.Current.Shutdown();
            }
        }

        public void TaskListInit()
        {
            TaskSelector.Items.Clear();
            TaskSelector.Items.Add(Properties.Resources.STR_REC_PAUSE);

            try
            {
                using (StreamReader fp = new StreamReader(App.ListFileName))
                {
                    while (fp.Peek() >= 0)
                    {
                        TaskSelector.Items.Add(fp.ReadLine());
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Application.Current.Shutdown();
            }
        }
    }
}
