using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobusCustomerAPI.Data.Models
{
    public partial class GlobusDbContext : DbContext
    {
        public GlobusDbContext()
        {

        }
        public GlobusDbContext(DbContextOptions<GlobusDbContext> options): base(options)
        {

        }
        public virtual DbSet<TblCustomerDetails> TblCustomerDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=FINTRAK-MMBS\FINTRAKSQL;Database=GlobusCustomerDB; User Id=sa; Password=sqluser10$;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblCustomerDetails>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.ToTable("Tbl_CustomerDetails");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("PhoneNumber")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Email)
                    .HasColumnName("Email")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Password)
                    .HasColumnName("Password")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.LGA)
                    .HasColumnName("LGA")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.StateOfResidence)
                    .HasColumnName("StateOfResidence")
                    .HasColumnType("varchar(100)");

            });
            }
        }
}
