using Microsoft.EntityFrameworkCore;
using Schedulling.Modal.Database_Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedulling.MyData
{
    public class DatabaseContexts :DbContext
    {
        public DatabaseContexts(DbContextOptions<DatabaseContexts> options) : base(options)
        {

        }
        public DbSet<Schedules> Schedules { get; set; }
        public DbSet<Completed> Completed { get; set; }
        public DbSet<Phones> Phones { get; set; }
        public DbSet<Schedulling.Modal.Database_Modal.Members> Members { get; set; }
    }
}
