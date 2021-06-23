using PfeWebApi.DAL;
using PfeWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PfeWebApi.Controllers
{
    [RoutePrefix("contact")]
    public class ContactController : ApiController
    {
        [Route("send")]
        [HttpPost]
        public IHttpActionResult contact( [FromBody]Contact contact)
        {
            string message;
            string q = "insert into [dbo].[contact] values(@em,@sb,@ms)";
            SqlCommand cmd = new SqlCommand(q);
            cmd.Parameters.AddWithValue("@em", contact.Email);
            cmd.Parameters.AddWithValue("@sb", contact.Subject);
            cmd.Parameters.AddWithValue("@ms", contact.Message);
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
