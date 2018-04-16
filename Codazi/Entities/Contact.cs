using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Codazi.Entities
{
    public class Contact : BaseEntity
    {
        #region Member Variables

        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _phone = string.Empty;
        private string _email = string.Empty;

        #endregion

        #region Public Properties

        public string FirstName { get { return _firstName; } }
        public string LastName { get { return _lastName; } }
        public string Phone { get { return _phone; } }
        public string Email { get { return _email; } }
        #endregion

        public Contact(string fName, string lName, string phone, string email)
        {
            _firstName = fName;
            _lastName = lName;
            _phone = phone;
            _email = email;
            _id = -1;
        }
    }
}