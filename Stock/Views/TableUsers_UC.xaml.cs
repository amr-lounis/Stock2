using Stock.Controllers;
using Stock.Dataset.Model;
using Stock.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Stock.Views
{
    public partial class TableUsers_UC : UserControl
    {
        public TableUsers_UC()
        {
            InitializeComponent();
            initReceiver();
            v_text_pageNumber.Text = "" + page;
            v_text_search.Text = "";
            GridRefresh();
        }

        //************************************************************************************* event
        #region event
        
        private void v_text_search_changed(object sender, RoutedEventArgs e)
        {
            page = 0;
            GridRefresh();
        }
        private void event_forward(object sender, RoutedEventArgs e)
        {
            page++;
            GridRefresh();
        }
        private void event_backward(object sender, RoutedEventArgs e)
        {
            page--;
            GridRefresh();
        }

        private void event_add(object sender, RoutedEventArgs e)
        {
            v_GridEdit.Visibility = Visibility.Visible;

            dynamic data = new System.Dynamic.ExpandoObject();
            data.mode = "Add";
            data.message = null;
            EditUsers_UC.Send(this, data);// changed
        }
        private void event_edit(object sender, RoutedEventArgs e)
        {
            v_GridEdit.Visibility = Visibility.Visible;

            if (myDataGrid.SelectedItem != null)
            {
                dynamic data = new System.Dynamic.ExpandoObject();
                data.mode = "Edit";
                data.message = myDataGrid.SelectedItem as user;
                EditUsers_UC.Send(this, data);// changed
            }
        }
        private void event_delete(object sender, RoutedEventArgs e)
        {
            if (myDataGrid.SelectedItem != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var o = myDataGrid.SelectedItem as user; // changed
                    try
                    {
                        ointerface.delete(o.ID);
                        MessageBox.Show("Ok delete");
                    }
                    catch (Exception) { MessageBox.Show("Can not delete"); }
                }
                GridRefresh();
            }
        }
        private void event_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (myDataGrid.SelectedItem != null)
            {
                var o = myDataGrid.SelectedItem as user;
                dynamic data = new System.Dynamic.ExpandoObject();
                data.ID = o.ID;
                data.NAME = o.NAME;
                ReturnMessage(this, data);
            }
        }
        private void v_btn_OverlayGridCancel(object sender, EventArgs e)
        {
            GridRefresh();
        }
        private void GridRefresh()
        {
            v_GridEdit.Visibility = Visibility.Collapsed;
            myDataGrid.ItemsSource = null;
            string s;
            myDataGrid.ItemsSource = ointerface.search(v_text_search.Text, ref  page ,out s);
            v_text_pageNumber.Text = s;
        }
        #endregion
        //************************************************************************************* Messanger //dynamic data = new System.Dynamic.ExpandoObject();
        #region Messanger
        void initReceiver() { OnSendMessage = ReceiveMessage; }
        public static void Send(object _sender, dynamic _data) { if (OnSendMessage != null) OnSendMessage(_sender, _data); }
        public void ReturnMessage(object _sender, dynamic _data) { if (OnReturnMessage != null) OnReturnMessage(_sender, _data); }
        public void ReceiveMessage(object _sender, dynamic _data)
        {
            OnReturnMessage = (_sender as CashRegisters_UC).ReturnCustome; //change
        }
        public delegate void delegateSend(object _sender, dynamic _data);
        public static event delegateSend OnSendMessage;

        public delegate void delegateReturn(object _sender, dynamic _data);
        public static event delegateReturn OnReturnMessage;
        //-----------------
        public void ReturnAddEdit(object _sender, dynamic _data)
        {
            GridRefresh();
        }
        #endregion
        //************************************************************************************* Variable
        #region Variable
        ITableUsers ointerface = new TableUsers_CV();
        public static int page = 0;
        #endregion
    }
}
