using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmployeePayrollModel
{
  public class EmployeeModel
    {
        [Key]
        public int EmployeeId { get; set; }
        [ForeignKey("RegisterModel")]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Profile { get; set; }
        public string Gender { get; set; }
        public string Department { get; set; }
        public int Salary { get; set; }
        public int StartDate { get; set; }
    }
}
