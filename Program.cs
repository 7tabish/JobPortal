using JobPortal.DBContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Authorization;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("JobPortalContextConnection") ?? throw new InvalidOperationException("Connection string 'JobPortalContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<JobDbContext>(options =>
            options.UseSqlServer(builder.Configuration["ConnectionStrings:DevConnection"]));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<JobDbContext>();



builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    //options.LoginPath = "/Accoun/Login";
    //options.LogoutPath = "/Account/Logout";
    options.Cookie.Name = "jobPortalApp";

});

//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();
//});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.MapRazorPages();

app.Run();
