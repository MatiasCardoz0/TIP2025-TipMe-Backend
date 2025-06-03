using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TipMeBackend.Data.MesaRepository;
using TipMeBackend.Data.PropinaRepository;
using TipMeBackend.Data.UsuarioRepository;
using TipMeBackend.Middlewares;
using TipMeBackend.Models;
using TipMeBackend.Services.MesaService;
using TipMeBackend.Services.PropinaService;
using TipMeBackend.Services.UsuarioService;
using TipMeBackend.Services;
using TipMeBackend.Services.MPService;
using MercadoPago.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    });
});

builder.Services.AddSingleton<WebSocketHandler>();
builder.Services.AddSingleton<JwtService>();
builder.Services.AddScoped<IMesaRepository, MesaRepository>();
builder.Services.AddScoped<IMesaService,MesaService>();
builder.Services.AddScoped<IPropinaService, PropinaService>();
builder.Services.AddScoped<IPropinaRepository, PropinaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UserService>();
builder.Services.AddScoped<IMPService, MPService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5065); // HTTP
    //options.ListenAnyIP(7241, listenOptions => listenOptions.UseHttps()); // HTTPS
});

var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:SecretKey"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});


builder.Services.AddDbContext<Context>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 31))
    ));

var app = builder.Build();

app.UseWebSockets();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.MapControllers();

// Credenciales de MP
MercadoPagoConfig.AccessToken = "Access token a cambiar";

app.Run();
