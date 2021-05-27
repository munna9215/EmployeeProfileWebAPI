using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeApplication.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace EmployeeApplication.Controllers
{
    public class EmployeeController : ApiController
    {

        public HttpResponseMessage Get()
        {
            string query = @"select EmployeeID, EmployeeName,Department,Convert(varchar(10) ,DateOfJoining , 120) as DateOfJoining ,PhotoFileName from Employee";
            DataTable table = new DataTable();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string post(Employee emp)
        {
            try
            {
                string savequery = @"insert into Employee values(
                '" + emp.EmployeeName + @"',
                '" + emp.Department + @"',
                '" + emp.DateOfJoining + @"',
                '" + emp.PhotoFileName + @"' )";
                DataTable table = new DataTable();
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(savequery, conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Added Sucessfully";
            }
            catch (Exception)
            {
                return "Failed to Add!!";
            }
        }

        public string put(Employee emp)
        {
            try
            {
                string updatequery = @"update Employee set 
            EmployeeName ='" + emp.EmployeeName + @"' ,
            Department ='" + emp.Department + @"' ,
            DateOfJoining ='" + emp.DateOfJoining + @"' ,
            PhotoFileName ='" + emp.PhotoFileName + @"' 
            where EmployeeID = " + emp.EmployeeID + @"";
                DataTable table = new DataTable();
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(updatequery, conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Updated Sucessfully";
            }
            catch (Exception)
            {
                return "Failed to Update!!";
            }
        }

        public string delete(int Id)
        {
            try
            {
                string delquery = @"delete from Employee where EmployeeID= " + Id + @"";
                DataTable table = new DataTable();
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(delquery, conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Deleted Sucessfully";
            }
            catch (Exception)
            {
                return "Failed to Delete!!";
            }
        }

        [Route("api/Employee/GetAllDepartmentNames")]
        [HttpGet]
        public HttpResponseMessage GetAllDepartmentNames()
        {
            string deptquery = @"select DepartmentName from Department";

            DataTable table = new DataTable();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(deptquery, conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("api/Employee/SaveFile")]
        public string SaveFile()
        {
            try
            {
                var httprequest = HttpContext.Current.Request;
                var postedfile = httprequest.Files[0];
                string filename = postedfile.FileName;
                var pysicalpath = HttpContext.Current.Server.MapPath("/Photos/" + filename);
                postedfile.SaveAs(pysicalpath);
                return filename;
                
            }
            catch(Exception)
            {
                return "anonymous.png";
            }
        }
    }
}
