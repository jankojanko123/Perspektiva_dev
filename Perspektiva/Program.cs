using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Perspektiva.Data;
using Perspektiva.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");
// tole zgorej sm spremenu, iz ApplicationDbContextConnection v DefaultConnection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));;


// Add services to the container.
connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

/*TODO: can throw out*/
builder.Services.Configure<FormOptions>(Option =>
{
  Option.MultipartBodyLengthLimit = 200000000;
});


/*CoreAdmin*/
/*you can only enter core admin with these roles*/
builder.Services.AddCoreAdmin("Admin");
builder.Services.AddCoreAdmin("SuperAdmin");
/**/
var app = builder.Build();

/*Settings for coreadmin*/
//app.UseCoreAdminCustomUrl("adminpanel");
app.UseCoreAdminCustomTitle("Perspectiva SysAdmin controll pannes");

/**/
/// <summary>
/// enums - checks if enums in table- if not it seeds them.
/// </summary>

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await ContextSeed.SeedRolesAsync(userManager, roleManager); 
        await ContextSeed.SeedSuperAdminAsync(userManager, roleManager); //seeds default user as super admin

    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}
/// <summary>
/// 
/// </summary>




var mvcBuilder = builder.Services.AddRazorPages();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{



    app.UseMigrationsEndPoint();
    app.UseBrowserLink();
    
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();


app.Run();
