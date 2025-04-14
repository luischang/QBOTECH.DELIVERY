using Microsoft.EntityFrameworkCore;
using QBOTECH.DELIVERY.CORE.Interfaces;
using QBOTECH.DELIVERY.CORE.Services;
using QBOTECH.DELIVERY.INFRASTRUCTURE.Data;
using QBOTECH.DELIVERY.INFRASTRUCTURE.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var _config = builder.Configuration;

var connectionString = _config.GetConnectionString("DevConnection");
builder.Services.AddDbContext<QbotechDeliveryContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
// Add UsersService
builder.Services.AddScoped<IUsersService, UsersService>();
//Add COARS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});



builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
