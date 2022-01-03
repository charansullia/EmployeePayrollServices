using EmployeePayrollModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollRepository.Interface
{
  public interface IUserRepository
    {
        Task<string> Register(RegisterModel register);
    }
}
