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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class classroom_studentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public classroom_studentController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        //Get
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
            select Number, StudentID, StudentName,Major,Intake 
            from dbo.classroom_student
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
        public JsonResult Post(classroom_student student)
        {
            string query = @"
                    insert into dbo.classroom_student (StudentID,StudentName,Major, Intake)
                    values
                    ('" + student.StudentID + @"',
                    '" + student.StudentName + @"',
                    '" + student.Major + @"',
                    '" + student.Intake + @"')
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
        public JsonResult Put(classroom_student student)
        {
            string query = @"
                    update dbo.classroom_student set
                    StudentID = '" + student.StudentID + @"',
                    StudentName = '" + student.StudentName + @"',
                    Major = '" + student.Major + @"',
                    Intake = '" + student.Intake + @"'
                    where Number = " + student.Number + @";
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
                    delete from dbo.classroom_student
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


        /*[Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("Khoi.png");
            }
        }*/

        [Route("GetAllMajors")]
        public JsonResult GetAllMajors()
        {
            string query = @"
            select Major from dbo.classroom_student
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
    }
}
