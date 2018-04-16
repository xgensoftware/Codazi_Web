using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Codazi.Controllers
{
    public class SoftwareController : Controller
    {
        // GET: Software
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NDDragDrop()
        {
            return View();
        }
        
    }
}