using EasyBudget.Config;
using EasyBudget.Data;
using EasyBudget.Data.Dto.CategoryDto;
using EasyBudget.Data.Dto.Validators;
using EasyBudget.Repositories.Implementations;
using EasyBudget.Repositories.IRepositories;
using EasyBudget.Services.Implementations;
using EasyBudget.Services.IServices;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace EasyBudget
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(
                options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("EasyBudgetDb")));

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IMovementRepository, MovementRepository>();
            builder.Services.AddScoped<IMovementService, MovementService>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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