using EmployeePayrollManager.Interface;
using EmployeePayrollModel;
using EmployeePayrollRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollManager.Manager
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository empRepository;
        public EmployeeManager(IEmployeeRepository empRepository)
        {
            this.empRepository = empRepository;
        }

        public async Task<EmployeeModel> AddEmployeeDetail(EmployeeModel employeeModel)
        {
            try
            {
                return await this.empRepository.AddEmployeeDetail(employeeModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EmployeeModel> EditEmployeeDetail(EmployeeModel employeeModel)
        {
            try
            {
                return await this.empRepository.EditEmployeeDetail(employeeModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
