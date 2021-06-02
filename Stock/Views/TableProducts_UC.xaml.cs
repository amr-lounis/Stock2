﻿using Stock.Controllers;
using System;
using System.Windows;
using System.Windows.Input;
using Stock.Interfaces;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using Data.Model;

namespace Stock.Views
{
    public partial class TableProducts_UC : UserControl
    {
        public TableProducts_UC()
        {
            InitializeComponent();
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
        //--------------------------------------------------------------------
        private void event_add(object sender, RoutedEventArgs e)
        {
            v_GridEdit.Visibility = Visibility.Visible;
            v_uc_EditProduct.v_image.Source = ointerface.getImage(0);
            v_uc_EditProduct.ReceiveMessage(this, new product());
        }
        private void event_edit(object sender, RoutedEventArgs e)
        {
            if (myDataGrid.SelectedItem != null)
            {
                v_GridEdit.Visibility = Visibility.Visible;
                var o = myDataGrid.SelectedItem as product;
                v_uc_EditProduct.v_image.Source = ointerface.getImage(o.ID);
                v_uc_EditProduct.ReceiveMessage(this, o);
            }
        }
        private void event_delete(object sender, RoutedEventArgs e)
        {
            if (myDataGrid.SelectedItem != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var o = myDataGrid.SelectedItem as product;
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
        public void ReturnAddEdit(object _sender, dynamic _data)
        {
            try
            {
                var o = _data as product;
                if(o.ID > 0)//edit
                {
                    ointerface.edit(o);
                    var bitMap = v_uc_EditProduct.v_image.Source as BitmapImage;
                    ointerface.setImage(bitMap,o.ID);
                }
                else //add
                {
                    ointerface.add(o);
                    var bitMap = v_uc_EditProduct.v_image.Source as BitmapImage;
                    ointerface.setImage(bitMap, o.ID);
                }
            }
            catch (Exception) { }
            GridRefresh();
        }
        //--------------------------------------------------------------------
        private void event_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (myDataGrid.SelectedItem != null)
            {
                try
                {
                    var id = (myDataGrid.SelectedItem as product).ID;
                    cashRegister.ReturnProduct(this, id);
                }
                catch (Exception) { }
            }
        }
        private void v_btn_OverlayGridCancel(object sender, EventArgs e)
        {
            GridRefresh();
        }
        private void GridRefresh()
        {
            try
            {
                v_GridEdit.Visibility = Visibility.Collapsed;
                myDataGrid.ItemsSource = null;
                string s;
                myDataGrid.ItemsSource = ointerface.search(v_text_search.Text, ref page, out s);
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
                cashRegister = (_sender as CashRegisters_UC);
                v_text_search.Text = _data as string;
            }
            catch (Exception) { }
        }
        #endregion
        //************************************************************************************* Variable
        #region Variable
        ITableProducts ointerface = new TableProducts_CV();
        public static int page = 0;
        #endregion
    }
}

