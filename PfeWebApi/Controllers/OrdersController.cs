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
    [RoutePrefix("orders")]
    public class OrdersController : ApiController
    {
        [Route("all")]
        [HttpGet]
        public IHttpActionResult getAllOrders()
        {
            string message;
            List<Orders> orders = new List<Orders>();
            SqlCommand cmd = new SqlCommand("select * from [dbo].[orders] order by [orderId] desc");
            DataTable dt = DataAccess.getData(cmd,out message);
            if (!message.Equals("ok"))
                return BadRequest(message);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    Orders o = new Orders();
                    o.Id = (int)r[0];
                    o.CustomerId = (int)r[1];
                    o.TableId = (int)r[2];
                    o.State = (bool)r[3];
                    o.Date = (DateTime)r[4];
                    orders.Add(o);
                }
                return Ok(orders);
            }
            else
                return Ok("empty");
        }

        [Route("delete")]
        [HttpDelete]
        public IHttpActionResult deleteAnOrders([FromBody]int id)
        {
            string message;
            string q = "delete from orders where orderId=@id";
            SqlCommand cmd = new SqlCommand(q);
            cmd.Parameters.AddWithValue("@id", id);
            DataAccess.setData(cmd, out message);
            if (!message.Equals("ok"))
            {
                return BadRequest(message);
            }
            else
                return Ok("table have been deleted");
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult addAnOrders([FromBody]Orders orders)
        {
            string message;
            string q = "insert into [dbo].[orders] ([customerId],[tableId]) values(@cs,@tb)";
            SqlCommand cmd = new SqlCommand(q);
            cmd.Parameters.AddWithValue("@cs", orders.CustomerId);
            cmd.Parameters.AddWithValue("@tb", orders.TableId);
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
