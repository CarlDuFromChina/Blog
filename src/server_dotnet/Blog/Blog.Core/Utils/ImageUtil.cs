using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Blog.Core.Utils
{
    public class ImageUtil
    {
        public static Stream GetThumbnail(string fullFileName)
        {
            //缩小图片
            using (var image = Image.FromFile(fullFileName))
            {
                var width = 150;
                var height = width * image.Height / image.Width;
                var thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);
                var mStream = new MemoryStream();
                thumb.Save(mStream, image.RawFormat);
                return mStream;
            }
        }
    }
}
