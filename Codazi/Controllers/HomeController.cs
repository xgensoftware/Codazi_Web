using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Codazi.Models;
using com.Xgensoftware.Core.Helpers;
using com.Xgensoftware.Core;
using Codazi.Helper;
using Codazi.Entities;


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

                    SendMailHelper mailHelper =
                        new SendMailHelper("smtp.gmail.com", 587, "asanfilippo@xgensoftware.com", "3620MichellE");
                    mailHelper.SendEmail("info@xgensoftware.com", "info@Codazi.com", "Request from Codazi.com",
                        s.ToString(), true);

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

        public ActionResult PurchaseSoftware(string productId)
        {
            switch (productId.ToLower())
            {
                case "dandnd":
                    return PartialView("_PurchaseDragDrop");

                default:
                    return View("Software");
            }
        }

    [Route("Payment")]
        public ActionResult PaymentReceived(string tx)
        {
            PayPalHelper payPal = new PayPalHelper();

#if DEBUG
            PurchaseViewModel model = payPal.PDTTestResponse(tx);
#else
            string amountPaid = Request.QueryString["amt"];
            string orderId = Request.QueryString["cm"];
            PurchaseViewModel model = payPal.PDTResponse(tx);
#endif
            return View(model);
        }
        
        [HttpPost]
        public ActionResult PaymentReceived(PurchaseViewModel pvm)
        {
            if (string.IsNullOrEmpty(pvm.CompanyName))
                return View(pvm);
            else
            {
                pvm.SerialNumber = SerialNumberGenerator.GenerateSN(16, '-', 4);
                Company c = new Company(pvm.CompanyName);
                Contact ct = new Contact(pvm.FirstName, pvm.LastName, pvm.Phone, pvm.EmailAddress);
                Purchase p = new Purchase(pvm.ReceiptID, pvm.TransactionId, pvm.PurchaseStatus, pvm.RawTransaction);
                p.Save(c, ct, pvm.SerialNumber, pvm.Quantity, pvm.ItemNumber);

                return View("PurchaseComplete", pvm);
            }
        }
    }
}