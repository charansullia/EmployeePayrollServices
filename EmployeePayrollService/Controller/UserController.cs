using EmployeePayrollManager.Interface;
using EmployeePayrollModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePayrollService.Controller
{
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;
        private readonly IConfiguration configuration;
        private readonly ILogger<UserController> logger;
        public UserController(IUserManager manager, IConfiguration configuration, ILogger<UserController> logger)
        {
            this.manager = manager;
            this.configuration = configuration;
            this.logger = logger;
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel register)
        {
            try
            {
                this.logger.LogInformation(register.FirstName + " " + register.LastName + " is trying to Register");
                var result = await this.manager.Register(register);
                if (result != null)
                {
                    this.logger.LogInformation(register.FirstName + " " + register.LastName + result);
                    return this.Ok(new { Status = true, message = "RegisterSuccessfull", Data = result });
                }
                else
                {
                    this.logger.LogWarning(register.FirstName + " " + register.LastName + result);
                    return this.BadRequest(new { Status = false, message = "RegistrationUnSuccessful" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(register.FirstName + " " + register.LastName );
                return this.NotFound(new { Status = false, message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/login")]
        public async Task<IActionResult> Login([FromBody] LoginModel logindata)
        {
            try
            {
                this.logger.LogInformation(logindata.Email + " " + logindata.Password + "is trying to Login");
                var result = await this.manager.Login(logindata);
                if (result == true)
                {
                    this.logger.LogInformation(logindata.Email + " " + logindata.Password + result);
                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(this.configuration["Connections:Connection"]);
                    IDatabase database = connectionMultiplexer.GetDatabase();
                    string FirstName = database.StringGet("First Name");
                    string LastName = database.StringGet("Last Name");
                    string Email = database.StringGet("Email");
                    int UserId = Convert.ToInt32(database.StringGet("UserId"));

                    RegisterModel data = new RegisterModel
                    {
                        FirstName = FirstName,
                        LastName = LastName,
                        UserId = UserId,
                        Email = Email
                    };
                    string tokenString = this.manager.TokenGeneration(logindata.Email);
                    return this.Ok(new { Status = true, Message = "Login Successfull", Token = tokenString, Data = data });
                }
                else
                {
                    this.logger.LogWarning(logindata.Email + " " + logindata.Password + result);
                    return this.BadRequest(new { Status = false, message = "Login UnSuccessfull" });

                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(logindata.Email + " " + logindata.Password );
                return this.NotFound(new { Status = false, message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/reset")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetModel reset)
        {
            try
            {
                this.logger.LogInformation(reset.Email + " " + reset.Password + "is trying to ResetPassword");
                var result = await this.manager.ResetPassword(reset);
                if (result == true)
                {
                    this.logger.LogInformation(reset.Email + " " + reset.Password + result);
                    return this.Ok(new { Status = true, message = "Password Successfully Changed", Data = result });
                }
                else
                {
                    this.logger.LogWarning(reset.Email + " " + reset.Password + result);
                    return this.BadRequest(new { Status = false, message = "Password not changed" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(reset.Email + " " + reset.Password );
                return this.NotFound(new { Status = false, message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/forgetPassword")]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            try
            {
                this.logger.LogInformation(Email + " " + "is trying to ForgetPassword");
                var result = await this.manager.ForgotPassword(Email);
                if (result == true)
                {
                    this.logger.LogInformation(Email + " " + result);
                    return this.Ok(new { Status = true, message = " Password Link Send Sucessfully", Data = result });
                }
                else
                {
                    this.logger.LogWarning(Email + " " + result);
                    return this.BadRequest(new { Status = false, message = "Password Link send Unsuccessfully " });
                }

            }
            catch (Exception ex)
            {
                this.logger.LogError(Email);
                return this.NotFound(new { Status = false, message = ex.Message });
            }
        }

    }
}
