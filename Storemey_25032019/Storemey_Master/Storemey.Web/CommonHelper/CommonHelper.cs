using Storemey.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace Storemey.Web.CommonHelper
{
    public static class CommonWebHelper
    {

        /// <summary>  
        /// This is a Helper Method For Image Element  
        /// </summary>  
        /// <param name="Source">Provide Source</param>  
        /// <param name="Alt">alt Attribute For That Image</param>  
        /// <param name="Title">Title For the Image</param>  
        /// <param name="Height">Height Attribute (By Default Height will be 128)</param>  
        /// <param name="Width">Width Attribute (By Default Width will be 128)</param>  
        /// <returns></returns>  
        /// 


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
            EncoderParameter encoderParameter = new EncoderParameter(encoder, quality);
            encoderParameters.Param[0] = encoderParameter;
            newImage.Save(filePath, imageCodecInfo, encoderParameters);
        }

        /// <summary>
        /// Method to get encoder infor for given image format.
        /// </summary>
        /// <param name="format">Image format</param>
        /// <returns>image codec info.</returns>
        private static ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }



        public static string img(string Source, string Alt, string Title, int Height = 128, int Width = 128)
        {
            return String.Format("<img src='{0}' alt='{1}' title='{2}' height='{3}' width='{4}'/>", Source, Alt, Title, Height, Width);
        }

        /// <summary>  
        /// This is a Helper Method For Image Element  
        /// </summary> 
        /// 

        public static string TenancyConnectionString(string HostName)
        {
            string ConnectionString = string.Empty;
            if (!string.IsNullOrEmpty(HostName))
            {
                ConnectionString = ConfigurationManager.AppSettings["dbConnectionString"].Replace("[dbName]", HostName);
            }
            else
            {
                ConnectionString = ConfigurationManager.AppSettings["dbConnectionString"].Replace("[dbName]", "StoremeyMaster");
            }
            return ConnectionString;
        }


        // all params are optional
        public static void ChangeDatabase(
            this StoremeyDbContext source,
            string initialCatalog = "",
            string dataSource = "",
            string userId = "",
            string password = "",
            bool integratedSecuity = true,
            string configConnectionStringName = "",
            string fullConnectionString = "")
        /* this would be used if the
        *  connectionString name varied from 
        *  the base EF class name */
        {
            try
            {



                Database.SetInitializer<StoremeyDbContext>(null);



                // use the const name if it's not null, otherwise
                // using the convention of connection string = EF contextname
                // grab the type name and we're done
                var configNameEf = string.IsNullOrEmpty(configConnectionStringName)
                    ? source.GetType().Name
                    : configConnectionStringName;

                // add a reference to System.Configuration
                //var entityCnxStringBuilder = new EntityConnectionStringBuilder
                //    (fullConnectionString);

                // init the sqlbuilder with the full EF connectionstring cargo
                var sqlCnxStringBuilder = new SqlConnectionStringBuilder
                    (fullConnectionString);

                // only populate parameters with values if added
                if (!string.IsNullOrEmpty(initialCatalog))
                    sqlCnxStringBuilder.InitialCatalog = initialCatalog;
                if (!string.IsNullOrEmpty(dataSource))
                    sqlCnxStringBuilder.DataSource = dataSource;
                if (!string.IsNullOrEmpty(userId))
                    sqlCnxStringBuilder.UserID = userId;
                if (!string.IsNullOrEmpty(password))
                    sqlCnxStringBuilder.Password = password;

                // set the integrated security status
                sqlCnxStringBuilder.IntegratedSecurity = integratedSecuity;





                // now flip the properties that were changed
                source.Database.Connection.ConnectionString
                    = sqlCnxStringBuilder.ConnectionString;
            }
            catch (Exception ex)
            {
                // set log item if required
            }
        }


        public static void CreateDirectoriesIfMissing(string storename)
        {
            string folderImage = "images", folderupload = "upload", folderexport = "export", folderbackup = "backup", folderROOT = "storeDocuments", backslace = "/", productImage = "productImage", userImages = "userImages", CatergoryImage = "CatergoryImage", storeImage = "storeImage";
            bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath(folderROOT + backslace + storename));
            if (!folderExists)
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folderROOT + backslace + storename));

            folderExists = Directory.Exists(HttpContext.Current.Server.MapPath(folderROOT + backslace + storename + backslace + folderImage));
            if (!folderExists)
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folderROOT + backslace + storename + backslace + folderImage));


            folderExists = Directory.Exists(HttpContext.Current.Server.MapPath(folderROOT + backslace + storename + backslace + folderImage + backslace + productImage));
            if (!folderExists)
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folderROOT + backslace + storename + backslace + folderImage + backslace + productImage));


            folderExists = Directory.Exists(HttpContext.Current.Server.MapPath(folderROOT + backslace + storename + backslace + folderImage + backslace + userImages));
            if (!folderExists)
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folderROOT + backslace + storename + backslace + folderImage + backslace + userImages));

            
            folderExists = Directory.Exists(HttpContext.Current.Server.MapPath(folderROOT + backslace + storename + backslace + folderImage + backslace + CatergoryImage));
            if (!folderExists)
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folderROOT + backslace + storename + backslace + folderImage + backslace + CatergoryImage));


            folderExists = Directory.Exists(HttpContext.Current.Server.MapPath(folderROOT + backslace + storename + backslace + folderImage + backslace + storeImage));
            if (!folderExists)
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folderROOT + backslace + storename + backslace + folderImage + backslace + storeImage));


            folderExists = Directory.Exists(HttpContext.Current.Server.MapPath(folderROOT + backslace + storename + backslace + folderupload));
            if (!folderExists)
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folderROOT + backslace + storename + backslace + folderupload));


            folderExists = Directory.Exists(HttpContext.Current.Server.MapPath(folderROOT + backslace + storename + backslace + folderexport));
            if (!folderExists)
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folderROOT + backslace + storename + backslace + folderexport));


            folderExists = Directory.Exists(HttpContext.Current.Server.MapPath(folderROOT + backslace + storename + backslace + folderbackup));
            if (!folderExists)
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folderROOT + backslace + storename + backslace + folderbackup));
        }

    }
}