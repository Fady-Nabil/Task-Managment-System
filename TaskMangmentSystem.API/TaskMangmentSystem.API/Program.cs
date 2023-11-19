using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskMangmentSystem.API.Extensions;
using TaskMangmentSystem.API.Hubs;
using TaskMangmentSystem.API.Middleware;
using TaskMangmentSystem.Core.Entities;
using TaskMangmentSystem.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IssueContext>(options => 
    options.UseSqlServer(builder.Configuration.
    GetConnectionString("DefaultConnection")));
builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseRouting();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.UseEndpoints(route =>
{
    route.MapHub<NotificationsHub>("/hubs/notifications");
});

app.MapControllers();

app.Run();
