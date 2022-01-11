using EmployeePayrollManager.Interface;
using EmployeePayrollModel;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ILogger<UserController> logger;
        public UserController(IUserManager manager, ILogger<UserController> logger)
        {
            this.manager = manager;
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
                    return this.Ok(new { Status = true, Message = "RegisterSuccessfull",Data=result });
                }
                else
                {
                    this.logger.LogInformation(register.FirstName + " " + register.LastName + result);
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "RegistrationUnSuccessful"});
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation("Exception occured while using register " + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/login")]
        public async Task<IActionResult> Login([FromBody] LoginModel logindata)
        {
            try
            {
                this.logger.LogInformation(logindata.Email + " " + logindata.Password + " is trying to Login");
                var result =await this.manager.Login(logindata);
                if (result ==true)
                {
                    this.logger.LogInformation(logindata.Email + " " + logindata.Password + result);
                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
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
                    return this.Ok(new{ Status = true, Message = "Login Successfull", Token = tokenString,Data=data });
                }
                else
                {
                    this.logger.LogInformation(logindata.Email + " " + logindata.Password + result);
                    return this.BadRequest(new ResponseModel<string>(){ Status = false, Message = "Login UnSuccessfull" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation("Exception occured while using Logging " + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/reset")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetModel reset)
        {
            try
            {
                this.logger.LogInformation(reset.Email + " " + reset.Password + " is trying to ResetPassword");
                var result =await this.manager.ResetPassword(reset);
                if (result == true)
                {
                    this.logger.LogInformation(reset.Email + " " + reset.Password + result);
                    return this.Ok(new { Status = true, Message = "Password Successfully Changed",Data=result });
                }
                else
                {
                    this.logger.LogInformation(reset.Email + " " + reset.Password + result);
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Password not changed" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation("Exception occured while using ResetPassword " + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/forgetPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgetModel forget)
        {
            try
            {
                this.logger.LogInformation(forget.Email  + " is trying to ForgetPassword");
                var result = await this.manager.ForgotPassword(forget);
                if (result == true)
                {
                    this.logger.LogInformation(forget.Email  + result);
                    return this.Ok(new { Status = true, Message = " Password Link Send Sucessfully",Data=result});
                }
                else
                {
                    this.logger.LogInformation(forget.Email + result);
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Password Link send Unsuccessfully " });
                }

            }
            catch (Exception ex)
            {
                this.logger.LogInformation("Exception occured while using ForgetPassword " + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

    }
}
