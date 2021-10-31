using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CinemaApp.Auth.Interfaces;
using CinemaApp.Auth.Classes;
using CinemaApp.Database;
using CinemaApp.DAL.Repositories.MovieRepository;
using CinemaApp.Domain.Services.MovieService;
using CinemaApp.DAL.Repositories.ScreeningRepository;
using CinemaApp.Domain.Services.ScreeningService;
using CinemaApp.DAL.Repositories.ScreeningDayRepository;
using CinemaApp.Domain.Services.ScreeningDayService;
using CinemaApp.DAL.Repositories.Authentication;
using CinemaApp.DAL.Repositories.UserRepository;
using CinemaApp.Domain.Services.UserService;
using CinemaApp.Domain.Services.ReservationService;
using CinemaApp.DAL.Repositories.ReservationRepository;

namespace CinemaApp.API
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
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            //Dependency injection:
            
            //DbContext
            services.AddDbContext<CinemaAppDbContext>();
            
            //ScreeningDay
            services.AddScoped<IScreeningDayRepository, ScreeningDayRepository>();
            services.AddScoped<IScreeningDayService, ScreeningDayService>();

            //Screening
            services.AddScoped<IScreeningRepository, ScreeningRepository>();
            services.AddScoped<IScreeningService, ScreeningService>();

            //Movie
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieService, MovieService>();

            //User
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            //Reservation
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IReservationRepository, ReservationRepository>();

            //Authentication
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CinemaApp.API", Version = "v1" });
            });

            var key = "This is my test key";

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(key));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CinemaApp.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
