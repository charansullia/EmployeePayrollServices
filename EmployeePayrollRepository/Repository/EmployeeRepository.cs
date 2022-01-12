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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly UserContext context;
        public EmployeeRepository(UserContext context)
        {
            this.context = context;
        }

        public async Task<EmployeeModel> AddEmployeeDetail(EmployeeModel employeeModel)
        {
            try
            {
                this.context.Emp.Add(employeeModel);
                await this.context.SaveChangesAsync();
                return employeeModel;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EmployeeModel> EditEmployeeDetail(EmployeeModel employeeModel)
        {
            try
            {
                var EmployeeDetail = await this.context.Emp.Where(x => x.EmployeeId == employeeModel.EmployeeId).SingleOrDefaultAsync();
                if (EmployeeDetail != null)
                {
                    EmployeeDetail.NAME = employeeModel.NAME;
                    EmployeeDetail.PROFILE = employeeModel.PROFILE;
                    EmployeeDetail.GENDER = employeeModel.GENDER;
                    EmployeeDetail.DEPARTMENT = employeeModel.DEPARTMENT;
                    EmployeeDetail.SALARY = employeeModel.SALARY;
                    EmployeeDetail.STARTDATE = employeeModel.STARTDATE;
                    this.context.Emp.Update(EmployeeDetail);
                    await this.context.SaveChangesAsync();
                    return employeeModel;
                }
                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
