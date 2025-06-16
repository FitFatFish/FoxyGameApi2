using Foxy.Core.Repository;
using Foxy.Core.Services;
using Foxy.DataLayer.DBContext;
using Foxy.DataLayer.Models.FoxyGame;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "https://foxy.com",       
            ValidAudience = "foxyapi",                        
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("A94FBC709E9A4D61B74CDA5F8F351EC8"))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddOpenApi();
builder.Services.AddDbContext<FoxyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

#region IOC
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<ISupportService, SupportService>();
builder.Services.AddScoped<IStoreItemService, StoreItemService>();
 
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
