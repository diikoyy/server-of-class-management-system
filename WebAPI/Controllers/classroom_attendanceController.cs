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
    public class classroom_attendanceController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public classroom_attendanceController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //Get
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
            select Number, StudentID, ModuleID, 
            convert (varchar(10), DateOfAttendance,120) as DateOfAttendance 
            from dbo.classroom_attendance
            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ClassManagementSystem");
            SqlDataReader myReader;
            using(SqlConnection myCon = new SqlConnection(sqlDataSource))
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
        public JsonResult Post(classroom_attendance attendance)
        {
            string query = @"
                    insert into dbo.classroom_attendance (StudentID,ModuleID,DateOfAttendance)
                    values
                    ('" + attendance.StudentID + @"',
                    '" + attendance.ModuleID + @"',
                    '" + attendance.DateOfAttendance + @"')
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
        public JsonResult Put(classroom_attendance attendance)
        {
            string query = @"
                    update dbo.classroom_attendance set
                    StudentID = '" + attendance.StudentID + @"',
                    ModuleID = '" + attendance.ModuleID + @"',
                    DateOfAttendance = '" + attendance.DateOfAttendance + @"'
                    where Number = "+attendance.Number+@";
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
                    delete from dbo.classroom_attendance
                    where Number = "+id+@"
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
