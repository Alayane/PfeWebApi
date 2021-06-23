using PfeWebApi.DAL;
using PfeWebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace PfeWebApi.Controllers
{
    [RoutePrefix("customers")]
    public class CustomersController : ApiController
    {
        [Route("add/{customer}")]
        [HttpPost]
        public IHttpActionResult Add(string customer)
        {
            string message;
            string q = "insert into [dbo].[customers] values(@nm);SELECT @@IDENTITY AS id;";
            SqlCommand cmd = new SqlCommand(q);
            cmd.Parameters.AddWithValue("@nm", customer);
            var dt =DataAccess.getData(cmd, out message);
            string orderId = dt.Rows[0]["id"].ToString();
            if (!message.Equals("ok"))
            {
                return BadRequest(message);
            }
            else
                return Ok(orderId);
        }



    }
}