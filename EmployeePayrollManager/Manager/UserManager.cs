using EmployeePayrollManager.Interface;
using EmployeePayrollModel;
using EmployeePayrollRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollManager.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository repository;
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }
        public async Task<string> Register(RegisterModel register)
        {
            try
            {
                return await this.repository.Register(register);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
