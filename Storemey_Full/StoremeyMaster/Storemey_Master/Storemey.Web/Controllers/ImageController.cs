using Storemey.Web.CommonHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Storemey.Web.Controllers
{

    public class parameters
    {
        ///<summary>
        /// Gets or sets Name.
        ///</summary>
        public string image { get; set; }
        //public string image2 { get; set; }

        ///<summary>
        /// Gets or sets DateTime.
        ///</summary>
        public string x1 { get; set; }
        public string y1 { get; set; }
        public string height { get; set; }
        public string width { get; set; }
    }


    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetImage(string id, string foldername, string Size)
        {
            //var dir = Server.MapPath("/Images");
            //var path = Path.Combine(dir, id + ".jpg"); //validate the path for security or use other means to generate the path.
            string path = "/storeDocuments/" + StoremeyConsts.StoreName + "/images/" + foldername + "/" + id + Size + "@" + Size + ".jpg";

            if (System.IO.File.Exists(Server.MapPath(path)))
            {
                return base.File(path, "image/jpeg");
            }
            else
            {
                path = "/images/noimage" + Size + "@" + Size + ".jpg";


            }

            return base.File(path, "image/jpeg");

        }
        public async Task<string> uploadimage()
        {
            var postedFile = HttpContext.Request.Files[0];
            if (postedFile != null)
            {
                string path = System.Web.Hosting.HostingEnvironment.MapPath("/TempImage/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string ext = Path.GetExtension(postedFile.FileName);
                string newfilename = Guid.NewGuid().ToString() + ext;
                postedFile.SaveAs(path + Path.GetFileName(newfilename));

                //_MasterCountriesAppService.ImportFromCSV(postedFile.FileName);

                return newfilename;
            }
            return string.Empty;
        }

        public async Task<string> cropimage(parameters p)
        {

            if (!p.image.Contains("data:image"))
            {
                //var imagesource = Regex.Replace(p.image2, "^data:image\\/[a-zA-Z]+;base64,", string.Empty);
                //var startString = p.image2.Replace(imagesource, "");
                //Bitmap bmp = (Bitmap)Image.FromFile(Server.MapPath("/") + p.image);
                Bitmap bmp = GetCopyImage(Server.MapPath("/") + p.image);


                var result = CommonWebHelper.ResizeImage(bmp, Convert.ToInt32(p.x1), Convert.ToInt32(p.y1), Convert.ToInt32(p.width), Convert.ToInt32(p.height), p.image);

                //Bitmap bImage = result;  // Your Bitmap Image
                //System.IO.MemoryStream ms = new MemoryStream();
                //bImage.Save(ms, ImageFormat.Jpeg);
                //byte[] byteImage = ms.ToArray();
                //var SigBase64 = startString + Convert.ToBase64String(byteImage);
                return result;
            }
            //else
            //{
            //    var imagesource = Regex.Replace(p.image, "^data:image\\/[a-zA-Z]+;base64,", string.Empty);
            //    var startString = p.image.Replace(imagesource, "");
            //    var bitmapimage = CommonWebHelper.Base64StringToBitmap(imagesource);

            //    var result = CommonWebHelper.ResizeImage(bitmapimage, Convert.ToInt32(p.x1), Convert.ToInt32(p.y1), Convert.ToInt32(p.width), Convert.ToInt32(p.height));

            //    Bitmap bImage = result;  // Your Bitmap Image
            //    System.IO.MemoryStream ms = new MemoryStream();
            //    bImage.Save(ms, ImageFormat.Jpeg);
            //    byte[] byteImage = ms.ToArray();
            //    var SigBase64 = startString + Convert.ToBase64String(byteImage);

            //    return SigBase64;
            //}

            return p.image;
        }

        public static Bitmap GetCopyImage(string path)
        {
            using (Bitmap im = (Bitmap)Image.FromFile(path))
            {
                Bitmap bm = new Bitmap(im);
                return bm;
            }
        }

    }
}