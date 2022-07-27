using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonsterMexa.API;
using MonsterMexa.API.Controllers;
using MonsterMexa.BusinessLogic;
using MonsterMexa.DataAccess.Postgres;
using MonsterMexa.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MonsterMexaDbContext>(options =>
{
    options.UseNpgsql("Host=localhost;Port=5433;Database=MonsterMexdb;Username=postgres;Password=pwd;Integrated Security=True");
});

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ApiMappingProfile>();
    cfg.AddProfile<DataAccessMappingProfile>();
});

builder.Services.AddScoped<IProductsPepository, ProductsRepository>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<ICategotiesService, CategoriesService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();

builder.Services.AddScoped<ServiceB>();
builder.Services.AddScoped<ServiceA>();
builder.Services.AddTransient<RepositoryA>();
builder.Services.AddScoped<RepositoryB>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddControllers().AddControllersAsServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();
app.UseSession();
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

public partial class Program { }
