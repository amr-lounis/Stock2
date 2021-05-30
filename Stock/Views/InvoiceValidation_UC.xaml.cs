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
using Utils;

namespace Stock.Views
{
    public partial class InvoiceValidation_UC : UserControl
    {
        public InvoiceValidation_UC()
        {
            InitializeComponent();
            initReceiver();
        }
        long idInvoice;
        ITableInvoices ointerface = new TableInvoices_CV();

        private void v_btn_InvoiceValidate(object sender, RoutedEventArgs e)
        {
            ReturnMessage(this, null);
        }
        private void v_btn_InvoicePrint(object sender, RoutedEventArgs e)
        {

        }
        private void v_btn_InvoicePDF(object sender, RoutedEventArgs e)
        {

        }
        private void v_money_numeric_money_paid_changed(object sender, RoutedEventArgs e)
        {
            var v = (v_money_numeric_total.Value - v_money_numeric_money_paid.Value) ?? 0;
            v_money_numeric_money_unpaid.Value = Math.Round(v,2);
        }
        //************************************************************************************* Messanger //dynamic data = new System.Dynamic.ExpandoObject();
        #region Messanger
        void initReceiver() { OnSendMessage = ReceiveMessage; }
        public static void Send(object _sender, dynamic _data) { if (OnSendMessage != null) OnSendMessage(_sender, _data); }
        public void ReturnMessage(object _sender, dynamic _data) { if (OnReturnMessage != null) OnReturnMessage(_sender, _data); }
        public void ReceiveMessage(object _sender, dynamic _data)
        {
            OnReturnMessage = (_sender as CashRegisters_UC).ReturnInvoiceValidation; //change
            if (_data != null)
            {
                idInvoice = (long)_data;
                InitInput(idInvoice);
            }
        }
        public delegate void delegateSend(object _sender, dynamic _data);
        public static event delegateSend OnSendMessage;

        public delegate void delegateReturn(object _sender, dynamic _data);
        public static event delegateReturn OnReturnMessage;
        #endregion
        public void InitInput(long _id)
        {
            sold_invoice _data = ointerface.get(_id);

            v_label_id.Content = _data.ID;
            Console.WriteLine(_data.MONEY_WITHOUT_ADDEDD);
            v_money_numeric_total.Value = _data.MONEY_TOTAL;
            v_money_numeric_money_paid.Value = _data.MONEY_TOTAL;

            v_text_Description.Text = H_NumberToWord.NumberArabicDAS(_data.MONEY_TOTAL ?? 0);
        }
    }
}
