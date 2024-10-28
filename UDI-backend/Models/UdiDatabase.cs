using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UDI_backend.Models;

public class UdiDatabase : DbContext, IUdiDatabase {

	public DbSet<Application> Applications => Set<Application>();
	public DbSet<Form> Forms => Set<Form>();
	public DbSet<Reference> References => Set<Reference>();

	public UdiDatabase(DbContextOptions<UdiDatabase> options) : base(options) {
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.Entity<Reference>(entity => {
			entity.HasKey(r => r.ReferenceNumber);

			entity.Property(r => r.ReferenceNumber)
			 .ValueGeneratedNever();

			entity.HasOne(r => r.Form)
			 .WithOne(f => f.Reference)
			 .HasForeignKey<Form>(f => f.ReferenceNumber)
			 .OnDelete(DeleteBehavior.Cascade);

			entity.Property(r => r.FormId)
			 .IsRequired(false);

			entity.Property(r => r.Deadline)
			 .HasDefaultValueSql("DATEADD(day, 14, GETUTCDATE())");
		});
	}
}