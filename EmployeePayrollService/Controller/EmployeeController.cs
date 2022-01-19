using EmployeePayrollManager.Interface;
using EmployeePayrollModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePayrollService.Controller
{
   // [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeManager manager;
        public EmployeeController(IEmployeeManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/addEmployee")]
        public async Task<IActionResult> AddEmployeeDetail([FromBody] EmployeeModel employeeModel)
        {
            try
            {
                var result = await this.manager.AddEmployeeDetail(employeeModel);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "EmployeeDetail Added Sucessfully", Data = result});
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "EmployeeDetail Added UnSucessfully" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        [HttpPut]
        [Route("api/EditEmployee")]
        public async Task<IActionResult> EditEmployeeDetail([FromBody] EmployeeModel employeeModel)
        {
            try
            {
                var result = await this.manager.EditEmployeeDetail(employeeModel);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "EmployeeDetail Edited Sucessfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "EmployeeDetail Edited UnSucessfully" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetEmployeeDetail")]
        public IActionResult GetEmployeeDetail(int EmployeeId)
        {
            try
            {
                IEnumerable<EmployeeModel> result = this.manager.GetEmployeeDetail(EmployeeId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Get EmployeeDetails", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "EmployeeDetails not Get", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployeeDetail(int EmployeeId)
        {
            try
            {
                var result = await this.manager.DeleteEmployeeDetail(EmployeeId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "EmployeeDetail Deleted Sucessfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "EmployeeDetail Not Deleted" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

    }
}
