using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.FinnhubService;
using ServiceContracts.StocksService;
using Services.FinnhubService;
using Services.StockService;
using StockApp;
using StockApp.Middleware;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddControllersWithViews();
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
builder.Services.AddTransient<IBuyOrderService, StockBuyOrdersService>();
builder.Services.AddTransient<ISellOrderService, StockSellOrdersService>();
builder.Services.AddTransient<IFinnhubCompanyProfileService, FinnhubCompanyProfileService>();
builder.Services.AddTransient<IFinnhubStockPriceQuoteSerivce, FinnhunStockPriceQuoteService>();
builder.Services.AddTransient<IFinnhubSotckService, FinnhubStocksService>();
builder.Services.AddTransient<IFinnhubSearchStocksService, FinnhubSearchStocksService>();
builder.Services.AddTransient<IStockRepository, StocksRepository>();
builder.Services.AddTransient<IFinnhubRepository, FinnhubRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddHttpClient();
builder.Services.AddTransient<ExcpetionHandlingMiddleware>();


var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseMiddleware<ExcpetionHandlingMiddleware>();
}

if (builder.Environment.IsEnvironment("Test") == false)
    Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");


app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();


public partial class Program { } //make the auto-generated Program accessible programmatically