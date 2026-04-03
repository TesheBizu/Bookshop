using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;


namespace Bookshop
{
    public class ImageHelper
    {
        public static byte[] ImageToByte(Image img)
        {
            if (img == null) return null;

            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap bmp = new Bitmap(img))
                {
                    bmp.Save(ms, img.RawFormat);
                    return ms.ToArray();
                }
            }
        }


        public static Image ByteToImage(byte[] data)
        {
            if (data == null || data.Length == 0) return null;
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }

    }
}
