using EmployeePayrollModel;
using EmployeePayrollRepository.Context;
using EmployeePayrollRepository.Interface;
using Experimental.System.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
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
                    register.Password = EncodePassword(register.Password);
                    this.context.Users.Add(register);
                    await this.context.SaveChangesAsync();
                    return register;
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

        public async Task<bool> Login(LoginModel logindata)
        {
            try
            {
                var ValidEmail = await this.context.Users.Where(x => x.Email == logindata.Email).SingleOrDefaultAsync();
                logindata.Password = EncodePassword(logindata.Password);
                var ValidPassword = await this.context.Users.Where(x => x.Password == logindata.Password).SingleOrDefaultAsync();
                if (ValidEmail != null)
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

        public async Task<bool>ResetPassword(ResetModel reset)
        {
            try
            {
                var Email = await this.context.Users.Where(x => x.Email == reset.Email).SingleOrDefaultAsync();
                if (Email != null)
                {
                    Email.Password = EncodePassword(reset.Password);
                    this.context.Update(Email);
                    await this.context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch(ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ForgotPassword(ForgetModel forget)
        {
            try
            {
                var ExistingEmail =await this.context.Users.Where(x => x.Email == forget.Email).SingleOrDefaultAsync();
                if (ExistingEmail != null)
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress(this.configuration["Credentials:Email"]);
                    mail.To.Add(forget.Email);
                    SendMSMQ();
                    mail.Body = RecieveMSMQ();
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(this.configuration["Credentials:Email"], this.configuration["Credentials:Password"]);
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);
                    return true;
                }
                return false;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SendMSMQ()
        {
            MessageQueue msgqueue;
            if (MessageQueue.Exists(@".\Private$\Fundoo"))
            {
                msgqueue = new MessageQueue(@".\Private$\Fundoo");
            }
            else
            {
                msgqueue = MessageQueue.Create(@".\Private$\Fundoo");
            }
            string body = "This is Password reset link.";
            msgqueue.Label = "Mail Body";
            msgqueue.Send(body);
        }

        public string RecieveMSMQ()
        {
            MessageQueue Messagequeue = new MessageQueue(@".\Private$\Fundoo");
            var recievemsg = Messagequeue.Receive();
            recievemsg.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            return recievemsg.Body.ToString();
        }

        public string TokenGeneration(string Email)
        {
            byte[] key = Convert.FromBase64String(this.configuration["SecretKey"]);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                      new Claim(ClaimTypes.Name, Email)}),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

    }
}


