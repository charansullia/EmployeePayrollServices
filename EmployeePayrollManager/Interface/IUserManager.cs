using EmployeePayrollModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollManager.Interface
{
   public interface IUserManager
    {
        Task<RegisterModel> Register(RegisterModel register);
        Task<bool> Login(LoginModel logindata);
        Task<bool> ResetPassword(ResetModel reset);
        Task<bool> ForgotPassword(ForgetModel forget);
    }
}
