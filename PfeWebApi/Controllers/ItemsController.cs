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
    [RoutePrefix("items")]
    public class ItemsController : ApiController
    {
        [Route("all/{category}")]
        [HttpGet]
        public IHttpActionResult getItems(string category)
        {
            string message;
            List<Item> items = new List<Item>();
            SqlCommand cmd = new SqlCommand(@"select i.itemName,i.quantity,i.price,i.image,i.description,i.itemId
                        from items i join category c on c.categoryId = i.category
                        where c.name =@cat");
            cmd.Parameters.AddWithValue("@cat", category);
            DataTable dt = DataAccess.getData(cmd, out message);
            if (!message.Equals("ok"))
                return BadRequest(message);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    Item i = new Item();
                    i.Name = r[0].ToString();
                    i.Qte = r[1].ToString();
                    i.Price = (decimal)r[2];
                    i.Image = (byte[])r[3];
                    i.Des = r[4].ToString();
                    i.Id = int.Parse( r[5].ToString());
                    items.Add(i);
                }
                return Ok(items);
            }
            else
                return Ok("empty");
        }

        public class Item
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Qte { get; set; }
            public string Des { get; set; }
            public decimal Price { get; set; }
            public byte[] Image { get; set; }
        }
    }
}
