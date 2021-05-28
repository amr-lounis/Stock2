using Microsoft.Win32;
using Stock.Controllers;
using Stock.Dataset.Model;
using Stock.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Utils;
using Image = System.Windows.Controls.Image;

namespace Stock.Views
{
    public partial class EditProducts_UC : UserControl
    {
        public EditProducts_UC()
        {
            InitializeComponent();
            initReceiver();
        }

        //************************************************************************************* Button

        #region Button
        private void v_btn_EditImage(object sender, RoutedEventArgs e)
        {
            var path = H_Files.browserFile("image | *.png;*.jpg;");
            var bitMap = H_Images.BitmapImageReadFile(path, 300, 300);
            v_image.Source = bitMap;
        }
        private void v_btn_DeleteImage(object sender, RoutedEventArgs e)
        {
            v_image.Source = ointerface.getImage(0);
        }
        private void v_btn_Save(object sender, RoutedEventArgs e)
        {
            try
            {
                var o = getInput();
                var bitMap = v_image.Source as BitmapImage;
                if (type.Equals("Add"))
                {
                    ointerface.add(o);
                    ointerface.setImage(bitMap, o.ID);
                    MessageBox.Show("Ok add");
                }
            }
            catch (Exception) { MessageBox.Show("Can not add"); }
            try
            {
                var o = getInput();
                var bitMap = v_image.Source as BitmapImage;
                if (type.Equals("Edit"))
                {
                    ointerface.edit(o);
                    ointerface.setImage(bitMap, o.ID);
                    MessageBox.Show("Ok edit");
                }
            }
            catch (Exception) { MessageBox.Show("Can not edit"); }
            ReturnMessage(this, null);
        }
        #endregion

        //************************************************************************************* variable
        #region variable
        ITableProducts ointerface = new TableProducts_CV();
        string type = "";
        #endregion

        //************************************************************************************* Messanger //dynamic data = new System.Dynamic.ExpandoObject();
        #region Messanger
        void initReceiver() { OnSendMessage = ReceiveMessage; }
        public static void Send(object _sender, dynamic _data) { if (OnSendMessage != null) OnSendMessage(_sender, _data); }
        public void ReturnMessage(object _sender, dynamic _data) { if (OnReturnMessage != null) OnReturnMessage(_sender, _data); }
        public void ReceiveMessage(object _sender, dynamic _data)
        {
            OnReturnMessage = (_sender as TableProducts_UC).ReturnAddEdit; //change
            if (_data.mode != null)
            {
                if (_data.mode.Equals("Add"))
                {
                    type = "Add";
                    InitInput(0);
                }
                else if (_data.mode.Equals("Edit") && (_data.message != null))
                {
                    type = "Edit";
                    var id = (long)_data.message;
                    InitInput(id);
                }
            }
        }
        public delegate void delegateSend(object _sender, dynamic _data);
        public static event delegateSend OnSendMessage;

        public delegate void delegateReturn(object _sender, dynamic _data);
        public static event delegateReturn OnReturnMessage;
        #endregion

        //************************************************************************************* in/out
        #region in/out
        void InitInput(long _id)
        {
            product _product;
            if (_id <= 0) { _product = new product(); }
            else { _product = ointerface.get(_id); }

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

            v_image.Source = ointerface.getImage(_product.ID);
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