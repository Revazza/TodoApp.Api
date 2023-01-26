using Microsoft.AspNetCore.Identity;
using TodoApp.Api.Auth;
using TodoApp.Api.Db.Entities;
using TodoApp.Api.Models;
using TodoApp.Api.Seed;
using TodoApp.Api.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AuthConfigurator.Configure(builder);

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<TokenGenerator>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


var seeder = new Seeder(app);
await seeder.SeedRoles();

app.MapControllers();

app.Run();
