using Stock.Controllers;
using Stock.Dataset.Model;
using Stock.Interfaces;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Stock.Views
{
    public partial class TableInvoices_UC : UserControl
    {
        public TableInvoices_UC()
        {
            InitializeComponent();
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
                try
                {
                    var o = myDataGrid.SelectedItem as sold_invoice;
                    cashRegister.ReturnInvoice(this, o);//change
                }
                catch (Exception) { }
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
            try
            {
                myDataGrid.ItemsSource = null;
                string s;
                myDataGrid.ItemsSource = ointerface.search(v_text_search.Text, getBegin(), getEnd(), ref page, out s);
                v_text_pageNumber.Text = s;
            }
            catch (Exception) { v_text_pageNumber.Text = "ERROR"; }
        }
        #endregion
        //************************************************************************************* Messanger
        #region Messanger
        CashRegisters_UC cashRegister;
        public void ReceiveMessage(object _sender, dynamic _data)
        {
            try
            {
                cashRegister = (_sender as CashRegisters_UC); //change
            }
            catch (Exception) { }
        }
        #endregion
        //************************************************************************************* Variable
        #region Variable
        ITableInvoices ointerface = new TableInvoices_CV();
        public static int page = 0;
        #endregion
    }
}