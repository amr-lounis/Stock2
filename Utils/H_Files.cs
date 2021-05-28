using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utils
{
    public class H_Files
    {
        //-------------------------------------------------------------------------------------------------------------- File
        #region browser file
        public static string browserFile(string p_filter)//"image | *.png;*.jpg;"
        {
            OpenFileDialog mOpenFileDialog = new OpenFileDialog();
            mOpenFileDialog.FileName = "";
            mOpenFileDialog.Filter = p_filter;
            mOpenFileDialog.InitialDirectory = System.Reflection.Assembly.GetEntryAssembly().Location;// default dir
            if (mOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                String path = mOpenFileDialog.FileName;
                return path;
            }
            else { return ""; }
        }
        public static string browserFileCreate(string p_filter)
        {
            SaveFileDialog mSaveFileDialog = new SaveFileDialog();
            mSaveFileDialog.FileName = "";
            mSaveFileDialog.Filter = p_filter;
            mSaveFileDialog.InitialDirectory = System.Reflection.Assembly.GetEntryAssembly().Location;// default dir
            if (mSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                String path = mSaveFileDialog.FileName;
                return path;
            }
            else { return ""; }
        }
        #endregion
    }
}
