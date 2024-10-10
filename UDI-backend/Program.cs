using UDI_backend.Database;

namespace UDI_backend {
	public class Program {
		public static void Main(string[] args) {
			DatabaseContext db = new();
			db.CreateApplication(123, "2024-01-01");
			//var builder = WebApplication.CreateBuilder(args);

			//// Add services to the container.

			//builder.Services.AddControllers();

			//var app = builder.Build();

			//// Configure the HTTP request pipeline.

			//app.UseHttpsRedirection();

			//app.UseAuthorization();


			//app.MapControllers();

			//app.Run();
		}
	}
}
