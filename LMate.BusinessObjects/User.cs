using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LMate.BusinessObjects
{
    public class User
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { set; get; }

        public string Name { set; get; }
        public string IRDNumber { set; get; }
    }
}
