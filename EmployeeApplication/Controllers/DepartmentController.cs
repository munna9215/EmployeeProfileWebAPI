using EmployeeApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeApplication.Controllers
{
    public class DepartmentController : ApiController
    {

        public HttpResponseMessage Get()
        {
            string query = @"select DepartmentId, DepartmentName from Department";
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

        public string post(Department dept)
        {
            try
            {
                string savequery = @"insert into Department values('" + dept.DepartmentName + @"')";
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
            catch(Exception ex)
            {
                return "Failed to Add!!";
            }
        }

        public string put(Department dept)
        {
            try
            {
                string updatequery = @"update Department set DepartmentName ='" + dept.DepartmentName + @"'where DepartmentID = "+dept.DepartmentID+@"";
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
            catch (Exception ex)
            {
                return "Failed to Update!!";
            }
        }

        public string delete(int Id)
        {
            try
            {
                string delquery = @"delete from Department where DepartmentID= " + Id +@"";
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
            catch (Exception ex)
            {
                return "Failed to Delete!!";
            }
        }
    }
}
