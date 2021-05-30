using System;
using System.Windows;
using System.Windows.Controls;

namespace Stock.Views
{
    public partial class EditCashRegisters_UC : UserControl
    {
        public EditCashRegisters_UC()
        {
            InitializeComponent();
        }
        private void v_btn_editOk(object sender, EventArgs e)
        {
            try
            {
                if ((data.mode as string).Equals("string"))
                {
                    data.data = v_GridEdit_text.Text;
                    cashRegister.ReturnAddEdit(this, data);
                }
                else if ((data.mode as string).Equals("double"))
                {
                    data.data = v_GridEdit_value.Value;
                    cashRegister.ReturnAddEdit(this, data);
                }
            }
            catch (Exception) { }
        }
        //************************************************************************************* Messanger //dynamic data = new System.Dynamic.ExpandoObject();
        #region Messanger
        CashRegisters_UC cashRegister;
        dynamic data = new System.Dynamic.ExpandoObject();
        public void ReceiveMessage(object _sender, dynamic _data)
        {
            try {
                cashRegister = _sender as CashRegisters_UC;
                data = _data;
                if ( (_data.mode as string).Equals("string"))
                {
                    InitInputText(_data.data);
                }
                else if ( (_data.mode as string).Equals("double") )
                {
                    InitInputValue(_data.data);
                }
            } catch (Exception){ }
        }
        #endregion
        //************************************************************************************* 
        void InitInputText(string _text)
        {
            v_GridEdit_text.Visibility = Visibility.Visible;
            v_GridEdit_text.Text = _text;
            v_GridEdit_value.Visibility = Visibility.Collapsed;
            v_GridEdit_value.Value = 0;
        }
        void InitInputValue(double _value)
        {
            v_GridEdit_value.Visibility = Visibility.Visible;
            v_GridEdit_text.Text = "";
            v_GridEdit_text.Visibility = Visibility.Collapsed;
            v_GridEdit_value.Value = Math.Round( _value,2);
        }
    }
}
