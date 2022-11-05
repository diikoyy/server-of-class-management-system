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
    public class classroom_exam_regisController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public classroom_exam_regisController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //Get
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
            select Number, StudentID, StudentName, ModuleID, ModuleName, Attempt
            from dbo.classroom_exam_regis
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
        public JsonResult Post(classroom_exam_regis exam_Regis)
        {
            string query = @"
                    insert into dbo.classroom_exam_regis (StudentID,StudentName,ModuleID,ModuleName,Attempt)
                    values
                    ('" + exam_Regis.StudentID + @"',
                    '" + exam_Regis.StudentName + @"',
                    '" + exam_Regis.ModuleID + @"',
                    '" + exam_Regis.ModuleName + @"',
                    '" + exam_Regis.Attempt + @"')
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
        public JsonResult Put(classroom_exam_regis exam_Regis)
        {
            string query = @"
                    update dbo.classroom_exam_regis set
                    StudentID = '" + exam_Regis.StudentID + @"',
                    StudentName = '" + exam_Regis.StudentName + @"',
                    ModuleID = '" + exam_Regis.ModuleID + @"',
                    ModuleName = '" + exam_Regis.ModuleName + @"',
                    Attempt = '" + exam_Regis.Attempt + @"'
                    where Number = " + exam_Regis.Number + @";
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
                    delete from dbo.classroom_exam_regis
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
