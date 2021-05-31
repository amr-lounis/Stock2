using Stock.Dataset.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using Utils;

namespace Stock.Views
{
    public partial class EditUsers_UC : UserControl
    {
        public EditUsers_UC()
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
                tableUsers_UC.ReturnAddEdit(this, o);
            }
            catch (Exception) { MessageBox.Show("Error"); }
        }
        #endregion
        //************************************************************************************* Messanger
        #region Messanger
        TableUsers_UC tableUsers_UC;
        public void ReceiveMessage(object _sender, dynamic _data)
        {
            try
            {
                tableUsers_UC = (_sender as TableUsers_UC);
                InitInput(_data as user);
            }
            catch (Exception) { }
        }
        #endregion
        //*************************************************************************************  in/out
        #region in/out
        void InitInput(user _user)
        {
            v_text_ID.Content = _user.ID;
            v_text_NAME.Text = _user.NAME ?? "";
            v_text_GENDER.Text = _user.GENDER ?? "";
            v_password_1.Password = _user.PASSWORD ?? "";
            v_password_2.Password = _user.PASSWORD ?? "";
            v_text_ROLE.Text = _user.ID_ROLE+"";
            v_text_ACTIVITY.Text = _user.ACTIVITY ?? "";
            v_text_DESCRIPTION.Text = _user.DESCRIPTION ?? "";
            v_text_NRC.Text = _user.NRC ?? "";
            v_text_NIF.Text = _user.NIF ?? "";
            v_text_ADDRESS.Text = _user.ADDRESS ?? "";
            v_text_CITY.Text = _user.CITY ?? "";
            v_text_COUNTRY.Text = _user.COUNTRY ?? "";
            v_text_PHONE.Text = _user.PHONE ?? "";
            v_text_FAX.Text = _user.FAX ?? "";
            v_text_WEBSITE.Text = _user.WEBSITE ?? "";
            v_text_EMAIL.Text = _user.EMAIL ?? "";
        }
        user getInput()
        {
            var o = new user();
            o.ID = H_Math.LongFromString(v_text_ID.Content.ToString());
            o.NAME = v_text_NAME.Text ?? "";
            o.GENDER = v_text_GENDER.Text ?? "";
            o.PASSWORD = v_password_1.Password ?? "";
            o.PASSWORD = v_password_2.Password ?? "";
            o.ID_ROLE = H_Math.LongFromString(v_text_ROLE.Text);
            o.ACTIVITY = v_text_ACTIVITY.Text ?? "";
            o.DESCRIPTION = v_text_DESCRIPTION.Text ?? "";
            o.NRC = v_text_NRC.Text ?? "";
            o.NIF = v_text_NIF.Text ?? "";
            o.ADDRESS = v_text_ADDRESS.Text ?? "";
            o.CITY = v_text_CITY.Text ?? "";
            o.COUNTRY = v_text_COUNTRY.Text ?? "";
            o.PHONE = v_text_PHONE.Text ?? "";
            o.FAX = v_text_FAX.Text ?? "";
            o.WEBSITE = v_text_WEBSITE.Text ?? "";
            o.EMAIL = v_text_EMAIL.Text ?? "";

            return o;
        }
        #endregion
    }
}