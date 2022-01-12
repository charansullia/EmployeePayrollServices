using EmployeePayrollModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollRepository.Interface
{
  public interface IEmployeeRepository
    {
        Task<EmployeeModel> AddEmployeeDetail(EmployeeModel employeemodel);
        Task<EmployeeModel> EditEmployeeDetail(EmployeeModel employeeModel);
    }
}
