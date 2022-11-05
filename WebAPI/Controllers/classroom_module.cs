using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class classroom_moduleController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public classroom_moduleController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //Get
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
            select Number, ModuleID, ModuleName,
            convert (varchar(10), ExamDate, 120) as ExamDate
            from dbo.classroom_module
            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ClassManagementSystem");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        //Post
        [HttpPost]
        public JsonResult Post(classroom_module module)
        {
            string query = @"
                    insert into dbo.classroom_module (ModuleID,ModuleName,ExamDate)
                    values
                    ('" + module.ModuleID + @"',
                    '" + module.ModuleName + @"',
                    '" + module.ExamDate + @"')
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ClassManagementSystem");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added successfully!");
        }


        //Update
        [HttpPut]
        public JsonResult Put(classroom_module module)
        {
            string query = @"
                    update dbo.classroom_module set
                    ModuleID = '" + module.ModuleID + @"',
                    ModuleName = '" + module.ModuleName + @"',
                    ExamDate = '" + module.ExamDate + @"'
                    where Number = " + module.Number + @";
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ClassManagementSystem");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated successfully!");
        }

        //Since we are sending the id in the url => add id in the root parameter
        //Delete
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    delete from dbo.classroom_module
                    where Number = " + id + @"
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ClassManagementSystem");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted successfully!");
        }
    }
}
