using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UDI_backend.Models;

public partial class UdiMssqlDatabaseContext : DbContext
{
    public UdiMssqlDatabaseContext()
    {
    }

    public UdiMssqlDatabaseContext(DbContextOptions<UdiMssqlDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Form> Forms { get; set; }

    public virtual DbSet<Process> Processes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:amardeep-fatima-server.database.windows.net,1433;Initial Catalog=udi-mssql-database;Persist Security Info=False;User ID=CloudSA35e670b3;Password=udierbest!123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.OrganisationId).HasName("PK__actor__722346BCAE979417");

            entity.ToTable("actor");

            entity.Property(e => e.OrganisationId)
                .ValueGeneratedNever()
                .HasColumnName("OrganisationID");
            entity.Property(e => e.ContactName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OrganisationName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PK__Applicat__C93A4F79ADE7D38F");

            entity.ToTable("application");

            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.DNumber).HasColumnName("D_number");
            entity.Property(e => e.TravelDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Form>(entity =>
        {
            entity.HasKey(e => e.FormId).HasName("PK__form__FB05B7BD760F52FA");

            entity.ToTable("form");

            entity.Property(e => e.FormId).HasColumnName("FormID");
            entity.Property(e => e.HasDebt).HasColumnName("hasDebt");
            entity.Property(e => e.ObjectionReason).HasColumnType("text");
        });

        modelBuilder.Entity<Process>(entity =>
        {
            entity.HasKey(e => e.RefrenceId).HasName("PK__process__2DA2E1F8B1D8870B");

            entity.ToTable("process");

            entity.Property(e => e.RefrenceId).HasColumnName("RefrenceID");
            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.FormId).HasColumnName("FormID");
            entity.Property(e => e.OrganisationId).HasColumnName("OrganisationID");

            entity.HasOne(d => d.Application).WithMany(p => p.Processes)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__process__Applica__6B24EA82");

            entity.HasOne(d => d.Form).WithMany(p => p.Processes)
                .HasForeignKey(d => d.FormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__process__FormID__6A30C649");

            entity.HasOne(d => d.Organisation).WithMany(p => p.Processes)
                .HasForeignKey(d => d.OrganisationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__process__Organis__693CA210");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
