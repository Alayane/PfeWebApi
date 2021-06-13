using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PfeWebApi.Models
{
    public class OrderDetails
    {
        public int OrderId  { get; set; }
        public int ItemId  { get; set; }
        public int Qte  { get; set; }

    }
}