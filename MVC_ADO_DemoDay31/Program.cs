using Microsoft.Data.SqlClient;
using System.Data;
using MVC_ADO_DemoDay31.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// --- START: Dependency Injection Registrations ---

// Get the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register IDbConnection as a SqlConnection
// This ensures that when IDbConnection is requested, a new SqlConnection is provided.
// Using a lambda with AddScoped ensures that a new connection is created for each request.
builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(connectionString));

// Register ProductsRepository
// When ProductsRepository is requested, the DI container will now know how to create it.
// It will also automatically inject the IDbConnection registered above into its constructor.
builder.Services.AddScoped<ProductsRepository>();

// --- END: Dependency Injection Registrations ---


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");

app.Run();