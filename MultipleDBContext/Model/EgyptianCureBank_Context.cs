using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultipleDBContext.Model;

namespace MultipleDBContext.Model
{
    public class EgyptianCureBank_Context : DbContext
    {
        public EgyptianCureBank_Context(DbContextOptions<EgyptianCureBank_Context> options) : base(options)
        {

        }
        public DbSet<City> Cities { get; set; }
    }
}
