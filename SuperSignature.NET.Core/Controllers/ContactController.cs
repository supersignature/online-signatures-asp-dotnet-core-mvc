using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperSignature.MVC.Core.Models;

namespace SuperSignature.MVC.Core.Controllers
{
    public class ContactController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _contextAccessor;

        public ContactController(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor contextAccessor)
        {
            _hostingEnvironment = hostingEnvironment;
            _contextAccessor = contextAccessor;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Contact Form";
            return View();
        }

        public ActionResult EditMode()
        {
            ViewBag.Title = "Edit Mode";
            var contact = new ContactModel
            {
                FirstName = "John",
                LastName = "Doe",
                SignatureData = "1,transparent,1,500,300,true,126,ctlSignature; 3,#000000 55,126 55,123 57,120 63,118 70,123 76,148 78,178 81,204 81,223 81,229 81,232 81,224 81,216 85,208 86,201 91,194 95,188 98,183 104,186 111,198 115,204 120,207 125,207 136,207 150,200 164,194 171,188 175,181 176,176 176,172 176,166 176,164 173,164 170,164 169,164 166,167 164,172 164,178 164,183 164,188 164,192 167,194 171,195 173,195 178,193 183,191 188,186 192,180 194,173 197,164 197,145 197,128 197,112 197,109 197,120 197,136 197,149 204,157 213,161 225,163 233,155 243,145 253,132 258,123 258,108 258,96 258,84 256,80 255,80 254,84 252,115 252,136 254,158 260,172 265,176 271,176 273,176 284,170 297,151 305,132 307,124 307,121 307,122 307,130 312,142 318,149 323,152 326,152 332,152 337,148 348,134 350,126 350,116 350,110 344,105 333,103 325,100 319,100 309,110 302,116 300,120 300,124 300,124 301,124 305,124 310,124; 3,#000000 365,32 366,39 372,80 375,108 377,128 385,144 387,152; 3,#000000 397,178 395,180 395,184 397,187 400,187 405,184 409,178 410,172 413,167 412,164 411,163 408,163 405,163 399,163 397,164 395,166;"
            };

            return View(contact);
        }

        public ActionResult BootstrapModal()
        {
            ViewBag.Title = "Bootstrap Modal";

            var signModel = new SignatureModel
            {
                SignObject = "ctlSignature",
                SignWidth = 500,
                SignHeight = 300,
                SignBorderStyle = "dashed",
                SignPenColor = "#000000",
                SignPenSize = 3,
                SignBorderColor = "black",
                SignBorderWidth = 1,
                SignRequiredPoints = 50, // This will govern validity
                SignPenCursor = "/images/pen.cur",
                SignStartMessage = "",
                SignSuccessMessage = "",
                SignErrorMessage = "",
                SignBackColor = "transparent",
                SignRefreshImage = "/images/clear.png",
                SignSmooth = true,
                SignTransparent = true,
                SignZIndex = 1,
                SignImageScaleFactor = 1.0f
            };

            return View(signModel);
        }


        [HttpPost]
        public ActionResult SaveSignature([FromBody]SignData sData)
        {
            if (null == sData)
                return NotFound(); 

            var bmpSign = SignUtility.GetSignatureBitmap(sData.Data, sData.Smooth, _contextAccessor, _hostingEnvironment);

            var fileName = System.Guid.NewGuid() + ".png";
            var filePath = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "Signatures"), fileName);

            bmpSign.Save(filePath, ImageFormat.Png);
          
            return Content(fileName);
        }
    }
}
