using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Utils
{
    public class H_Images
    {
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
        public static string BitmapImage2Base64(BitmapImage _BitmapImage)
        {
            var bmi = BitmapImage2Bytes(_BitmapImage);
            return Convert.ToBase64String(bmi);
        }
        #endregion
    }
}
