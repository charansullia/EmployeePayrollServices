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

        public async Task<RegisterModel> Register(RegisterModel register)
        {
            try
            {
                register.Password = EncodePassword(register.Password);
                return await this.repository.Register(register);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string EncodePassword(string Password)
        {
            try
            {
                byte[] encData_byte = new byte[Password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(Password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("error in Base64Encode" + ex.Message);
            }
        }

        public bool Login(LoginModel logindata)
        {
            try
            {
                logindata.Password = EncodePassword(logindata.Password);
                return this.repository.Login(logindata);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
