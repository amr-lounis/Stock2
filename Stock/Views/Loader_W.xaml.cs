using MahApps.Metro.Controls;
using Data.Model;
using Stock.Controllers;
using Stock.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Stock.Views
{
    public partial class Loader_W : MetroWindow
    {
        public Loader_W()
        {
            InitializeComponent();
            SplashScreen splashScreen = new SplashScreen("/assets/images/company.png");
            splashScreen.Show(false);
            loadDatabase();
            nextWindow();
            splashScreen.Close(TimeSpan.FromSeconds(2));
        }
        private void nextWindow()
        {
            Login_W wLogin = new Login_W();
            this.Hide();
            wLogin.Show();
            wLogin.Owner = this.Owner;
            this.Close();
        }
        private void loadDatabase()
        {
            Task t = new Task(delegate ()
            {
                try
                {
                    var _ = Entities.GetInstance();
                }
                catch (Exception e) { Console.WriteLine(e.Message); Console.Beep(); Console.Beep(); }
            }
            );
            t.Start();
        }
    }
}
