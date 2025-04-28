using Microsoft.EntityFrameworkCore;
using EMS.Models;
using EMS.Data;
using Microsoft.AspNetCore.Identity;
using EMS.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// ✅ Configure Application DbContext (for non-Identity tables)
builder.Services.AddDbContext<EmsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Configure Identity DbContext (for Identity tables)
builder.Services.AddDbContext<AuthContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Configure Identity with EMSUser
builder.Services.AddIdentity<EMSUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<AuthContext>()
.AddDefaultTokenProviders();

// ✅ Add MVC and Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// ✅ Configure Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // <-- Make sure Authentication is added
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
