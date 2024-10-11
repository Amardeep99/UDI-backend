using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UDI_backend.Models2;

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

    public virtual DbSet<Reference> References { get; set; }

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
            entity.HasKey(e => e.FormId).HasName("PK__form__FB05B7BD638D661D");

            entity.ToTable("form");

            entity.HasIndex(e => e.OrganisationId, "IX_form_OrganisationID");

            entity.HasIndex(e => e.ReferenceId, "IX_form_ReferenceID");

            entity.Property(e => e.FormId).HasColumnName("FormID");
            entity.Property(e => e.ObjectionReason).HasColumnType("text");
            entity.Property(e => e.OrganisationId).HasColumnName("OrganisationID");
            entity.Property(e => e.ReferenceId).HasColumnName("ReferenceID");

            entity.HasOne(d => d.Organisation).WithMany(p => p.Forms)
                .HasForeignKey(d => d.OrganisationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__form__Organisati__02084FDA");

            entity.HasOne(d => d.Reference).WithMany(p => p.Forms)
                .HasForeignKey(d => d.ReferenceId)
                .HasConstraintName("FK__form__ReferenceI__03F0984C");
        });

        modelBuilder.Entity<Reference>(entity =>
        {
            entity.HasKey(e => e.ReferenceId).HasName("PK__referenc__E1A99A7932C18402");

            entity.ToTable("reference");

            entity.HasIndex(e => e.ApplicationId, "IX_reference_ApplicationID");

            entity.HasIndex(e => e.FormId, "IX_reference_FormID");

            entity.Property(e => e.ReferenceId).HasColumnName("ReferenceID");
            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.FormId).HasColumnName("FormID");

            entity.HasOne(d => d.Application).WithMany(p => p.References)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reference__Appli__7F2BE32F");

            entity.HasOne(d => d.Form).WithMany(p => p.References)
                .HasForeignKey(d => d.FormId)
                .HasConstraintName("FK__reference__FormI__02FC7413");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
