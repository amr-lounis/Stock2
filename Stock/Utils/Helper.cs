using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Serialization;

namespace Stock.Utils
{
    public class Helper
    {
        //-------------------------------------------------------------------------------------------------------------- date and time
        #region date and time
        public static DateTime DateTimeFromString(string _DateTime)
        {
            try
            {
                return DateTime.ParseExact(_DateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }
        public static string DateTimeToString(DateTime _DateTime)
        {
            return _DateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------------- Math
        #region math
        public static double rnd(object _value)
        {
            return Math.Round((double)_value, 2);
        }
        public static double DoubleFromString(string _value)
        {
            try
            {
                return double.Parse(_value);
            }
            catch (Exception)
            {
                return 0f;
            }
        }
        public static long LongFromString(string _value)
        {
            try
            {
                return long.Parse(_value);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int intFromString(string _value)
        {
            try
            {
                return int.Parse(_value);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------------- File
        #region browser file
        public static string browserFile(string p_filter)//"image | *.png;*.jpg;"
        {
            OpenFileDialog mOpenFileDialog = new OpenFileDialog();
            mOpenFileDialog.FileName = "";
            mOpenFileDialog.Filter = p_filter;
            mOpenFileDialog.InitialDirectory = System.Reflection.Assembly.GetEntryAssembly().Location;// default dir
            if (mOpenFileDialog.ShowDialog() == true)
            {
                String path = mOpenFileDialog.FileName;
                return path;
            }
            else{return "";}
        }
        public static string browserFileCreate(string p_filter)
        {
            SaveFileDialog mSaveFileDialog = new SaveFileDialog();
            mSaveFileDialog.FileName = "";
            mSaveFileDialog.Filter = p_filter;
            mSaveFileDialog.InitialDirectory = System.Reflection.Assembly.GetEntryAssembly().Location;// default dir
            if (mSaveFileDialog.ShowDialog() == true)
            {
                String path = mSaveFileDialog.FileName;
                return path;
            }
            else{return "";}
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------------- Image
        #region Image
        public static BitmapImage BitmapImageReadFile(string _path, int new_width, int new_height)
        {
            return BitmapImageFromStream(new FileStream(_path, FileMode.Open), new_width, new_height);
        }
        public static void BitmapImageWriteFile(BitmapImage _bitmapimage, string _path)
        {
            var data = BitmapImage2Bytes(_bitmapimage);
            FileStream fs = new FileStream(_path, FileMode.OpenOrCreate, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(data);
            bw.Close();
            fs.Close();
        }
        public static BitmapImage BitmapImageFromStream(Stream _stream, int _width, int _height)
        {
            var image = new BitmapImage();
            _stream.Position = 0;
            image.BeginInit();
            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = null;
            image.StreamSource = _stream;
            image.DecodePixelWidth = _width;
            image.DecodePixelHeight = _height;
            image.EndInit();
            image.Freeze();
            _stream.Close();
            return image;
        }
        public static byte[] BitmapImage2Bytes(BitmapImage bitmapImage)
        {
            byte[] data;
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
                ms.Close();
            }
            return data;
        }
        #endregion
        //-------------------------------------------------------------------------------------------------------------- object I/O
        #region object I/O
        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }
        public static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }
        #endregion
    }
}
