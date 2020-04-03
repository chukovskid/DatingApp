using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DatingApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) // ova configuration treba da vodi kon AppSetings i appsetings.Developer kade sto e LOGIRANJETO
        {
            Configuration = configuration; 
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) 
        { // servisi se raboti koi sakame da gi dodademe na Applikacijata, kako sto e preku Link, localhost:4500.Images neka ti tekne vo c# Interfaces go koristevme
            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection"))); 
            // mora da mu dademe DB a taa ke bide SQLite so pristap Configuration.GetConnectionString("DefaultConnection") koj ke go zeme od **appsetings.json**
            // i MORA DA INSTALIRAS Mc.EntityFramework.SQLite!
            services.AddControllers();
            services.AddCors(); // ova go stavam za localhost:5000 da ne go cita kako virus ili hak. ti pisit na googleChrome
     
      }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) // se racuna za MIDDLEWARE
        { // ova e kako tece programata, niz sto sve treba da pomine, biten e redot
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // nesto pravi so catching Exeptions
            }

            // app.UseHttpsRedirection(); // go iskomentirav ova za da ne slusa https

            // ovie dole fun, routing, auth, endpoint se od MVC(porano bile zaedno vo .net 2.0)
            app.UseRouting();
            
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); // ova e sto dozvoluvame, koj bilo Metod i koj bilo Header

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
