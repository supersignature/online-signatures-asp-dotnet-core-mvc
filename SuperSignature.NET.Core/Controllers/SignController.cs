using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperSignature.MVC.Core.Models;

namespace SuperSignature.MVC.Core.Controllers
{
    public class SignController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _contextAccessor;

        public SignController(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor contextAccessor)
        {
            _hostingEnvironment = hostingEnvironment;
            _contextAccessor = contextAccessor;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Default .NET Core Signature";
            return View();
        }

        [HttpPost]
        public ActionResult Save()
        {
            var signData = HttpContext.Request.Form["ctlSignature_data"].ToString();
            var signDataSmooth = HttpContext.Request.Form["ctlSignature_data_smooth"].ToString();

            if (string.IsNullOrEmpty(signData) || string.IsNullOrEmpty(signDataSmooth)) return NotFound();


            var bmpSign = SignUtility.GetSignatureBitmap(signData, signDataSmooth, _contextAccessor, _hostingEnvironment);

            FileContentResult result;

            using (var memStream = new MemoryStream())
            {
                bmpSign.Save(memStream, ImageFormat.Png);
                result = this.File(memStream.GetBuffer(), "image/png");
            }

            return result;

        }


        public ActionResult Multiple()
        {
            ViewBag.Title = "Multiple Instances";
            return View();
        }

        public ActionResult SignPartial()
        {
            ViewBag.Title = "Partial View";

            var signModel = new SignatureModel
            {
                SignObject = "ctlSignature",
                SignWidth = 600,
                SignHeight = 250,
                SignBackGroundImage = "",
                SignSmooth = true,
                SignTransparent = false,
                SignBorderStyle = "dashed",
                SignPenColor = "#000000",
                SignPenSize = 3,
                SignBorderColor = "black",
                SignBorderWidth = 1,
                SignRequiredPoints = 50,
                SignPenCursor = "/images/pen.cur",
                SignStartMessage = "Welcome !!!!",
                SignSuccessMessage = "Wow !!!!",
                SignErrorMessage = "Keep signing !!!!",
                SignBackColor = "yellow",
                SignRefreshImage = "/images/clear.png",
                SignZIndex = 1,
                SignImageScaleFactor = 1.0f
            };

            return View(signModel);
        }

        [HttpPost]
        public JsonResult Multiple([FromBody]ICollection<SignData> signatures)
        {
            foreach (var sign in signatures)
            {
                if (!string.IsNullOrEmpty(sign.Data) && !string.IsNullOrEmpty(sign.Smooth))
                {
                    var bmpSign = SignUtility.GetSignatureBitmap(sign.Data, sign.Smooth, _contextAccessor, _hostingEnvironment);
                }
                // Save, etc...
            }

            return Json(signatures.Count + " signatures were saved!");
        }
    }
}
