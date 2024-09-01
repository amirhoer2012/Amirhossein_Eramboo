using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DBContext_EF : DbContext
    {
        public DBContext_EF(DbContextOptions op) : base(op) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExcelContentlRow>()
                .HasOne(c => c.ExcelContent)
                .WithMany(g => g.Rows)
                .HasForeignKey(s => s.ExcelFileName);


            modelBuilder.Entity<ExcelContent>().HasKey(c => c.ExcelFileName);
            modelBuilder.Entity<ExcelContentlRow>().HasKey(c => new { c.ExcelFileName, c.Id });

        }

        public DbSet<ExcelContentlRow> ExcelContentRows { get; set; }

        public DbSet<ExcelContent> ExcelContents { get; set; }
    }
}
