using GroovyApi.Services;
using Microsoft.Extensions.FileProviders;

namespace GroovyApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Make uploads folder if doesn't exist.
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Console.WriteLine("Uploads folder does not exist. Creating...");
                Directory.CreateDirectory(uploadsFolder);
            }

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<FileService>();
            builder.Services.AddSingleton<DatabaseService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Serve static files from the "Uploads" folder
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadsFolder),
                RequestPath = "/Uploads"  // This is the URL path for accessing files
            });

            app.MapControllers();

            app.Run();
        }
    }
}
