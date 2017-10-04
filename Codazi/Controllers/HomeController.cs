using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Codazi.Models;
using com.Xgensoftware.Core;
namespace Codazi.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("About")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Route("Contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Route("Services")]
        public ActionResult Services()
        {
            return View();
        }

        [Route("Contact")]
        [HttpPost]
        public ActionResult Contact(ContactViewModel m)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    StringBuilder s = new StringBuilder();
                    s.Append("A new request for information has been made at codazi.com");
                    s.Append("<br />");
                    s.Append("<br />");
                    s.Append(string.Format("From: {0}", m.Name));
                    s.Append("<br />");
                    s.Append(string.Format("Email Address: {0}", m.Email));
                    s.Append("<br/>");
                    s.Append(string.Format("Comment: {0}", m.Comment));

                    SendMailHelper mailHelper = new SendMailHelper("smtp.gmail.com", 587, "asanfilippo@xgensoftware.com", "3620MichellE");
                    mailHelper.SendEmail("info@xgensoftware.com", "info@Codazi.com", "Request from Codazi.com", s.ToString(), true);

                    return View("Success");
                }
                catch (Exception ex)
                {
                    return PartialView("Error");
                }
            }

            return View();
        }

        [Route("Software")]
        public ActionResult Software()
        {
            return View();
        }
    }
}