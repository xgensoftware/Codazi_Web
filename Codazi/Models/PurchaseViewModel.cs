using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using com.Xgensoftware;
using com.Xgensoftware.Core;
using System.ComponentModel.DataAnnotations;

namespace Codazi.Models
{
    public class PurchaseViewModel :BaseViewModel
    {
        #region Member Variables
        
        #endregion

        #region Private Methods
        private string GetTransactionValue(string transaction, string key)
        {
            if (!string.IsNullOrEmpty(transaction))
            {
                string[] keys = transaction.Split('\n');
                string thisVal = "";
                string thisKey = "";
                foreach (string s in keys)
                {
                    string[] bits = s.Split('=');
                    if (bits.Length > 1)
                    {
                        thisVal = bits[1];
                        thisKey = bits[0];
                        if (thisKey.TrimEnd().Equals(key, StringComparison.InvariantCultureIgnoreCase))
                            break;
                    }
                }
                return thisVal;
            }
            return transaction;
        }
        #endregion

        #region Public Properties
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string EmailAddress { get; set; }
        
        public string CompanyName { get; set; }

        public int Quantity { get; set; }

        public string TransactionId { get; set; }

        public string ReceiptID { get; set; }

        public decimal TotalAmount { get; set; }

        public string PurchaseStatus { get; set; }

        public string ItemNumber { get; set; }

        public string SerialNumber { get; set; }

        public string RawTransaction { get; set; }
        #endregion

        public void ParseTransaction(string transaction)
        {
            this.FirstName = GetTransactionValue(transaction, "first_name");
            this.LastName = GetTransactionValue(transaction, "last_name");
            this.EmailAddress = GetTransactionValue(transaction, "payer_email");
            if(!string.IsNullOrEmpty(this.EmailAddress))
                this.EmailAddress = this.EmailAddress.Replace("%40", "@");
            
            this.ReceiptID = GetTransactionValue(transaction, "receipt_id");
            this.Phone = GetTransactionValue(transaction, "contact_phone");
            
            this.PurchaseStatus = GetTransactionValue(transaction, "payment_status");
            this.ItemNumber = GetTransactionValue(transaction, "item_number");
            this.RawTransaction = transaction;

            if (!string.IsNullOrEmpty(transaction))
            {
                this.Quantity = GetTransactionValue(transaction, "quantity").Parse<int>();
                this.TotalAmount = GetTransactionValue(transaction, "payment_gross").Parse<decimal>();
            }
        }
    }
}