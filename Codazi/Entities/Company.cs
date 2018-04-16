using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using com.Xgensoftware.Core;
using Codazi.Repository;

namespace Codazi.Entities
{
    public class Company : BaseEntity
    {
        #region Member Variables

        private CompanyRepository _rep = new CompanyRepository();

        private string _companyName;

        #endregion

        #region Public Properties
        public string CompanyName { get { return _companyName; } }
        
        #endregion

        #region Constructor
        public Company(string companyName)
        {
            _companyName = companyName;
            _id = -1;
        }

        public Company(System.Data.DataRow dr)
        {
            _id = dr["CompanyId"].Parse<int>();
            _companyName = dr["companyName"].ToString();
        }
        #endregion

        #region Public Methods
        
        #endregion

        #region Override
        
        public override void Fetch()
        {
            base.Fetch();
        }

        #endregion
    }
}