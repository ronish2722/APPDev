using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication3.Application.Common.Interface;
using WebApplication3.Infrastructure.DI;
using WebApplication3.Infrastructure.Identity;
using WebApplication3.Infrastructure.Services;

//var builder = WebApplication.CreateBuilder(args);
var builder = WebApplication.CreateBuilder(args);

const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastucture(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:7158/",
            "http://localhost:5011/");
        });
});

var app = builder.Build();

//seed data
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
await SeedIdentityData.InitializeAsync(services);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();


app.Run();
