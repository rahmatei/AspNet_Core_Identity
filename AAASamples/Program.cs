using AAASamples.Infra;
using AAASamples.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMvc();
builder.Services.AddRazorPages();
builder.Services.Configure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, opts =>
{
    opts.LoginPath = "/Account/Login";
    opts.AccessDeniedPath = "/Account/AccessDenied";
});
builder.Services.AddDbContext<AAADbContext>(c => c.UseSqlServer("Server=.; Initial Catalog=AAADb; User ID=sa; Password=12345678;  TrustServerCertificate=True"));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(c =>
{
    /*c.Password.RequiredLength = 10;
    c.Password.RequiredUniqueChars = 5;
    c.Password.RequireNonAlphanumeric = true;*/
    c.User.AllowedUserNameCharacters = "qwertyuiopasdfghjklzxcvbnm";
    c.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AAADbContext>().AddPasswordValidator<BlackListPasswordValidator<IdentityUser>>().AddUserValidator<CustomUserValidator>();

//UsernameInPasswordValidator

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapDefaultControllerRoute();

app.Run();
