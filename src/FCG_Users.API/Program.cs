using FCG_Common.Api.Middlewares;
using FCG_Users.API;
using FCG_Users.API.Config;
using FCG_Users.API.Endpoints;
using FCG_Users.Application;
using FCG_Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHealthCheck(builder.Configuration);

builder.Services.ConfigureAuthentication(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ExceptionHaandlingMiddleware>();

builder.Services.AddApiServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.ConfigureSwagger();

var app = builder.Build();

await SeedConfig.SeedDatabase(app.Services, app.Configuration);

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapAllApiEndpoints();

app.Run();
