using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EMS.Models;

public partial class EmsContext : DbContext
{
    public EmsContext()
    {
    }

    public EmsContext(DbContextOptions<EmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-303LNE9\\MSSQLSERVER01;Database=EMS;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Employee");

            entity.Property(e => e.Address)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__32C52A79ACF13712");

            entity.ToTable("Student");

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.StudentAddress)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.StudentName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
