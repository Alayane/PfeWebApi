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
    [RoutePrefix("categories")]
    public class CategoryController : ApiController
    {
        [Route("all")]
        [HttpGet]
        public IHttpActionResult getCategories()
        {
            string message;
            List<Category> categories = new List<Category>();
            SqlCommand cmd = new SqlCommand("select * from category");
            DataTable dt = DataAccess.getData(cmd, out message);
            if (!message.Equals("ok"))
                return BadRequest(message);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    Category c = new Category();
                    c.ID = int.Parse(r[0].ToString());
                    c.name = r[1].ToString();
                    c.image =(byte[])r[2];
                    categories.Add(c);
                }
                return Ok(categories);
            }
            else
                return Ok("empty");
        }
    }
}
