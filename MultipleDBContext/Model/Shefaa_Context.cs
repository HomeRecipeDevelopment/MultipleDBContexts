using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultipleDBContext.Model
{
    public class Shefaa_Context : DbContext
    {
        public Shefaa_Context(DbContextOptions<Shefaa_Context> options) : base(options)
        {

        }
        public DbSet<Governments> Governments { get; set; }
    }
}
