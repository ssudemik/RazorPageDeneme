using Microsoft.EntityFrameworkCore;
using RazorPageDeneme.Models;
using System.Configuration;

namespace RazorPageDeneme
{
    public class Program
    {

        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

            // builder.Services.AddDbContext<Context>();

       

        builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Context")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapGet("/", context =>
            {
                context.Response.Redirect("/ProductFile/Product");
                return Task.CompletedTask;
            });

            app.MapRazorPages();

            app.Run();

        }

    }
}