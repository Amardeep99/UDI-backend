using Microsoft.EntityFrameworkCore;
using UDI_backend.Database;
using UDI_backend.Models;

namespace UDI_backend {
	public class Program {
		public static void Main(string[] args) {
			//Start 

			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			var connectionString = builder.Configuration.GetConnectionString("UdiDatabase");

			builder.Services.AddDbContext<UdiDatabase>(options =>
				options.UseSqlServer(connectionString).UseLowerCaseNamingConvention());

			builder.Services.AddScoped<DatabaseContext>();

			builder.Services.AddControllers();

			// Network settings
			builder.Services.AddCors(options => {
				options.AddPolicy("AllowSpecificOrigin",
					builder => builder.WithOrigins("http://localhost:5173")
						.AllowAnyMethod()
						.AllowAnyHeader()
						.AllowCredentials());
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.

			// app.UseHttpsRedirection();

			app.MapControllers();

			app.Run();
		}
	}
}
