using BackOffice.Application.Filters;
using BackOffice.Core.Repositories;
using BackOffice.Infrastructure.Persistence;
using BackOffice.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var corsPolicy = "CorsPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200", "http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

builder.Services.AddControllers(options => options.Filters.Add(typeof(Validator)));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("BackOffice");
builder.Services.AddDbContext<BackOfficeDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IPessoasRepository, PessoasRepository>();
builder.Services.AddScoped<IEnderecosRepository, EnderecosRepository>();
builder.Services.AddScoped<IDepartamentosRepository, DepartamentosRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(corsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
