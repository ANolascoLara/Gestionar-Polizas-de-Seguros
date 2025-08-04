using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

builder.Services.AddScoped< BL.Usuario>();
builder.Services.AddScoped<BL.Rol>();
builder.Services.AddScoped<BL.Login>();
builder.Services.AddScoped<BL.Poliza>();
builder.Services.AddScoped<BL.TipoPoliza>();
builder.Services.AddScoped<BL.Estatus>();
builder.Services.AddScoped<BL.Pais>();
builder.Services.AddScoped<BL.Estado>();
builder.Services.AddScoped<BL.Municipio>();
builder.Services.AddScoped<BL.Colonia>();
builder.Services.AddScoped<BL.Genero>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "localhost",
            ValidAudience = "localhost",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("S3cr3t_k3y!.123_S3cr3t_k3y!.123.Pass@word1"))
        };

    });

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
