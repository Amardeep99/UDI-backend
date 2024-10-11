using UDI_backend.Database;

namespace UDI_backend {
	public class Program {
		public static void Main(string[] args) {
			var builder = WebApplication.CreateBuilder(args);
			DatabaseContext db = new();
			// Add services to the container.

			builder.Services.AddControllers();
			builder.Services.AddSingleton(new DatabaseContext());
			var app = builder.Build();

			// Configure the HTTP request pipeline.

			// app.UseHttpsRedirection();

			app.MapControllers();

			app.Run();
		}
	}
}
