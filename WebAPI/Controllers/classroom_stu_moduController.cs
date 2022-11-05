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
    public class classroom_stu_moduController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public classroom_stu_moduController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //Get
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
            select Number, StudentID, StudentName, ModuleID, ModuleName
            from dbo.classroom_stu_modu
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
        public JsonResult Post(classroom_stu_modu stu_Modu)
        {
            string query = @"
                    insert into dbo.classroom_stu_modu (StudentID,StudentName,ModuleID,ModuleName)
                    values
                    ('" + stu_Modu.StudentID + @"',
                    '" + stu_Modu.StudentName + @"',
                    '" + stu_Modu.ModuleID + @"',
                    '" + stu_Modu.ModuleName + @"')
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
        public JsonResult Put(classroom_stu_modu stu_Modu)
        {
            string query = @"
                    update dbo.classroom_stu_modu set
                    StudentID = '" + stu_Modu.StudentID + @"',
                    StudentName = '" + stu_Modu.StudentName + @"',
                    ModuleID = '" + stu_Modu.ModuleID + @"',
                    ModuleName = '" + stu_Modu.ModuleName + @"'
                    where Number = " + stu_Modu.Number + @";
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
                    delete from dbo.classroom_stu_modu
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
