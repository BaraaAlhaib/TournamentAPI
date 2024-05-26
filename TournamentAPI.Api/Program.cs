
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TournamentAPI.Api.Extensions;
using TournamentAPI.Core.Entities;
using TournamentAPI.Core.Repositories;
using TournamentAPI.Data.Data;
using TournamentAPI.Data.Dto;
using TournamentAPI.Data.Repositories;

namespace TournamentAPI.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
         

            builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
                .AddNewtonsoftJson()
                .AddXmlDataContractSerializerFormatters();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<TournamentContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("TournamentDB")));

            builder.Services.AddAutoMapper(typeof(TournamentMappings));
            builder.Services.AddScoped<IUoW, UoW>();
            builder.Services.AddScoped<ITournamentRepository, TournamentRepository>();
            builder.Services.AddScoped<IGameRepository, GameRepository>();

            var app = builder.Build();

            app.SeedDataAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
