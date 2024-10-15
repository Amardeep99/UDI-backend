using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UDI_backend.Models;

public class UdiDatabase : DbContext {

	public DbSet<Application> Applications => Set<Application>();
	public DbSet<Form> Forms => Set<Form>();
	public DbSet<Reference> References => Set<Reference>();

	public UdiDatabase(DbContextOptions<UdiDatabase> options) : base(options) {
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.Entity<Reference>()
			.HasOne(r => r.Form)          
			.WithOne(f => f.Reference)      
			.HasForeignKey<Form>(f => f.ReferenceId);  

		modelBuilder.Entity<Reference>()
			.Property(r => r.FormId)
			.IsRequired(false);  
	}
}