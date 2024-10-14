using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UDI_backend.Models;

public class UdiDatabase : DbContext {

	public DbSet<Application> Applications => Set<Application>();
	public DbSet<Form> Forms => Set<Form>();
	public DbSet<Reference> References => Set<Reference>();

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
		optionsBuilder.UseSqlServer("Server=tcp:amardeep-fatima-server.database.windows.net,1433;Initial Catalog=udi-mssql-database;Persist Security Info=False;User ID=CloudSA35e670b3;Password=udierbest!123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
			.UseLowerCaseNamingConvention();
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