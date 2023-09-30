
using NetCore_InMemoryCash.Models;
using NetCore_InMemoryCash.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddRazorPages();


// Add services to the container.
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ICacheProvider, CacheProvider>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); //Install Nuget in console: PM> Install-Package Swashbuckle.AspNetCore -Version 5.3.3

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

app.UseAuthorization();

//app.MapRazorPages();
app.MapControllers();
app.Run();

//
//https://www.c-sharpcorner.com/article/how-to-implement-caching-in-the-net-core-web-api-application/

//https://www.thecodebuzz.com/iservicecollection-does-not-contain-a-definition-of-addswaggergen/