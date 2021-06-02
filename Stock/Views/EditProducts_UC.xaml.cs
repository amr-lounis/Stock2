using Data.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using Utils;

namespace Stock.Views
{
    public partial class EditProducts_UC : UserControl
    {
        public EditProducts_UC()
        {
            InitializeComponent();
        }
        //************************************************************************************* Button
        #region Button
        private void v_btn_EditImage(object sender, RoutedEventArgs e)
        {
            try
            {
                var path = H_Files.browserFile("image | *.png;*.jpg;");
                var bitMap = H_Images.BitmapImageReadFile(path, 300, 300);
                v_image.Source = bitMap;
            }
            catch (Exception) { MessageBox.Show("Error"); }
        }
        private void v_btn_DeleteImage(object sender, RoutedEventArgs e)
        {
            v_image.Source = null;
        }
        private void v_btn_Save(object sender, RoutedEventArgs e)
        {
            try
            {
                var o = getInput();
                tableProducts_UC.ReturnAddEdit(this,o);
            }
            catch (Exception) { MessageBox.Show("Error"); }
        }
        #endregion
        //************************************************************************************* Messanger
        #region Messanger
        TableProducts_UC tableProducts_UC;
        public void ReceiveMessage(object _sender, dynamic _data)
        {
            try
            {
                tableProducts_UC = (_sender as TableProducts_UC);
                InitInput(_data as product);
            }
            catch (Exception) {}
        }
        #endregion
        //************************************************************************************* in/out
        #region in/out
        void InitInput(product _product)
        {
            v_text_ID.Content = _product.ID;
            v_text_NAME.Text = _product.NAME ?? "";
            v_text_DESCRIPTION.Text = _product.DESCRIPTION ?? "";
            v_text_CATEGORY.Text = _product.ID_CATEGORY +"";
            v_text_UNITY.Text = _product.ID_UNITE + "";
            v_text_CODE.Text = _product.CODE ?? "";

            v_Numeric_TAX_PERCE.Value = H_Math.rnd( _product.TAX_PERCE ?? 0 );
            v_Numeric_STAMP.Value = H_Math.rnd( _product.STAMP ?? 0 );
            v_Numeric_MONEY_PURCHASE.Value = H_Math.rnd(_product.MONEY_PURCHASE ?? 0 );
            v_Numeric_MONEY_SELLING.Value = H_Math.rnd(_product.MONEY_SELLING ?? 0);
            v_Numeric_MONEY_SELLING_MIN.Value = H_Math.rnd( _product.MONEY_SELLING_MIN ?? 0 );
        }
        //*************************************************************************************  
        product getInput()
        {
            var o = new product();
            o.ID = H_Math.LongFromString(v_text_ID.Content.ToString());
            o.NAME = v_text_NAME.Text;
            o.DESCRIPTION = v_text_DESCRIPTION.Text;
            o.ID_CATEGORY = H_Math.LongFromString(v_text_CATEGORY.Text);
            o.ID_UNITE = H_Math.LongFromString(v_text_UNITY.Text);
            o.CODE = v_text_CODE.Text;

            o.TAX_PERCE = H_Math.rnd( v_Numeric_TAX_PERCE.Value );
            o.STAMP = H_Math.rnd( v_Numeric_STAMP.Value );
            o.MONEY_PURCHASE = H_Math.rnd( v_Numeric_MONEY_PURCHASE.Value) ;
            o.MONEY_SELLING = H_Math.rnd(v_Numeric_MONEY_SELLING.Value );
            o.MONEY_SELLING_MIN = H_Math.rnd( v_Numeric_MONEY_SELLING_MIN.Value );

            o.IMPORTANCE = 0;
            return o;
        }
        #endregion
        //****************************************************************************************************************
    }
}