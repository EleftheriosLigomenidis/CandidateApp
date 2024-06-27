using CandidateApp.Business.Exceptions;
using CandidateApp.Business.Options;
using CandidateApp.Controllers;
using CandidateApp.Data;

using CandidateApp.Extentions;
using CandidateApp.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    // efarmozete apo to browser
    options.AddPolicy(name: "_devOrigin",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                      });
});
builder.Services.AddControllers().ConfigureApiBehaviorOptions(setupAction =>
{
    setupAction.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .SelectMany(e => e.Value?.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

        var exception = new ValidationException()
        {
            Errors = errors
        };

        var result =JsonSerializer.Serialize(exception);

        return new BadRequestObjectResult(result)
        {
            ContentTypes = { "application/json" }
        };
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBusinessServices();

builder.Services.Configure<AzureBlobStorageSettings>(builder.Configuration.GetSection(AzureBlobStorageSettings.Key));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("_devOrigin");
app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();

app.Run();
