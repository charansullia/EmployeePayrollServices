using EmployeePayrollModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayrollRepository.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<RegisterModel> Users { get; set; }
        public DbSet<EmployeeModel> Emp { get; set; }
    }
}
