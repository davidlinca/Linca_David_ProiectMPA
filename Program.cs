using Linca_David_ProiectMPA.Data;
using Linca_David_ProiectMPA.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Linca_David_ProiectMPAContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Linca_David_ProiectMPAContext") ?? throw new InvalidOperationException("Connection string 'Linca_David_ProiectMPAContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services
    .AddHttpClient<ITypePredictionService, TypePredictionService>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri("https://localhost:51148");
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DbInitializer.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
