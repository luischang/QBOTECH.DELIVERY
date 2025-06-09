using Microsoft.EntityFrameworkCore;
using QBOTECH.DELIVERY.CORE.Interfaces;
using QBOTECH.DELIVERY.CORE.Services;
using QBOTECH.DELIVERY.INFRASTRUCTURE.Data;
using QBOTECH.DELIVERY.INFRASTRUCTURE.Repositories;
using QBOTECH.DELIVERY.INFRASTRUCTURE.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var _config = builder.Configuration;

var connectionString = _config.GetConnectionString("DevConnection");
builder.Services.AddDbContext<QbotechDeliveryContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
// Add UsersService with IConfiguration
builder.Services.AddSharedInfrastructure(_config);
builder.Services.AddTransient<IUsersService, UsersService>();
 
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IDeliveriesService, DeliveriesService>();
builder.Services.AddScoped<IDeliveryLocationRepository, DeliveryLocationRepository>();
builder.Services.AddScoped<QBOTECH.DELIVERY.CORE.Services.DeliveryLocationService>();
builder.Services.AddScoped<IReportsService, ReportsService>();
builder.Services.AddScoped<IReportsRepository, ReportsRepository>();
builder.Services.AddScoped<IReportsService, ReportsService>();

//Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add CORS
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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
