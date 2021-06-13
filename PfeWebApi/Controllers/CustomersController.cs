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
        [Route("add")]
        [HttpPost]
        public IHttpActionResult Add([FromBody]Customers customers)
        {
            string message;
            string q = "insert into [dbo].[customers] values(@id,@nm)";
            SqlCommand cmd = new SqlCommand(q);
            cmd.Parameters.AddWithValue("@id", customers.Id);
            cmd.Parameters.AddWithValue("@nm", customers.Name);
            DataAccess.setData(cmd, out message);
            if (!message.Equals("ok"))
            {
                return BadRequest(message);
            }
            else
                return Ok();
        }



    }
}