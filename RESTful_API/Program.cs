using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RESTful_API.DAL.Context;
using RESTful_API.DAL.Repository;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddLogging();
builder.Services.AddDbContext<RESTful_Api_Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("AppSettings:Token").Value!))
    };
});

builder.Services.AddTransient(typeof(IProductRepository), typeof(ProductRepository));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Use(async (context, next) =>
{
    // Request baþlangýç zamanýný tutar.
    var startTime = DateTime.UtcNow;

    // Request URL'ini loglar.
    Console.WriteLine($"Request URL: {context.Request.Path}");

    // Request'in sonraki middleware'le devam etmesini saðlar.
    await next();

    // Response için geçen süreyi hesaplar.
    var elapsedTime = DateTime.UtcNow - startTime;

    // Response status code'unu loglar.
    Console.WriteLine($"Response Status Code: {context.Response.StatusCode}");

    // Response süresini loglar.
    Console.WriteLine($"Elapsed Time: {elapsedTime.TotalMilliseconds} ms");
});


app.UseMiddleware<LoggingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
