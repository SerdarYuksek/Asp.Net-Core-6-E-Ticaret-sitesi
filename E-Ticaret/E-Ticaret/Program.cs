using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();


builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
                 .RequireAuthenticatedUser()
                 .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddSession(options =>
{
    // 10 dakikalý Redis Timeout Süresi.
    options.IdleTimeout = TimeSpan.FromMinutes(10);
  
});

builder.Services.AddMvc();


builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie( x =>
    {
        x.LoginPath = "/Account/Login";
       
    });

builder.Services.AddDistributedMemoryCache();

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

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
app.UseSession();


app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
