using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecureLoginRoleApp.Data;
using SecureLoginRoleApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Database connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity with Roles
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// ✅ Configure Cookie settings for Login & Access Denied redirection
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

// Force HTTPS
builder.Services.AddHttpsRedirection(options => options.HttpsPort = 443);

builder.Services.AddControllersWithViews();

var app = builder.Build();


async Task CreateRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roleNames = { "Admin", "User" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
            await roleManager.CreateAsync(new IdentityRole(roleName));
    }

    // Create default admin
    var adminUser = new ApplicationUser { UserName = "admin", Email = "admin@test.com" };
    string adminPassword = "Admin@123";
    var user = await userManager.FindByNameAsync(adminUser.UserName);
    if (user == null)
    {
        var createUser = await userManager.CreateAsync(adminUser, adminPassword);
        if (createUser.Succeeded)
            await userManager.AddToRoleAsync(adminUser, "Admin");
    }

    // Create default normal user
    var normalUser = new ApplicationUser { UserName = "user1", Email = "user1@test.com" };
    string userPassword = "User@123";
    var user2 = await userManager.FindByNameAsync(normalUser.UserName);
    if (user2 == null)
    {
        var createUser = await userManager.CreateAsync(normalUser, userPassword);
        if (createUser.Succeeded)
            await userManager.AddToRoleAsync(normalUser, "User");
    }
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await CreateRoles(services);
}

// Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // must come before Authorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();