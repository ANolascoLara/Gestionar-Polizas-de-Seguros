using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DL.SistemaGestionPolizaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SistemaGestionPoliza")));
builder.Services.AddScoped<BL.IUsuario, BL.Usuario>();
builder.Services.AddScoped<BL.IRol, BL.Rol>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
