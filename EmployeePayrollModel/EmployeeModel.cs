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
        public virtual RegisterModel user { get; set; }
        public string NAME { get; set; }
        public string PROFILE { get; set; }
        public string GENDER { get; set; }
        public string DEPARTMENT { get; set; }
        public int SALARY { get; set; }
        public int STARTDATE { get; set; }
    }
}
