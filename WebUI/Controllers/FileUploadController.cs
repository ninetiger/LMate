using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebUI.Repositories;
using System.Net;
using System.Net.Http;
using System.Web;

namespace WebUI.Controllers
{
    public class FileUploadController : Controller
    {
                private ReceiptRepository _efReceiptRepository;

        //todo need DI later
        public FileUploadController()
        {
            _efReceiptRepository = new ReceiptRepository();
        }
        //todo need a destructor???

        public FileUploadController(ReceiptRepository efReceiptRepository)
        {
            _efReceiptRepository = efReceiptRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        //knob
        public ActionResult List()
        {
            return View();
        }

        public ActionResult Model()
        {
            return View();
        }

        [HttpPost]
        public void UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file];
                if (hpf.ContentLength == 0)
                    continue;
                string savedFileName = Path.Combine(
                   AppDomain.CurrentDomain.BaseDirectory + "Files",
                   Path.GetFileName(hpf.FileName));
                hpf.SaveAs(savedFileName);


                //// Now we need to wire up a response so that the calling script understands what happened
                //HttpContext.Response.ContentType = "text/plain";
                //var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                //var result = new { name = hpf.FileName };

                //HttpContext.Response.Write(serializer.Serialize(result));
                //HttpContext.Response.StatusCode = 200;

                //// For compatibility with IE's "done" event we need to return a result as well as setting the context.response
                //return new HttpResponseMessage(HttpStatusCode.OK);
            }

            //return null;
        }
    }
}
