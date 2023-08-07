using ACTIVIDAD_FORMULARIO.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddControllersWithViews();

string connString = ConfigurationExtensions
                    .GetConnectionString(builder.Configuration, "DefaultConnectionString");
builder.Services.AddDbContext<AlumnoContext>
    (
        options => options.UseSqlServer(connString)
    );


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Alumno}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())
{
    var servicios = scope.ServiceProvider;
    try
    {
        var context = servicios.GetRequiredService<AlumnoContext>();
        context.Database.EnsureCreated();
    }
    catch (Exception e)
    {

    }

}

app.Run();
