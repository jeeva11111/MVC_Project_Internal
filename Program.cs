using BackEnd.AuthorizationFilters.AuthFilter;
using BackEnd.AuthorizationFilters.Services;
using Microsoft.EntityFrameworkCore;
using MVC_Project_Internal.Data;
using MVC_Project_Internal.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ServerLink"));
});

// Add services to the container.
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

});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseMiddleware<ApiKeyValidation>();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();


app.Run();


