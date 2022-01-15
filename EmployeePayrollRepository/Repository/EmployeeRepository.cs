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
                    EmployeeDetail.Name = employeeModel.Name;
                    EmployeeDetail.Profile = employeeModel.Profile;
                    EmployeeDetail.Gender = employeeModel.Gender;
                    EmployeeDetail.Department = employeeModel.Department;
                    EmployeeDetail.Salary = employeeModel.Salary;
                    EmployeeDetail.StartDate = employeeModel.StartDate;
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
         
        public  IEnumerable <EmployeeModel>GetEmployeeDetail(int EmployeeId)
        {
            try
            {
                IEnumerable<EmployeeModel> EmployeeList = this.context.Emp.Where(x => x.EmployeeId == EmployeeId).ToList();
                if (EmployeeList != null)
                {
                    return EmployeeList;
                }
                return null;
            }
            catch(ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EmployeeModel>DeleteEmployeeDetail(int EmployeeId)
        {
            try
            {
                var EmpDetail = await this.context.Emp.Where(x => x.EmployeeId == EmployeeId).SingleOrDefaultAsync();
                if (EmpDetail != null)
                {
                    this.context.Remove(EmpDetail);
                    await this.context.SaveChangesAsync();
                    return EmpDetail;
                }
                return null;
            }
            catch(ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
