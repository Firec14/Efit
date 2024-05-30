using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EfitWeb1.Controllers
{
     public class FilesController : Controller
     {
          // GET: Files

          public ActionResult Download(string fileName)
          {
               string filePath = Server.MapPath("~/Content/assets/pdf/") + fileName;
               if (!System.IO.File.Exists(filePath))
               {
                    return HttpNotFound();
               }

               byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
               string fileType = "application/pdf";
               string fileNameToDownload = Path.GetFileName(filePath);

               return File(fileBytes, fileType, fileNameToDownload);
          }


     }
}