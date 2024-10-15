using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace UDI_backend.Models;

public class UdiDatabase : DbContext {
	private readonly IConfiguration _configuration;

	public DbSet<Application> Applications => Set<Application>();
	public DbSet<Form> Forms => Set<Form>();
	public DbSet<Reference> References => Set<Reference>();

	public UdiDatabase(IConfiguration configuration) {
		_configuration = configuration;
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
		// Blir konfigurert i Program
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