using Microsoft.EntityFrameworkCore;
using UDI_backend.Clients;
using UDI_backend.Database;
using UDI_backend.Models;

namespace UDI_backend {
	public class Program {
		public static void Main(string[] args) {
			//Start 

			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			var connectionString = builder.Configuration.GetConnectionString("UdiDatabase");

			builder.Services.AddDbContext<IUdiDatabase, UdiDatabase>(options =>
				options.UseSqlServer(connectionString).UseLowerCaseNamingConvention());

			builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();

			builder.Services.AddControllers();

			builder.Services.AddHttpClient<IBronnoysundsRegClient, BronnoysundsRegClient>();

			// Network settings
			builder.Services.AddCors(options => {
				options.AddPolicy("AllowAnyOrigin",
					builder => builder
						.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader());
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.

			// app.UseHttpsRedirection();
			app.UseCors("AllowAnyOrigin");

			app.MapControllers();

			app.Run();
		}
	}
}
