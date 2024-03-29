﻿using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Domain.Entities.AtelierApp;
using Atelier.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Persistence.Contexts
{
    public class DatabaseContext : IdentityDbContext<User, Role, string>, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageAccess> PageAccess { get; set; }

        public DbSet<AtelierBase> AtelierBases { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<JwtUserToken> JwtUserTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(b =>
            {
                // Primary key
                b.HasKey(u => u.Id);

                // Indexes for "normalized" username and email, to allow efficient lookups

                //b.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
                //b.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

                b.HasQueryFilter(p => !p.IsRemoved);
                // Maps to the AspNetUsers table
                b.ToTable("Users");

                // A concurrency token for use with the optimistic concurrency checking
                b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

                // Limit the size of columns to use efficient database types
                b.Property(u => u.UserName).HasMaxLength(256);
                b.Property(u => u.NormalizedUserName).HasMaxLength(256);
                b.Property(u => u.Email).HasMaxLength(256);
                b.Property(u => u.NormalizedEmail).HasMaxLength(256);


                // The relationships between User and other entity types
                // Note that these relationships are configured with no navigation properties

                // Each User can have many UserClaims
                b.HasMany<IdentityUserClaim<string>>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

                // Each User can have many UserLogins
                b.HasMany<IdentityUserLogin<string>>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

                // Each User can have many UserTokens
                b.HasMany<IdentityUserToken<string>>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany<IdentityUserRole<string>>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            });
            builder.Entity<IdentityUserClaim<string>>(b =>
            {
                // Primary key
                b.HasKey(uc => uc.Id);

                // Maps to the AspNetUserClaims table
                b.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(b =>
            {
                // Composite primary key consisting of the LoginProvider and the key to use
                // with that provider
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });

                // Limit the size of the composite key columns due to common DB restrictions
                b.Property(l => l.LoginProvider).HasMaxLength(128);
                b.Property(l => l.ProviderKey).HasMaxLength(128);

                // Maps to the AspNetUserLogins table
                b.ToTable("UserLogins");
            });

            builder.Entity<IdentityUserToken<string>>(b =>
            {
                // Composite primary key consisting of the UserId, LoginProvider and Name
                b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

                // Limit the size of the composite key columns due to common DB restrictions

                // Maps to the AspNetUserTokens table
                b.ToTable("UserTokens");
            });

            builder.Entity<IdentityRole<string>>(b =>
            {
                // Primary key
                b.HasKey(r => r.Id);

                // Index for "normalized" role name to allow efficient lookups
                b.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

                // Maps to the AspNetRoles table
                b.ToTable("Roles");

                // A concurrency token for use with the optimistic concurrency checking
                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

                // Limit the size of columns to use efficient database types
                b.Property(u => u.Name).HasMaxLength(256);
                b.Property(u => u.NormalizedName).HasMaxLength(256);


                // The relationships between Role and other entity types
                // Note that these relationships are configured with no navigation properties

                // Each Role can have many entries in the UserRole join table
                b.HasMany<IdentityUserRole<string>>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany<IdentityRoleClaim<string>>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
            });

            builder.Entity<IdentityRoleClaim<string>>(b =>
            {
                // Primary key
                b.HasKey(rc => rc.Id);

                // Maps to the AspNetRoleClaims table
                b.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserRole<string>>(b =>
            {
                //b.ToTable("AtelierId");
                
                // Primary key
                b.HasKey(r => new { r.UserId, r.RoleId });
                // Maps to the AspNetUserRoles table
                b.ToTable("UserRoles");
            });
            //Seed Data
            SeedData(builder);

            //-- عدم نمایش اطلاعات حذف شده
            ApplyQueryFilter(builder);
        }

        private void ApplyQueryFilter(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsRemoved);
            //modelBuilder.Entity<Role>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<AtelierBase>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Branch>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Page>().HasQueryFilter(p => !p.IsRemoved);


        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Role>().HasData(new Role { Name = RoleesName.BigAdmin, PersianTitle = RoleesTitle.BigAdmin, NormalizedName = "BigAdmin" });
            //modelBuilder.Entity<Role>().HasData(new Role { Name = RoleesName.Admin, PersianTitle = RoleesTitle.Admin, NormalizedName = "Admin" });
            //modelBuilder.Entity<Role>().HasData(new Role { Name = RoleesName.Secretary, PersianTitle = RoleesTitle.Secretary, NormalizedName = "Secretary" });
            //modelBuilder.Entity<Role>().HasData(new Role { Name = RoleesName.Employee, PersianTitle = RoleesTitle.Employee, NormalizedName = "Employee" });
            //modelBuilder.Entity<Role>().HasData(new Role { Name = RoleesName.Customer, PersianTitle = RoleesTitle.Customer, NormalizedName = "Customer" });
        }
    }
}
