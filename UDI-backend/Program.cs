using UDI_backend.Database;
using UDI_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace UDI_backend {
	public class Program {
		public static void Main(string[] args) {
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllers();

			// Configure DbContext
			builder.Services.AddDbContext<UdiDatabase>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DB_CONNECTION_STRING")));

			// Register DatabaseContext as scoped
			builder.Services.AddScoped<DatabaseContext>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			// app.UseHttpsRedirection();

			app.MapControllers();

			app.Run();
		}
	}
}