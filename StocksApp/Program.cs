using StocksApp.Configuration;
using Services;
using ServiceContracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IFinnhubService,FinnhubService>();
builder.Services.AddScoped<IStocksService, StocksService>();
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
builder.Services.AddDbContext<StocksDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]));


var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();


app.Run();
