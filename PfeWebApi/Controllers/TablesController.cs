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
    [RoutePrefix("tables")]
    public class TablesController : ApiController
    {
        [Route("all")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            List<Tables> tables = new List<Tables>();

            string message;
            string q = "select * from [dbo].[resTables]";
            DataTable dt = DataAccess.getData(new SqlCommand(q), out message);
            if (!message.Equals("ok"))
            {
                return BadRequest();
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        Tables t = new Tables();
                        t.Id = (int)r[0];
                        t.state =(int)r[1];
                        tables.Add(t);
                    }
                    return Ok(tables);
                }
                else
                    return BadRequest("Tables is empty");
            }

        }
        [Route("add/{id}")]
        [HttpPost]
        public IHttpActionResult Post(int id)
        {
            
            string message;
            SqlCommand cmds = new SqlCommand("select * from resTables where tableId=@id");
            cmds.Parameters.AddWithValue("@id",id);
            DataTable dt = DataAccess.getData(cmds,out message);
            if (dt.Rows.Count > 0)
            {
                return BadRequest("Table already exist");
            }
            else
            {
                string q = "insert into resTables (tableId) values(@id)";
                SqlCommand cmd= new SqlCommand(q);
                cmd.Parameters.AddWithValue("@id",id);
                DataAccess.setData(cmd,out message);
                if (!message.Equals("ok"))
                {
                    return BadRequest(message);
                }else
                    return Ok();

            }
        }
        [Route("delete/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            string message;
            string q = "delete from resTables where tableId=@id";
            SqlCommand cmd = new SqlCommand(q);
            cmd.Parameters.AddWithValue("@id", id);
            DataAccess.setData(cmd, out message);
            if (!message.Equals("ok"))
            {
                return BadRequest(message);
            }
            else
                return Ok();
        }
        [Route("take/{id}/{state}")]
        [HttpPost]
        public IHttpActionResult updateTable(int id,int state)
        {
            string message;
            string q = "update resTables set state=@state where tableId=@id";
            SqlCommand cmd = new SqlCommand(q);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@state", state);
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
