using EmployeePayrollModel;
using EmployeePayrollRepository.Context;
using EmployeePayrollRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollRepository.Repository
{
  public class UserRepository:IUserRepository
    {
        private readonly UserContext context;
        private readonly IConfiguration configuration;
        public UserRepository(UserContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        public async Task<string> Register(RegisterModel register)
        {
            try
            {
                var Data = this.context.Users.Where(x => x.Email == register.Email).SingleOrDefault();
                if (Data == null)
                {
                    register.Password = EncodePassword(register.Password);
                    this.context.Users.Add(register);
                    await this.context.SaveChangesAsync();
                    return "RegistrationSuccessfull";
                }
                return "RegistrationUnSuccessfull";
            }
            catch (ArgumentNullException ex)
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
    }
}
