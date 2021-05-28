using Stock.Controllers;
using Stock.Dataset.Model;
using Stock.Interfaces;
using System;
using System.Collections.Generic;
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
    public partial class TableInvoices_UC : UserControl
    {
        public TableInvoices_UC()
        {
            InitializeComponent();
            initReceiver();
            v_text_pageNumber.Text = "" + page;
            v_text_search.Text = "";
            ViewRefresh();
        }

        //************************************************************************************* event
        #region event

        private void v_text_search_changed(object sender, RoutedEventArgs e)
        {
            page = 0;
            ViewRefresh();
        }
        private void event_forward(object sender, RoutedEventArgs e)
        {
            page++;
            ViewRefresh();
        }
        private void event_backward(object sender, RoutedEventArgs e)
        {
            page--;
            ViewRefresh();
        }

        private void event_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (myDataGrid.SelectedItem != null)
            {
                var o = myDataGrid.SelectedItem as sold_invoice;
                dynamic data = new System.Dynamic.ExpandoObject();
                data.ID = o.ID;
                OnReturnMessage(this, data);
            }
        }
        private void v_btn_OverlayGridCancel(object sender, EventArgs e)
        {
            ViewRefresh();
        }
        private DateTime getBegin()
        {
            return v_dp_DateBegin.SelectedDate ?? DateTime.MinValue;
        }
        private DateTime getEnd()
        {
            return v_dp_DateBegin.SelectedDate ?? DateTime.MaxValue;
        }
        private void ViewRefresh()
        {
            myDataGrid.ItemsSource = null;
            string s;
            myDataGrid.ItemsSource = ointerface.search(v_text_search.Text, getBegin(), getEnd(), ref page, out s);
            v_text_pageNumber.Text = s;
        }
        #endregion
        //************************************************************************************* Messanger //dynamic data = new System.Dynamic.ExpandoObject();  //if (OnReturnMessage != null) OnReturnMessage(_sender, _data);
        #region Messanger
        void initReceiver() {OnSendMessage = Receiver;  }
        public static void Send(object _sender, dynamic _data){if (OnSendMessage != null) OnSendMessage(_sender, _data);}
        public void Receiver(object _sender, dynamic _data)
        {
            OnReturnMessage = (_sender as CashRegisters_UC).ReturnInvoice;
            ViewRefresh();
        }
        public delegate void delegateSend(object _sender, dynamic _data);
        public static event delegateSend OnSendMessage;

        public delegate void delegateReturn(object _sender, dynamic _data);
        public static event delegateReturn OnReturnMessage;
        #endregion
        //************************************************************************************* Variable
        #region Variable
        ITableInvoices ointerface = new TableInvoices_CV();
        public static int page = 0;
        #endregion
    }
}