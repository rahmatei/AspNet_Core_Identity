using AAASamples.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMvc();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AAADbContext>(c => c.UseSqlServer("Server=.; Initial Catalog=AAADb; User ID=sa; Password=12345678;  TrustServerCertificate=True"));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AAADbContext>();

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.MapRazorPages();
app.MapDefaultControllerRoute();

app.Run();
