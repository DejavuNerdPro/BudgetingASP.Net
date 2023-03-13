using Budgeting.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budgeting.Services
{
    public class DBService :DbContext
    {
        public DBService(DbContextOptions<DBService> options) : base(options)
        {

        }
        public DbSet<Expensediture> getRow { get; set; }
        public DbSet<UserDataModel> getUserData { get; set; }
        public DbSet<LoginDataModel> getLoginData { get; set; }
    }
}
