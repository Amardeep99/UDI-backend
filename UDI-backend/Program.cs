using UDI_backend.Database;
using UDI_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace UDI_backend {
	public class Program {
		public static void Main(string[] args) {
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();

			builder.Services.AddDbContextFactory<UdiDatabase>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DB_CONNECTION_STRING")));

			builder.Services.AddScoped<DatabaseContext>();

			var app = builder.Build();

			app.MapControllers();

			app.Run();
		}
	}
}