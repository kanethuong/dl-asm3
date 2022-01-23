using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using examedu.Services;
using examedu.Services.Account;
using examedu.Services.Question;
using ExamEdu.DB;
using ExamEdu.DTO;
using ExamEdu.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ExamEdu
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // DB config
            string connectionString = Configuration.GetConnectionString("Postgre");
            services.AddDbContext<DataContext>(opt => opt.UseNpgsql(connectionString));
            services.AddScoped<DataContext, DataContext>();
            
            // Setting JSON convert to camelCase in Object properties
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            
            // Map data from Model to DTO and back
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IExamService, ExamService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<ILevelService, LevelService>();

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            }).ConfigureApiBehaviorOptions(opt => // Custome Model State response object
            {
                opt.InvalidModelStateResponseFactory = actionContext =>
                    new BadRequestObjectResult(new ResponseDTO
                    {
                        Status = 400,
                        Message = "Input value is(are) invalid",
                        Errors = actionContext.ModelState.Where(x => x.Value.Errors.Count() != 0)
                                .ToDictionary(
                                    kvp => kvp.Key,
                                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                                )
                    });
            });
;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExamEdu", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExamEdu v1"));
            }

            // ReFormat exception message
            app.UseExceptionHandler(e => e.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;
                await context.Response.WriteAsJsonAsync(new ResponseDTO(500, exception.Message));
            }));
            app.UseHttpsRedirection();

            //Get front-end url from appsettings.json
            var frontEndDevUrl = Configuration["FrontEndDevUrl"];

            //Get front-end url from appsettings.json
            // var frontEndUrl = Configuration["FrontEndUrl"];

            //CORS config for Front-end url
            app.UseCors(options => options.WithOrigins(frontEndDevUrl)
                                        .AllowAnyMethod()
                                        .AllowAnyHeader()
                                        .AllowCredentials());

            // ReFormat error message
            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(new ResponseDTO(401).ToString());
                }

                if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(new ResponseDTO(403).ToString());
                }
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
