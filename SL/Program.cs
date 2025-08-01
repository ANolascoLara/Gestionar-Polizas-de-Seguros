using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(option => option.AddPolicy("MyPolicy", policy => policy.WithOrigins("http://localhost:5075", "http://localhost:5233", "*")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        ));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DL.SistemaGestionPolizaContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("SistemaGestionPoliza")));

builder.Services.AddScoped<BL.IUsuario, BL.Usuario>();
builder.Services.AddScoped<BL.IRol, BL.Rol>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyPolicy");
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
