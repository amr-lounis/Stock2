using Stock.Controllers;
using Stock.Dataset.Model;
using Stock.Interfaces;
using System;
using System.Windows;
using System.Windows.Controls;
using Utils;

namespace Stock.Views
{
    public partial class InvoiceValidation_UC : UserControl
    {
        public InvoiceValidation_UC()
        {
            InitializeComponent();
        }
        long idInvoice;
        ITableInvoices ointerface = new TableInvoices_CV();

        private void v_btn_InvoiceValidate(object sender, RoutedEventArgs e)
        {
            cashRegister.ReturnInvoice(this,null);
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
        //************************************************************************************* Messanger
        #region Messanger
        CashRegisters_UC cashRegister;
        public void ReceiveMessage(object _sender, dynamic _data)
        {
            if(_data != null)
            {
                try
                {
                    cashRegister = (_sender as CashRegisters_UC);
                    idInvoice = (long)_data;
                    InitInput(idInvoice);
                }
                catch (Exception) { }
            }
        }
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
