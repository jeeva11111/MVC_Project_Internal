using BackEnd.AuthorizationFilters.AuthFilter;
using BackEnd.AuthorizationFilters.Services;
using BackEnd.Middleware;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using WebApi_Project_Internal.Data;

using MVC_Project_Internal.Filters;
using WebApi_Project_Internal.AuthorizationFilters.Repositories;
using WebApi_Project_Internal.AuthorizationFilters.Services;
using WebApi_Project_Internal;
//using WebApi_Project_Internal.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ServerLink"));
});

// Add services to the container.
// Add SignalR and other services
//builder.Services.AddSignalR();
//builder.Services.AddTransient<HubNotifications>();
 builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ErrorHandler>();
builder.Services.AddScoped<IAccountServices, AccountRepositories>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped<IUserAccount, UserAccountSettings>();
builder.Services.AddScoped<ApiKeyValidation>();

builder.Services.AddHttpClient("Users", x =>
{
    x.BaseAddress = new Uri("https://localhost:7101/api/User/GetUsers");
});
//x => x.Filters.Add<ApiKeyValidation>()
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

//x => x.Filters.Add<ApiKeyValidation>()
builder.Services.AddSession(x =>
{
    x.IdleTimeout = TimeSpan.FromMinutes(10);
    x.Cookie.Name = "LearnNext";
    //x.Cookie.HttpOnly = true;
    //x.Cookie.IsEssential = true;

});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// app.UseMiddleware<ErrorHandler>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSession();
app.UseCors("AllowAllOrigins");

//app.UseMiddleware<ApiKeyValidation>();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();

// Map the SignalR hub
//app.MapHub<HubNotifications>("/HubNotifications");

app.Run();


