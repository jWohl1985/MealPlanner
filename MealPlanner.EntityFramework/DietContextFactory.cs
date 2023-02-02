using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MealPlanner.EntityFramework;

public class DietContextFactory : IDesignTimeDbContextFactory<DietContext>
{
    public DietContext CreateDbContext(string[] args = null!)
    {
        DbContextOptionsBuilder<DietContext> options = new DbContextOptionsBuilder<DietContext>();
        options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MealPlanner;Trusted_Connection=True");
        return new DietContext(options.Options);
    }
}
