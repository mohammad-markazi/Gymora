using Gymora.Database;
using Gymora.Service;
using Gymora.Service.Utilities;
using ErrorHandlingMiddleware = Gymora.Presentation.ErrorHandlingMiddleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDatabaseConfigService(builder.Configuration);
builder.Services.AddServiceConfigService(builder.Configuration);

var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
