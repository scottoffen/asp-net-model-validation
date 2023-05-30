using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Hellang.Middleware.ProblemDetails.Mvc;
using SampleApi.FluentValidationManual.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddProblemDetails();
builder.Services.AddControllers().AddProblemDetailsConventions();
builder.Services.AddValidatorsFromAssemblyContaining<SampleRequestValidator>(ServiceLifetime.Singleton);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseProblemDetails();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
