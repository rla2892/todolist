using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TodoList.Data.Context;
using TodoList.Service;

namespace TodoList
{
    public class Startup
    {
        private string _connectionString { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("TodoListLocal");
        }

        public IConfiguration Configuration { get; }

        private string _corsPolicyName = "localCorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(_connectionString));
            services.AddScoped<TodoService>(); // add singleton 되지 않음 -> appDbContext 와 scope 문제
            services.AddControllers();
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy(name: _corsPolicyName,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:8080")
                                      .AllowAnyMethod()
                                      .AllowAnyOrigin()
                                      .AllowAnyHeader();
                                  });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            // NOTE : swagger 정보 -> https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
            app.UseSwaggerUI();

            //app.UseHttpsRedirection(); // NOTE : TEST 를 위해 잠시 꺼둠

            app.UseRouting();

            app.UseCors(_corsPolicyName);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
