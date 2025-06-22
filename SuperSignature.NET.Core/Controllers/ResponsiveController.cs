using Microsoft.AspNetCore.Mvc;
using SuperSignature.MVC.Core.Models;

namespace SuperSignature.MVC.Core.Controllers
{
    public class ResponsiveController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Responsive Sign";

            var signModel = new SignatureModel
            {
                SignObject = "ctlSignature",
                SignWidth = 100,
                SignHeight = 100,
                SignBackGroundImage = "",
                SignSmooth = true,
                SignTransparent = true,
                SignBorderStyle = "dashed",
                SignPenColor = "#000000",
                SignPenSize = 2,
                SignBorderColor = "#dcdcdc",
                SignBorderWidth = 2,
                SignRequiredPoints = 50,
                SignPenCursor = "/images/pen.cur",
                SignStartMessage = "Resize browser or mobile orientation",
                SignSuccessMessage = "Cool Signature!",
                SignErrorMessage = "Keep signing...",
                SignBackColor = "transparent",
                SignRefreshImage = "/images/clear.png",
                SignZIndex = 1,
                SignImageScaleFactor = 1.0f
            };

            return View(signModel);
        }

        public ActionResult Bootstrap()
        {
            ViewBag.Title = "Bootstrap Responsive";

            var signModel = new SignatureModel
            {
                SignObject = "ctlSignature",
                SignWidth = 100,
                SignHeight = 100,
                SignBackGroundImage = "",
                SignSmooth = true,
                SignBorderStyle = "solid",
                SignPenColor = "blue",
                SignPenSize = 2,
                SignBorderColor = "#dcdcdc",
                SignBorderWidth = 3,
                SignRequiredPoints = 50,
                SignPenCursor = "/images/pen.cur",
                SignStartMessage = "Resize browser or mobile orientation",
                SignSuccessMessage = "Cool Signature!",
                SignErrorMessage = "Keep signing...",
                SignBackColor = "transparent",
                SignRefreshImage = "/images/clear.png",
                SignZIndex = 1
            };

            return View(signModel);
        }
    }
}