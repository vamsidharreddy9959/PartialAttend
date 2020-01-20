using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Attendance.Models
{
    public class Attend
    {
        public int EmpID { get; set; }

        [DisplayName("Employee Name")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public String Attendance { get; set; }

    }
}