using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Codazi.Models;
using com.Xgensoftware.Core;
using com.Xgensoftware.Core.Models;

namespace Codazi.Helper
{
    public class PayPalHelper
    {
        #region Member Variables

        private bool _useSandbox = false;
        private string _authToken = string.Empty;
        private string _transactionId = string.Empty;
        

        #endregion

        #region Constructor

        public PayPalHelper()
        {
            _useSandbox = ConfigurationManager.AppSettings["UseSandbox"].Parse<bool>();
            _authToken = _useSandbox ? ConfigurationManager.AppSettings["TestAuthToken"]: ConfigurationManager.AppSettings["AuthorizaToken"];
        }
        #endregion

        #region Private Methods 

        string GetPaypalResponse()
        {
            string paypalUrl = _useSandbox ? "https://www.sandbox.paypal.com/cgi-bin/webscr"
                : "https://www.paypal.com/cgi-bin/webscr";
            string query = "cmd=_notify-synch&tx=" + _transactionId + "&at=" + _authToken;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(paypalUrl);

            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = query.Length;

            string response = string.Empty;
            using (StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), Encoding.ASCII))
            {
                streamOut.Write(query);
                streamOut.Close();
            }

            using (StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                response = streamIn.ReadToEnd();
                streamIn.Close();
            }

            return response;

        }
        
        #endregion

        #region Public Methods 

        public PurchaseViewModel PDTResponse(string transactionId)
        {
            PurchaseViewModel model = new PurchaseViewModel();
            model.TransactionId = transactionId;
           
            _transactionId = transactionId;
            LogHelper.Instance.LogInfoMesage("Receiving Paypal payment....");

            string response = GetPaypalResponse();
            LogHelper.Instance.LogInfoMesage(String.Format("Paypal response: {0}",response));

            if (!string.IsNullOrEmpty(response))
            {
                if (response.StartsWith("SUCCESS"))
                {
                    model.ParseTransaction(response);
                }
            }
            else
            {
                LogHelper.Instance.LogInfoMesage(string.Format("Paypal response error for transactionId {0}", transactionId));
            }

            return model;
        }

        public PurchaseViewModel PDTTestResponse(string transaction)
        {
            PurchaseViewModel model = new PurchaseViewModel();
            model.TransactionId = "TestTrans1234";
            
            LogHelper.Instance.LogInfoMesage("Receiving Paypal payment....");
            LogHelper.Instance.LogInfoMesage(String.Format("Paypal response: {0}", transaction));

            model.ParseTransaction(transaction);

            return model;
        }
        #endregion
    }
}