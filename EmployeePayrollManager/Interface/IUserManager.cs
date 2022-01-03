using EmployeePayrollModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollManager.Interface
{
   public interface IUserManager
    {
        Task<string> Register(RegisterModel register);
    }
}
