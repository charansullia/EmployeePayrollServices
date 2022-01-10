using EmployeePayrollModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollManager.Interface
{
  public interface IEmployeeManager
    {
        Task<EmployeeModel> AddEmployeeDetail(EmployeeModel employeeModel);
    }
}
