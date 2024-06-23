using System.Text.Json.Serialization;
using Api_Author.Filters;
// using Api_Author.Middleware;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Api_Author.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("defaultConnection");

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(FilterException));
}).AddJsonOptions(json => json.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles).AddNewtonsoftJson();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddAutoMapper(typeof(Program));

// builder.Services.AddTransient<IService, ServiceA>();
// builder.Services.AddScoped<IService, ServiceA>();
//builder.Services.AddSingleton<IService, ServiceB>();
// builder.Services.AddTransient<ServiceTransient>();
// builder.Services.AddScoped<ServiceScope>();
// builder.Services.AddSingleton<ServiceSingleton>();
// builder.Services.AddResponseCaching();
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
// builder.Services.AddTransient<MyFilterAction>();
// builder.Services.AddHostedService<WriterOnFile>();

var app = builder.Build();

// app.UseLogginResponseHttp();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseResponseCaching();
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.UseAuthorization();

app.Run();
