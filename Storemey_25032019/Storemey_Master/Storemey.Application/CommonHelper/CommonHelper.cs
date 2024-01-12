using Storemey.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Storemey.Application
{
    public static class CommonEntityHelper
    {

        public static void Savenewimage(string base64String, string filePath, string imagename, string imageoldname)
        {
            if (!string.IsNullOrEmpty(imageoldname))
            {
                try
                {
                    File.Delete(filePath + imageoldname + "50@50.jpg");
                    File.Delete(filePath + imageoldname + "100@100.jpg");
                    File.Delete(filePath + imageoldname + "320@320.jpg");
                    File.Delete(filePath + imageoldname + "640@640.jpg");
                    File.Delete(filePath + imageoldname + "1080@1080.jpg");
                }
                catch (Exception)
                {

                }

            }

            base64String = Regex.Replace(base64String, "^data:image\\/[a-zA-Z]+;base64,", string.Empty);
            var bitmapimage = Base64StringToBitmap(base64String);

            CommonEntityHelper.Save(bitmapimage, 50, 50, 100, filePath + imagename + "50@50.jpg");
            CommonEntityHelper.Save(bitmapimage, 100, 100, 100, filePath + imagename + "100@100.jpg");
            CommonEntityHelper.Save(bitmapimage, 320, 320, 100, filePath + imagename + "320@320.jpg");
            CommonEntityHelper.Save(bitmapimage, 640, 640, 100, filePath + imagename + "640@640.jpg");
            CommonEntityHelper.Save(bitmapimage, 1080, 1080, 100, filePath + imagename + "1080@1080.jpg");

        }


        public static Bitmap Base64StringToBitmap(this string
                                   base64String)
        {
            Bitmap bmpReturn = null;

            byte[] byteBuffer = Convert.FromBase64String(base64String);
            MemoryStream memoryStream = new MemoryStream(byteBuffer);
            memoryStream.Position = 0;

            bmpReturn = (Bitmap)Bitmap.FromStream(memoryStream);
            memoryStream.Close();
            memoryStream = null;
            byteBuffer = null;
            return bmpReturn;
        }

        public static string ImagetoBase64String(string Path)
        {
            using (Image image = Image.FromFile(Path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }

        public static void Save(Bitmap image, int maxWidth, int maxHeight, int quality, string filePath)
        {

            // Get the image's original width and height
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            // To preserve the aspect ratio
            float ratioX = (float)maxWidth / (float)originalWidth;
            float ratioY = (float)maxHeight / (float)originalHeight;
            float ratio = Math.Min(ratioX, ratioY);

            // New width and height based on aspect ratio
            int newWidth = (int)(originalWidth * ratio);
            int newHeight = (int)(originalHeight * ratio);

            // Convert other formats (including CMYK) to RGB.
            Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);

            // Draws the image in the specified size with quality mode set to HighQuality
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            // Get an ImageCodecInfo object that represents the JPEG codec.
            ImageCodecInfo imageCodecInfo = GetEncoderInfo(ImageFormat.Jpeg);

            // Create an Encoder object for the Quality parameter.
            Encoder encoder = Encoder.Quality;

            // Create an EncoderParameters object. 
            EncoderParameters encoderParameters = new EncoderParameters(1);

            // Save the image as a JPEG file with quality level.
            EncoderParameter encoderParameter = new EncoderParameter(encoder, 100L);
            encoderParameters.Param[0] = encoderParameter;
            //newImage.Save(filePath, imageCodecInfo, encoderParameters);

            string outputFileName = filePath;
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(outputFileName, FileMode.Create, FileAccess.ReadWrite))
                {
                    newImage.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
        }
        private static ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }
    }
}