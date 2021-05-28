using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Stock.Views
{
    public partial class MainMenu_W : RibbonWindow
    {
        public MainMenu_W()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            try
            {
                InitializeComponent();
                _tabItems = new List<TabItem>();
                tabDynamic.DataContext = _tabItems;

                //Clock();

                v_text_user.Text = "user";
                v_image_user.Source = new BitmapImage(new Uri("/assets/images/user.png", UriKind.Relative));
                v_image_company.SmallImageSource = new BitmapImage(new Uri("/assets/images/user.png", UriKind.Relative));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        //-------------------------------------------------------------------------------
        DispatcherTimer timer = new DispatcherTimer();
        void Clock()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(InvalidateSampleData);
            timer.Start();
        }
        private void InvalidateSampleData(object state, EventArgs e)
        {
            var t = DateTime.Now;
            DigitalTimer.Text = t.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
        }
        //-------------------------------------------------------------------------------
        private void RibbonWin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RibbonWin.SelectedIndex == 0)
            {

            }
       
        }
        
        //-------------------------------------------------------------------------------
        private void tabDynamic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem tab = tabDynamic.SelectedItem as TabItem;
            if (tab == null) return;

            Console.WriteLine("tabDynamic_SelectionChanged : "+tab.Name);
        }
        //-------------------------------------------------------------------------------
        private void AddTabItem(object _content, string _name)
        {
            TabItem tab = new TabItem();
            tab.Header = _name;
            string tab_name = string.Format("tab_" + cpt++);
            Console.WriteLine("add new tab : " + tab_name);
            tab.Name = tab_name;
            tab.HeaderTemplate = tabDynamic.FindResource("TabHeader") as DataTemplate;
            tab.Content = _content;
            _tabItems.Add(tab);
            tabDynamic.DataContext = null;
            tabDynamic.DataContext = _tabItems;
            tabDynamic.SelectedItem = tab;
        }
        //******************************************************************************* Buttons 
        #region Buttons
        public void v_btn_cashRegister(object sender, RoutedEventArgs e)
        {
            CashRegisters_UC o = new CashRegisters_UC();
            AddTabItem(o, "productsold:" + cpt);
        }
        public void v_btn_user(object sender, RoutedEventArgs e)
        {
            TableUsers_UC o = new TableUsers_UC();
            AddTabItem(o, "User:" + cpt);
        }
        public void v_btn_customer(object sender, RoutedEventArgs e)
        {
            TableUsers_UC o = new TableUsers_UC();
            AddTabItem(o, "Customer:" + cpt);
        }
        public void v_btn_provider(object sender, RoutedEventArgs e)
        {
            TableUsers_UC o = new TableUsers_UC();
            AddTabItem(o, "Provider:" + cpt);
        }
        //-------------------------------------------------------------------------------
        public void v_btn_stock(object sender, RoutedEventArgs e)
        {
            TableProducts_UC o = new TableProducts_UC();
            AddTabItem(o, "Stock:" + cpt);
        }
        //-------------------------------------------------------------------------------
        private void v_btn_delete(object sender, RoutedEventArgs e)
        {
            string tabName = (sender as Button).CommandParameter.ToString();
            Console.WriteLine(tabName);
            var item = tabDynamic.Items.Cast<TabItem>().Where(i => i.Name.Equals(tabName)).SingleOrDefault();
            TabItem tab = item as TabItem;

            if (tab != null)
            {
                //if (MessageBox.Show(string.Format("Are you sure you want to close the tab '{0}'?", tab.Header.ToString()),
                //    "Close Tab", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    TabItem selectedTab = tabDynamic.SelectedItem as TabItem;
                    tabDynamic.DataContext = null;

                    _tabItems.Remove(tab);
                    tabDynamic.DataContext = _tabItems;
                }
            }
            if (_tabItems.Count >= 1)
            {
                tabDynamic.SelectedItem = _tabItems[0];
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        #endregion
        //-------------------------------------------------------------------------------
        public List<TabItem> _tabItems ;
        static int cpt = 0;
        //-------------------------------------------------------------------------------
    }
}
