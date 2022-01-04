using EmployeePayrollModel;
using EmployeePayrollRepository.Context;
using EmployeePayrollRepository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext context;
        private readonly IConfiguration configuration;

        public UserRepository(UserContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<RegisterModel> Register(RegisterModel register)
        {
            try
            {
                var ValidEmail =await this.context.Users.Where(x => x.Email == register.Email).SingleOrDefaultAsync();
                if (ValidEmail == null)
                {
                    if (register != null)
                    {
                        register.Password = EncodePassword(register.Password);
                        this.context.Users.Add(register);
                        await this.context.SaveChangesAsync();
                    }
                    return ValidEmail;
                }
                return null;
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

        public bool Login(LoginModel logindata)
        {
            try
            { 
                var ValidEmail= this.context.Users.Where(x => x.Email ==logindata.Email).SingleOrDefault();
                logindata.Password = EncodePassword(logindata.Password);
                var ValidPassword = this.context.Users.Where(x => x.Password == logindata.Password).SingleOrDefault();
                if(ValidEmail != null && ValidPassword !=null)
                {
                    return true;
                }
                return false;
            }
            catch(ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}


