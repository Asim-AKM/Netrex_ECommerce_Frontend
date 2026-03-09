using Microsoft.AspNetCore.Authentication.Cookies;
using Netrex.Frontend.Application.DIs;
using Netrex.Frontend.Application.Services.Customer.Implementation;
using Netrex.Frontend.Application.Services.Customer.Interfaces;
using Netrex.Frontend.Blazor.Components;
using Netrex.Frontend.Blazor.Middleware;
using Netrex.Frontend.Blazor.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. HttpClient Setup
var baseUrl = builder.Configuration["ApiSettings:BaseUrl"]!;
var v1 = builder.Configuration["ApiSettings:V1"]!;
var v2 = builder.Configuration["ApiSettings:V2"]!;

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri($"{baseUrl}{v1}");
});

builder.Services.AddHttpClient("ApiClientV2", client =>
{
    client.BaseAddress = new Uri($"{baseUrl}{v2}");
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                                  .AddCookie(options =>
                                  {
                                      options.LoginPath = "/login";
                                      options.AccessDeniedPath = "/401";
                                      options.ExpireTimeSpan = TimeSpan.FromDays(7);
                                      options.SlidingExpiration = true;
                                  });
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();


// 3. --- LOADER SERVICE REGISTER KAREIN ---
// Isse AddScoped ke sath register karna hai taaki poori app mein ek hi instance chale
builder.Services.AddScoped<LoaderService>();
builder.Services.AddScoped<ICustomerManager, CustomerManager>();
builder.Services.AddRazorPages();

// 4. Application Layer DIs (Iske andar hi AuthManager register ho raha hai)
builder.Services.AddApplicationDIs();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.MapStaticAssets();

app.MapRazorPages();
// 5. Render Mode Setup
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode(); // Ye animations ke liye zaroori hai

app.Run();