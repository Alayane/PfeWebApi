using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PfeWebApi.Models
{
    public class Reviews
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Review { get; set; }
    }
}