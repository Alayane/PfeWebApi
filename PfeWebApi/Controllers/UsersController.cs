using PfeWebApi.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PfeWebApi.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        [Route("login")]
        [HttpPost]
        public IHttpActionResult checkLogin([FromBody]User user )
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid!");

            string message;
            string q = "select * from users where Email=@em and password=@pw";
            SqlCommand cmd = new SqlCommand(q);
            cmd.Parameters.AddWithValue("@em", user.Email);
            cmd.Parameters.AddWithValue("@pw", user.Password);
            DataTable dt = DataAccess.getData(cmd,out message);
           
            if (dt.Rows.Count>0)
            {
                return Ok(dt);
            }
            else
            {
                return  BadRequest("Invalid User");
            }

        }


        //[Route("dashboard")]
        //public IHttpActionResult GetUser()
        //{
        //    var id = HttpContext.Current.Session["userId"];
        //    if (id != null)
        //    {
        //        string message;
        //        string q = "select * from users where userId=@id";
        //        SqlCommand cmd = new SqlCommand(q);
        //        cmd.Parameters.AddWithValue("@id",id.ToString());
        //        DataTable dt = DataAccess.getData(cmd, out message);
        //        if (dt.Rows.Count == 0)
        //        {
        //            return BadRequest(message);
        //        }
        //        else
        //        {
        //            return Ok(dt);
        //        }
        //    }
        //    else
        //       return BadRequest();
        //}

        public class User
        {
            [Required(ErrorMessage ="The email is required.")]
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessage = "The password is required.")]
            public string Password { get; set; }

        }
            

    }
}
