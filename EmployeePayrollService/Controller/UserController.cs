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
                string message = await this.manager.Register(register);
                if (message == "RegistrationSuccessfull")
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "RegistrationSuccessfull" });
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
    }
}
