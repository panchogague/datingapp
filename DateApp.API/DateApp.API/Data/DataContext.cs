using DateApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DateApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<ValuesModel> Values { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<PhotoModel> Photos {get;set;}
    }
}
