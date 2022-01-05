using EmployeePayrollManager.Interface;
using EmployeePayrollModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePayrollService.Controller
{
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;
        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel register)
        {
            try
            {
                var result = await this.manager.Register(register);
                if (result == null)
                {
                    return this.Ok(new ResponseModel<RegisterModel>() { Status = true, Message = "RegisterSuccessfull",Data=result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "RegistrationUnSuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/login")]
        public async Task<IActionResult> Login([FromBody] LoginModel logindata)
        {
            try
            {
                var result =await this.manager.Login(logindata);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<LoginModel>() { Status = true, Message = "Login Successfull",});
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Login UnSuccessfull" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/reset")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetModel reset)
        {
            try
            {
                var result =await this.manager.ResetPassword(reset);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<ResetModel>() { Status = true, Message = "Password Successfully Changed", });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Password not changed" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/forgetPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgetModel forget)
        {
            try
            {
                var result = await this.manager.ForgotPassword(forget);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<ForgetModel>() { Status = true, Message = " Password Link Send Sucessfully" });

                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Password Link send Unsuccessfully " });
                }

            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

    }
}
