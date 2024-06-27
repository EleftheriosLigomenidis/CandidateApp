using CandidateApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {}


        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<CandidateDegree> CandidateDegrees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CandidateDegree>()
         .HasKey(cd => new { cd.CandidateId, cd.DegreeId });

            modelBuilder.Entity<CandidateDegree>()
                .HasOne(cd => cd.Candidate)
                .WithMany(c => c.CandidateDegrees)
                .HasForeignKey(cd => cd.CandidateId)
                 .OnDelete(DeleteBehavior.Restrict); ;

            modelBuilder.Entity<CandidateDegree>()
                .HasOne(cd => cd.Degree)
                .WithMany(d => d.CandidateDegrees)
                .HasForeignKey(cd => cd.DegreeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.Property(e => e.Lastname).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Firstname).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Mobile)
                      .HasConversion<long>()
                      .HasAnnotation("Range", new RangeAttribute(10, 10));  // Mobile as long to fit the range
            });

            // Configure Degree
            modelBuilder.Entity<Degree>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(500).IsRequired();
            });

        }
    }
}
