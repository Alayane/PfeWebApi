﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PfeWebApi.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}