using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Storemey.Web.Controllers
{
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



    }
}