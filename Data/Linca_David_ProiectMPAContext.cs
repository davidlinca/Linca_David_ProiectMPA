using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Linca_David_ProiectMPA.Models;

namespace Linca_David_ProiectMPA.Data
{
    public class Linca_David_ProiectMPAContext : DbContext
    {
        public Linca_David_ProiectMPAContext (DbContextOptions<Linca_David_ProiectMPAContext> options)
            : base(options)
        {
        }

        public DbSet<Linca_David_ProiectMPA.Models.Movie> Movie { get; set; } = default!;
        public DbSet<Linca_David_ProiectMPA.Models.Genre> Genre { get; set; } = default!;
        public DbSet<Linca_David_ProiectMPA.Models.Director> Director { get; set; } = default!;
        public DbSet<Linca_David_ProiectMPA.Models.Studio> Studio { get; set; } = default!;
        public DbSet<Linca_David_ProiectMPA.Models.Actor> Actor { get; set; } = default!;
    }
}
