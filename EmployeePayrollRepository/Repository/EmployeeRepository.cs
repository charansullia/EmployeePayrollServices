using EmployeePayrollModel;
using EmployeePayrollRepository.Context;
using EmployeePayrollRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

    }
}
