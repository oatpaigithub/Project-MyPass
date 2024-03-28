using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFunctionSQL
{
    public class DbMemoryMain : DbContext
    {
        public DbSet<MemoryMain> memoryMains { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseInMemoryDatabase(databaseName: "InMemoryDb456");

        }
    }
}
