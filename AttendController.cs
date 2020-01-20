using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Attendance.Models;
using Rotativa;

namespace Attendance.Controllers
{
    public class AttendController : Controller
    {
        string connectionString = @"Data Source=LAPTOP-OQBDNB4B\SQLEXPRESS;Initial Catalog = HRM; Integrated Security = True";
        // GET: Attend
        public ActionResult Index()
        {
            DataTable dtblAttend = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM Attend", sqlcon);
                sqlda.Fill(dtblAttend);
            }
            return View(dtblAttend);
        }

        public ActionResult Print()
        {
            var report = new ActionAsPdf("Index");
            return report;
        }

        // GET: Attend/Create
        public ActionResult Create()
        {

            return View(new Attend());
        }

        // POST: Attend/Create
        [HttpPost]
        public ActionResult Create(Attend attend)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "INSERT INTO Attend VALUES (@Name,@Date,@Attendance)";
                SqlCommand sqlcmd = new SqlCommand(query,sqlcon);
                sqlcmd.Parameters.AddWithValue("@Name", attend.Name);
                sqlcmd.Parameters.AddWithValue("@Date", attend.Date);
                sqlcmd.Parameters.AddWithValue("@Attendance", attend.Attendance);
                sqlcmd.ExecuteNonQuery();
            }
           return RedirectToAction("Index");
        }

        // GET: Attend/Edit/5
        public ActionResult Edit(int id)
        {
            Attend attend = new Attend();
            DataTable dtblAttend = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "SELECT * FROM Attend where EmpID = @EmpID";
                SqlDataAdapter sqlda = new SqlDataAdapter(query, sqlcon);
                sqlda.SelectCommand.Parameters.AddWithValue("EmpID", id);
                sqlda.Fill(dtblAttend);
            }
             if(dtblAttend.Rows.Count == 1)
            {
                attend.EmpID = Convert.ToInt32(dtblAttend.Rows[0][0].ToString());
                attend.Name = (dtblAttend.Rows[0][1].ToString());
                attend.Date = Convert.ToDateTime(dtblAttend.Rows[0][2].ToString());
                attend.Attendance = (dtblAttend.Rows[0][3].ToString());
                return View(attend);
            }
             return RedirectToAction("Index");
        }

        // POST: Attend/Edit/5
        [HttpPost]
        public ActionResult Edit(Attend attend)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "UPDATE Attend SET Name = @Name, Date = @Date, Attendance = @Attendance where EmpID = @EmpID";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@EmpID", attend.EmpID);
                sqlcmd.Parameters.AddWithValue("@Name", attend.Name);
                sqlcmd.Parameters.AddWithValue("@Date", attend.Date);
                sqlcmd.Parameters.AddWithValue("@Attendance", attend.Attendance);
                sqlcmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: Attend/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "DELETE FROM Attend where EmpID = @EmpID";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@EmpID", id);
                sqlcmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}
