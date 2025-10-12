using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bumb_Alexandru_Flaviu_Lab2.Models;

namespace Bumb_Alexandru_Flaviu_Lab2.Data
{
    public class Bumb_Alexandru_Flaviu_Lab2Context : DbContext
    {
        public Bumb_Alexandru_Flaviu_Lab2Context (DbContextOptions<Bumb_Alexandru_Flaviu_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Bumb_Alexandru_Flaviu_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Bumb_Alexandru_Flaviu_Lab2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<Bumb_Alexandru_Flaviu_Lab2.Models.Author> Author { get; set; } = default!;
    }
}
