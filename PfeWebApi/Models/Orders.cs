using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PfeWebApi.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TableId { get; set; }
        public bool State { get; set; }
        public DateTime Date { get; set; }

    }
}