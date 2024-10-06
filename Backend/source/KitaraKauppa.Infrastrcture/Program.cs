using KitaraKauppa.Service.Repositories.Users;
using KitaraKauppa.Infrastrcture.Database;
using KitaraKauppa.Service.UsersService;
using Microsoft.EntityFrameworkCore;
//using KitaraKauppa.Infrastrcture.Repositories.Categories;
using KitaraKauppa.Service.Repositories.Cities;
//using KitaraKauppa.Infrastrcture.Repositories.Carts;
using KitaraKauppa.Service.Cryptography;
using KitaraKauppa.Infrastrcture.Cryptography;
using KitaraKauppa.Infrastrcture.Repositories.Orders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using KitaraKauppa.Core.Authentication;
using KitaraKauppa.Service.AuthenticationService;
using Microsoft.OpenApi.Models;
using KitaraKauppa.Service.Repositories.InMemory;
//using KitaraKauppa.Infrastrcture.Repositories.ProductReviews;
//using KitaraKauppa.Service.ProductReviewsService;
using Microsoft.AspNetCore.Authorization;
using KitaraKauppa.Service.Repositories.Products;
using KitaraKauppa.Service.ProductsServices;
using KitaraKauppa.Infrastrcture.Repositories.Products;
using KitaraKauppa.Presentation.Middleware;
using Serilog;
using Microsoft.Extensions.FileProviders;
using KitaraKauppa.Infrastrcture.Repositories.Users;
using KitaraKauppa.Infrastrcture.Repositories.Jwt;
using KitaraKauppa.Infrastrcture.Repositories.Cities;
using KitaraKauppa.Service.Repositories.Orders;
using KitaraKauppa.Service.OrdersService;
using KitaraKauppa.Service.Repositories.Brand;
using KitaraKauppa.Core.Configuration;
using KitaraKauppa.Infrastrcture.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<KitaraKauppaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseSnakeCaseNamingConvention()
    );

builder.Services.Configure<AzureBlobStorageSettings>(builder.Configuration.GetSection("AzureBlobStorage"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderManagement, OrderManagement>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IColorRepository, ColorRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IUserManagement, UserManagement>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IProductManagement, ProductManagement>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IAuthManagement, AuthManagement>();
builder.Services.AddSingleton<IInMemoryDB, InMemoryDB>();
builder.Services.AddTransient<IJwtManagement, JwtManagement>();
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JwtConfiguration"));
builder.Services.AddScoped<ExceptionMiddleware>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IProductDefinitionManagement, ProductDefinitionManagement>();
builder.Services.AddSingleton<IAzureBlobStorageSettings>(sp =>
        new AzureBlobStorageSettings(sp.GetRequiredService<IConfiguration>()));

// DI Product review repository
//builder.Services.AddScoped<IProductReviewRepository, ProductReviewsRepository>();
// DI order management service
//builder.Services.AddScoped<IProductReviewManagement, ProductReviewManagement>();



/** Domain DI Container End */

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
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtConfiguration:Issuer"],
            ValidAudience = builder.Configuration["JwtConfiguration:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfiguration:Secret"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOrUserIdPolicy", policy =>
        policy.RequireAssertion(context => IsAdminOrMatchingUserId(context))
    );

    options.AddPolicy("UserIdPolicy", policy =>
        policy.RequireAssertion(context => IsMatchingUserId(context))
    );
});

bool IsAdminOrMatchingUserId(AuthorizationHandlerContext context)
{
    var roleClaim = context.User.Claims.FirstOrDefault(c =>
        c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

    var userIdClaim = context.User.Claims.FirstOrDefault(c =>
        c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

    var routeUserId = GetRouteUserId(context);

    return roleClaim == "Admin" || userIdClaim == routeUserId;
}
bool IsMatchingUserId(AuthorizationHandlerContext context)
{
    var userIdClaim = context.User.Claims.FirstOrDefault(c =>
        c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

    var routeUserId = GetRouteUserId(context);

    return userIdClaim is not null && routeUserId is not null && userIdClaim == routeUserId;
}
string GetRouteUserId(AuthorizationHandlerContext context)
{
    return context.Resource is HttpContext httpContext
        ? httpContext?.Request.RouteValues["userId"]?.ToString()
        : null;
}

builder.Services.AddControllers(options =>
{

}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = false;
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Please enter a valid token"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendApp",
        builder =>
        {
            builder.WithOrigins("https://main.d7b5s8hshsh29.amplifyapp.com", "http://localhost:3000")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

//var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
//app.Urls.Add($"http://*:{port}");

var productImagesPath = "/var/app/current/ProductImages/";

if (!Directory.Exists(productImagesPath))
{
    Directory.CreateDirectory(productImagesPath);
}

app.UseCors("AllowFrontendApp");

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "ProductImages")),
    RequestPath = "/ProductImages"
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    
}

app.UseSwagger();
app.UseSwaggerUI();


app.UseMiddleware<ExceptionMiddleware>();

app.UseMiddleware<TokenValidationMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
