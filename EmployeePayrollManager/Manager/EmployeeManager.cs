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

        public async Task<EmployeeModel> AddEmployeeDetail(EmployeeModel employeemodel)
        {
            try
            {
                return await this.empRepository.AddEmployeeDetail(employeemodel);
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

        public IEnumerable<EmployeeModel> GetEmployeeDetail(int EmployeeId)
        {
            try
            {
                return this.empRepository.GetEmployeeDetail(EmployeeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EmployeeModel> DeleteEmployeeDetail(int EmployeeId)
        {
            try
            {
                return await this.empRepository.DeleteEmployeeDetail(EmployeeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
