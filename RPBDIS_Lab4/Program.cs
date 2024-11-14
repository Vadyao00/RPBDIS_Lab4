using Microsoft.EntityFrameworkCore;
using RPBDIS_Lab4.Data;
using RPBDIS_Lab4.Middleware;
using RPBDIS_Lab4.Services;

namespace RPBDIS_Lab4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string connection = builder.Configuration.GetConnectionString("DefaultConnectionLocal")!;
            builder.Services.AddDbContext<CinemaContext>(options => options.UseSqlServer(connection));
            builder.Services.AddTransient<IMovieService, MovieService>();
            builder.Services.AddMemoryCache();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();

            builder.Services.AddMvc();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //new logic
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseDbInitializer();
            app.UseMovieCache("Movies 10");

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
