using Netrex.Frontend.Application.DIs;
using Netrex.Frontend.Application.Services.Customer.Implementation;
using Netrex.Frontend.Application.Services.Customer.Interfaces;
using Netrex.Frontend.Blazor.Components;
using Netrex.Frontend.Blazor.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. HttpClient Setup
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7239/");
});

// 2. Add Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 3. --- LOADER SERVICE REGISTER KAREIN ---
// Isse AddScoped ke sath register karna hai taaki poori app mein ek hi instance chale
builder.Services.AddScoped<LoaderService>();
builder.Services.AddScoped<ICustomerManager, CustomerManager>();

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
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();

// 5. Render Mode Setup
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode(); // Ye animations ke liye zaroori hai

app.Run();