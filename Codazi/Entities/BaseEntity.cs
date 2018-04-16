using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Codazi.Repository;

namespace Codazi.Entities
{
    public class BaseEntity
    {
        protected int _id;

        public int Id
        {
            get { return _id; }
        }
        

        public virtual void Fetch()
        {
            
        }
    }
}