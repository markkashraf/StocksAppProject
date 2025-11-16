using StocksApp.Configuration;
using Services;
using ServiceContracts;
using Entities.DBContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IFinnhubService,FinnhubService>();
builder.Services.AddSingleton<IStocksService, StocksService>();
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
builder.Services.AddDbContext<StockMarketDBContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]));
//Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PersonsDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();


app.Run();
