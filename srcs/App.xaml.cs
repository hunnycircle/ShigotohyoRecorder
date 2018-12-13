using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace sghr
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        public static string ListFileName = System.AppDomain.CurrentDomain.BaseDirectory+"tasklist.txt";
        public static string LogFileName = System.AppDomain.CurrentDomain.BaseDirectory + "tasklog.txt";
        public static Boolean Init = false;

        public static int AddRecord( string taskname )
        {
            int retval = 0;

            try
            {
                using (StreamWriter fp = new StreamWriter(App.LogFileName, true, Encoding.UTF8))
                {
                    fp.WriteLine(taskname + "\t" + System.DateTime.Now);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                retval = -1;
            }

            return retval ;
        }

        private void AppStartup(object sender, StartupEventArgs e)
        {
            foreach (string arg in e.Args)
            {
                switch (arg)
                {
                    case "/init":
                        Init = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
