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
    [RoutePrefix("OrderDetails")]
    public class OrderDetailsController : ApiController
    {
        [Route("all/{id}")]
        [HttpGet]
        public IHttpActionResult getAllOrders(int id)
        {
            string message;
            List<Od> ods = new List<Od>();
            SqlCommand cmd = new SqlCommand("select i.itemName,od.quantity from orderDetail od join items i on i.itemId=od.itemId where od.orderId = @id");
            cmd.Parameters.AddWithValue("@id", id);
            DataTable dt = DataAccess.getData(cmd, out message);
            if (!message.Equals("ok"))
                return BadRequest(message);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    Od o = new Od();
                    o.name = r[0].ToString();
                    o.qte = int.Parse(r[1].ToString());
                    ods.Add(o);
                }
                return Ok(ods);
            }
            else
                return Ok("empty");
        }

        public class Od
        {
            public string name { get; set; }
            public int qte { get; set; }
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult addOrderDetail([FromBody]OrderDetails orderDetails)
        {
            string message;
            string q = "insert into [dbo].[orderDetail] values(@id,@it,@qt)";
            SqlCommand cmd = new SqlCommand(q);
            cmd.Parameters.AddWithValue("@it", orderDetails.ItemId);
            cmd.Parameters.AddWithValue("@qt", orderDetails.Qte);
            DataAccess.setData(cmd, out message);
            if (!message.Equals("ok"))
            {
                return BadRequest(message);
            }
            else
                return Ok();
        }

        [Route("bill/{id}")]
        [HttpGet]
        public IHttpActionResult getBill(int id)
        {
            string message;
            List<Bill> bills = new List<Bill>();
            SqlCommand cmd = new SqlCommand(
                @"select i.itemName,od.quantity,(od.quantity*i.price)as itemTotal
                ,(select top 1 sum(od.quantity*i.price) as Total
                from orderDetail od join items i on i.itemId=od.itemId 
                where od.orderId=@id
                group by rollup(od.quantity*i.price)
                order by Total desc)as Total from orderDetail od join items i 
                on i.itemId=od.itemId 
                where od.orderId=@id");
            cmd.Parameters.AddWithValue("@id", id);
            DataTable dt = DataAccess.getData(cmd, out message);
            if (!message.Equals("ok"))
                return BadRequest(message);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    Bill bill = new Bill();
                    bill.Item = r[0].ToString();
                    bill.Qte = r[1].ToString();
                    bill.ItemTotal = r[2].ToString();
                    bill.Total = r[3].ToString();
                    bills.Add(bill);
                }
                return Ok(bills);
            }
            else
                return Ok("empty");
        }

        public class Bill
        {
            public string Item { get; set; }
            public string Qte { get; set; }
            public string ItemTotal { get; set; }
            public string Total { get; set; }
        }
    }
}
