using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BraspagTest_WebAPI.Models
{
    public class MdrContext : DbContext
    {
        public MdrContext(DbContextOptions<MdrContext> options)
            : base(options)
        {
        }
        public DbSet<Mdr> Mdrs { get; set; }
    }
}
