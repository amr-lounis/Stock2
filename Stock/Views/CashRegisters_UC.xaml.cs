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
    public partial class CashRegisters_UC : UserControl
    {
        long IdInvoice;
        long IdUser;
        long IdCustomer;
        public CashRegisters_UC()
        {
            InitializeComponent();
            v_text_NumericUpDown.Value = 0;

            v_image_customer.Source = new BitmapImage(new Uri("/assets/images/customer.png", UriKind.Relative));
            IdInvoice = oi_Invoice.GetID_NonUsed();
            ViewRefresh();
        }

        //*************************************************************************************  event
        #region event
        //========================================
        private void v_text_search_gotFocus(object sender, EventArgs e)
        {
            v_GridSearchProduct.Visibility = Visibility.Visible;
            TableProducts_UC.Send(this, null);
        }
        private void v_text_search_lostFocus(object sender, EventArgs e)
        {
            //v_GridSearchProduct.Visibility = Visibility.Collapsed;
        }
        private void v_text_search_changed(object sender, EventArgs e)
        {
            if(v_GridSearchProduct.Visibility == Visibility.Visible)
            {
                TableProducts_UC.Send(this, v_text_search.Text);
            }
        }
        private void v_btn_EditCustomer(object sender, EventArgs e)
        {
            v_GridSearchCustomer.Visibility = Visibility.Visible;
            TableUsers_UC.Send(this, null);
        }
        private void v_btn_EditInvoice(object sender, EventArgs e)
        {
            v_GridSearchInvoice.Visibility = Visibility.Visible;
            TableInvoices_UC.Send(this, null);
        }
        private void v_btn_AddNewInvoice(object sender, EventArgs e)
        {
            IdInvoice = oi_Invoice.GetID_NonUsed();
            ViewRefresh();
        }
        private void v_btn_ValidateInvoice(object sender, EventArgs e)
        {
            v_GridInvoiceValidation.Visibility = Visibility.Visible;
            InvoiceValidation_UC.Send(this, string.Format("{0}", IdInvoice));
        }
        private void v_btn_delete(object sender, EventArgs e)
        {
            if (v_GridCashRegister.SelectedItem != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        var o = v_GridCashRegister.SelectedItem as sold_product; // changed
                        oi_CashRegisters.delete(o.ID);
                        MessageBox.Show("Ok delete");
                    }
                    catch (Exception) { MessageBox.Show("Can not delete"); }
                }
                ViewRefresh();
            }
        }
        //========================================
        #region edit
        private void v_btn_EditDescription(object sender, EventArgs e)
        {
            EditWhat = "DESCRIPTION";
            EditValue(EditWhat);
        }
        private void v_btn_EditMoneyOFOne(object sender, EventArgs e)
        {
            EditWhat = "MONEY_ONE";
            EditValue(EditWhat);
        }
        private void v_btn_EditQuantity(object sender, EventArgs e)
        {
            EditWhat = "QUANTITY";
            EditValue(EditWhat);
        }
        private void v_btn_EditTaxPerce(object sender, EventArgs e)
        {
            EditWhat = "TAX_PERCE";
            EditValue(EditWhat);
        }
        private void v_btn_EditStamp(object sender, EventArgs e)
        {
            var EditWhat = "STAMP";
            EditValue(EditWhat);
        }
        private string EditWhat = "";
        private void EditValue(string _column)
        {
            if (v_GridCashRegister.SelectedItem != null)
            {
                v_GridEdit.Visibility = Visibility.Visible;
                var o = v_GridCashRegister.SelectedItem;

                dynamic data = new System.Dynamic.ExpandoObject();
                data.table = "productsolds";
                data.id = (long)o.GetType().GetProperty("ID").GetValue(o, null);
                data.column = _column;
                data.data = o.GetType().GetProperty(_column).GetValue(o, null);
                EditCashRegisters_UC.Send(this, data);
            }
        }
        #endregion
        //========================================
        private void event_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (v_GridCashRegister.SelectedItem != null)
            {

            }
        }
        private void v_btn_OverlayGridCancel(object sender, EventArgs e)
        {
            ViewRefresh();
        }
        #endregion
        //************************************************************************************* Messanger
        #region Messanger
        public void ReturnInvoice(object _sender, dynamic _data)
        {
            IdInvoice = _data.ID;
            ViewRefresh();
        }
        public void ReturnCustome(object _sender, dynamic _data)
        {
            oi_Invoice.edit(IdInvoice, "ID_CUSTOMERS", _data.ID);
            ViewRefresh();
        }
        public void ReturnProduct(object _sender, dynamic _data)
        {
            var o = new sold_product();

            o.ID_INVOICE = IdInvoice;
            o.ID_PRODUCT = _data.ID;
            o.NAME = _data.NAME;
            o.DESCRIPTION = _data.DESCRIPTION;
            o.MONEY_ONE = _data.MONEY_SELLING;
            o.QUANTITY = 1;
            o.TAX_PERCE = _data.TAX_PERCE;
            o.STAMP = _data.STAMP;

            oi_CashRegisters.add(o);
            ViewRefresh();
        }
        public void ReturnInvoiceValidation(object _sender, dynamic _data)
        {
            MessageBox.Show("ReturnInvoiceValidation");
            ViewRefresh();
        }
        public void ReturnEditValue(object _sender, dynamic _data)
        {
            ViewRefresh();
        }
        #endregion
        /**************************************************************/
        private void ViewRefresh()
        {
            v_GridSearchProduct.Visibility = Visibility.Collapsed;
            v_GridSearchCustomer.Visibility = Visibility.Collapsed;
            v_GridSearchInvoice.Visibility = Visibility.Collapsed;
            v_GridInvoiceValidation.Visibility = Visibility.Collapsed;
            v_GridEdit.Visibility = Visibility.Collapsed;

            var invoice = oi_Invoice.get(IdInvoice);
            IdUser = invoice.ID_USERS ?? 0;
            IdCustomer = invoice.ID_CUSTOMERS ?? 0;

            v_text_InvoiceID.Text = string.Format("{0}", IdInvoice);
            v_text_customer_id.Text = string.Format("{0}", invoice.ID_CUSTOMERS);
            v_text_customer_name.Text = oi_User.get(invoice.ID_CUSTOMERS ?? 0).NAME;

            v_GridCashRegister.ItemsSource = null;
            v_GridCashRegister.ItemsSource = oi_CashRegisters.searchByInvoice(IdInvoice);

            v_text_NumericUpDown.Value = (double)oi_Invoice.get(IdInvoice).MONEY_TOTAL;

            EditWhat = "";
        }
        /**************************************************************/
        ITableCashRegisters oi_CashRegisters = new TableCashRegister_CV();
        ITableInvoices oi_Invoice = new TableInvoices_CV();
        ITableUsers oi_User = new TableUsers_CV();
        /**************************************************************/
    }
}
