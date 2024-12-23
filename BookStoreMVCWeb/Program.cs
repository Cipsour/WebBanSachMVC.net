using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookStoreMVCWeb.Data;
using Volo.Abp.Data;
using Microsoft.EntityFrameworkCore.Storage;
using BookStoreMVCWeb.Models;
using Microsoft.AspNetCore.Identity;
using BookStoreMVCWeb.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookStoreMVCWebContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookStoreMVCWebContext") ?? throw new InvalidOperationException("Connection string 'BookStoreMVCWebContext' not found.")));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<Cart>(sp => Cart.GetList(sp));
    // Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;

});
//builder.Services.AddDbContext<BookStoreMVCWebContext>();
builder.Services.AddIdentity<AppUser,IdentityRole>(
	)
	.AddEntityFrameworkStores<BookStoreMVCWebContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
	// Password settings.
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 6;
	options.Password.RequiredUniqueChars = 1;
	options.User.RequireUniqueEmail = true;
	options.SignIn.RequireConfirmedEmail = true;
});
builder.Services.AddScoped<EmailSender>();

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseSession();
app.UseStaticFiles();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseDeveloperExceptionPage();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    /*app.UseDeveloperExceptionPage();
app.UseExceptionHandler("/Home/Error");*/
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
	name: "Areas",
	pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
