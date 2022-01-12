﻿using EmployeePayrollManager.Interface;
using EmployeePayrollModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePayrollService.Controller
{
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
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "EmployeeDetail Added Sucessfully", Data = result.ToString() });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "EmployeeDetail Added UnSucessfully" });
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
    }
}
