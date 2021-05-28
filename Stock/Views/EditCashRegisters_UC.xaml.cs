using Stock.Controllers;
using Stock.Interfaces;
using Stock.Utils;
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
    public partial class EditCashRegisters_UC : UserControl
    {
        public EditCashRegisters_UC()
        {
            InitializeComponent();
            initReceiver();
            InitInputText("");
            InitInputValue(0);

        }
        string mode = "";
        string table = "";
        string column = "";
        long id ;
        private void v_btn_editOk(object sender, EventArgs e)
        {
            switch (mode)
            {
                case ("Text"):
                    {
                        try
                        {
                            oi_CashRegisters.edit(id, column, v_GridEdit_text.Text);
                            MessageBox.Show("Ok edit text");
                        }
                        catch (Exception) { MessageBox.Show("Can not edit"); }
                        ReturnMessage(this, null);
                    }
                    break;
                case ("Value"):
                    {
                        try
                        {
                            var value = Helper.rnd(v_GridEdit_value.Value);
                            oi_CashRegisters.edit(id, column, value );
                            MessageBox.Show("Ok edit value");
                        }
                        catch (Exception) { MessageBox.Show("Can not edit"); }
                        ReturnMessage(this, null);
                    }
                    break;
                default: break;
            }
            mode = "";
        }
        //************************************************************************************* Messanger //dynamic data = new System.Dynamic.ExpandoObject();
        #region Messanger
        void initReceiver() { OnSendMessage = ReceiveMessage; }
        public static void Send(object _sender, dynamic _data) { if (OnSendMessage != null) OnSendMessage(_sender, _data); }
        public void ReturnMessage(object _sender, dynamic _data) { if (OnReturnMessage != null) OnReturnMessage(_sender, _data); }
        public void ReceiveMessage(object _sender, dynamic _data)
        {
            try
            {
                table = (string) _data.table;
                id = (long)_data.id;
                column = (string)_data.column;
                OnReturnMessage = (_sender as CashRegisters_UC).ReturnEditValue; //change
                if (table.Equals("productsolds"))
                {
                    if (column.Equals("NAME") || column.Equals("DESCRIPTION") ) { InitInputText((string)_data.data); mode = "Text"; }
                    else { InitInputValue((double)_data.data); mode = "Value"; }
                }
            }
            catch (Exception){}
        }
        public delegate void delegateSend(object _sender, dynamic _data);
        public static event delegateSend OnSendMessage;

        public delegate void delegateReturn(object _sender, dynamic _data);
        public static event delegateReturn OnReturnMessage;

        #endregion
        //************************************************************************************* 
        void InitInputText(string _text)
        {
            v_GridEdit_value.Visibility = Visibility.Collapsed;
            v_GridEdit_text.Visibility = Visibility.Visible;
            v_GridEdit_text.Text = _text;
        }
        void InitInputValue(double _value)
        {
            v_GridEdit_value.Visibility = Visibility.Visible;
            v_GridEdit_text.Visibility = Visibility.Collapsed;
            v_GridEdit_value.Value = Helper.rnd( _value);
        }
        ITableCashRegisters oi_CashRegisters = new TableCashRegister_CV();
    }
}
