using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PfeWebApi.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string name { get; set; }
        public byte[] image { get; set; }
    }
}