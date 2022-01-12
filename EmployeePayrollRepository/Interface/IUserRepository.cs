using EmployeePayrollModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollRepository.Interface
{
  public interface IUserRepository
    {
        Task<RegisterModel> Register(RegisterModel register);
        Task<bool>Login(LoginModel logindata);
        Task<bool> ResetPassword(ResetModel reset);
        Task<bool> ForgotPassword(string Email);
        string TokenGeneration(string Email);
    }
}
