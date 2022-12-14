using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        public IConfiguration Configuration { get; set; }

        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }       
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<GitHubProfile> GitHubProfiles { get; set; }





        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {

            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(a =>
            {
                a.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");

                a.HasMany(p => p.Technologies);
            });

            modelBuilder.Entity<Technology>(a =>
            {
                a.ToTable("Technologies").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
                a.Property(p => p.Name).HasColumnName("Name");

                a.HasOne(p => p.ProgrammingLanguage);
            });


            modelBuilder.Entity<User>(p =>
            {
                p.ToTable("Users").HasKey(k => k.Id);
                p.Property(p => p.Id).HasColumnName("Id");
                p.Property(p => p.FirstName).HasColumnName("FirstName");
                p.Property(p => p.LastName).HasColumnName("LastName");
                p.Property(p => p.Email).HasColumnName("Email");
                p.Property(p => p.PasswordHash).HasColumnName("PasswordHash");
                p.Property(p => p.PasswordSalt).HasColumnName("PasswordSalt");
                p.Property(p => p.Status).HasColumnName("Status");
                p.Property(p => p.AuthenticatorType).HasColumnName("AuthenticatorType");

                p.HasMany(p => p.UserOperationClaims);
                p.HasMany(p => p.RefreshTokens);
                
            });

            

            modelBuilder.Entity<OperationClaim>(p =>
            {
                p.ToTable("OperationClaims").HasKey(x => x.Id);
                p.Property(x => x.Id).HasColumnName("Id");
                p.Property(x => x.Name).HasColumnName("Name");
            });

            modelBuilder.Entity<UserOperationClaim>(p =>
            {
                p.ToTable("UserOperationClaims").HasKey(x => x.Id);
                p.Property(x => x.Id).HasColumnName("Id");
                p.Property(x => x.UserId).HasColumnName("UserId");
                p.Property(x => x.OperationClaimId).HasColumnName("OperationClaimId");

                p.HasOne(x => x.OperationClaim);
                p.HasOne(x => x.User);
            });

            modelBuilder.Entity<GitHubProfile>(p =>
            {
                p.ToTable("GitHubProfiles").HasKey(x => x.Id);
                p.Property(x=>x.Id).HasColumnName("Id");
                p.Property(x => x.UserId).HasColumnName("UserId");
                p.Property(x => x.GitHubLink).HasColumnName("GitHubLink");

                p.HasOne(x => x.User);
            });

           

            
            ProgrammingLanguage[] programmingLanguageEntitySeeds = { new(1, "C#"), new(2, "Python"), new(3,"Java") };
            modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguageEntitySeeds);

            

            Technology[] technologyEntitySeeds =
            {
                new(1, 1, "ASP.NET"),
                new(2, 3, "SPRİNG"),
                new(3, 1, "Windows Presentation Foundation"),
                new(4, 3, "JavaServer Pages")
            };
            modelBuilder.Entity<Technology>().HasData(technologyEntitySeeds);
        }
    }
}
