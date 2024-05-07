using DCCRailway.Components;
using DCCRailway.Railway;
using DCCRailway.Railway.Configuration;
using DCCRailway.Railway.EndPoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
       .AddInteractiveServerComponents();

var railway = new RailwayManager();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.AddEndPoints(railway.Config);
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();