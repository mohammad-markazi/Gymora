using Gymora.Database;
using Gymora.Service;
using Gymora.Service.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ErrorHandlingMiddleware = Gymora.Presentation.ErrorHandlingMiddleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = " Bearer {token}"
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
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = "www.gymora.net",
        ValidAudience = "http://localhost:36145/",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Your_Secret_Key_Here_this_is_program_sport_for_gymora"))
    };
    options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
    {
        OnChallenge = async context =>
        {
            // جلوگیری از پاسخ پیش‌فرض
            context.HandleResponse();

            var response = ResponseFactory.Fail(
                message: "احراز هویت انجام نشد",
                statusCode: ApiStatusCode.Unauthorized
            );

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var json = JsonConvert.SerializeObject(response, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            await context.Response.WriteAsync(json);
        },
        OnForbidden = async context =>
        {
            var response = ResponseFactory.Fail(
                message: "شما دسترسی لازم برای این عملیات را ندارید",
                statusCode: ApiStatusCode.Forbidden
            );

            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";

            var json = JsonConvert.SerializeObject(response, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            await context.Response.WriteAsync(json);
        }
    };

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDatabaseConfigService(builder.Configuration);
builder.Services.AddServiceConfigService(builder.Configuration);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<GymoraDbContext>()?.Database.Migrate();
}
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
