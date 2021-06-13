using PfeWebApi.DAL;
using PfeWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PfeWebApi.Controllers
{
    [RoutePrefix("reviews")]
    public class ReviewController : ApiController
    {
        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddNewReview([FromBody]Reviews reviws)
        {
            string message;
            string q = "insert into [dbo].[reviews] values(@cs,@rv)";
            SqlCommand cmd = new SqlCommand(q);
            cmd.Parameters.AddWithValue("@cs", reviws.CustomerId);
            cmd.Parameters.AddWithValue("@rv", reviws.Review);
            DataAccess.setData(cmd, out message);
            if (message.Equals(string.Empty))
            {
                return Ok();
            }
            else
                return BadRequest(message);
        }

        [Route("all")]
        [HttpGet]
        public IHttpActionResult allReview()
        {
            List<Reviews> reviews = new List<Reviews>();

            string message;
            string q = "select * from [dbo].[reviews]";
            DataTable dt = DataAccess.getData(new SqlCommand(q), out message);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    Reviews v = new Reviews();
                    v.Id = (int)r[0];
                    v.CustomerId = (int)r[1];
                    v.Review = r[2].ToString();
                    reviews.Add(v);
                }
                return Ok(reviews);
            }
            else
                return BadRequest("Tables is empty");
        }
    }
}
