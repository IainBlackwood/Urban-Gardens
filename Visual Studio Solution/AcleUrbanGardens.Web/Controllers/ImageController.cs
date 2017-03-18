using AcleUrbanGardens.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AcleUrbanGardens.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult UpdateImage(int? id, string name, string imagePath, string cont)
        {
            if(id == null || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(imagePath)  || string.IsNullOrEmpty(cont))
            {
                // return http error
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = new UpdateImageViewModel();
            viewModel.ObjectId = id;
            viewModel.ObjectName = name;
            viewModel.ImagePath = imagePath;
            viewModel.Controller = cont;

            return View(viewModel);
        }
    }
}