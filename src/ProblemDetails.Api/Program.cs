using System.Diagnostics;
using ProblemDetails.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Problem Details support
builder.Services.AddProblemDetails(options =>
{
    // Customize ProblemDetails response
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions["traceId"] = 
            Activity.Current?.Id ?? context.HttpContext.TraceIdentifier;
        context.ProblemDetails.Extensions["environment"] = 
            builder.Environment.EnvironmentName;
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add custom exception handling middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Enable Problem Details middleware
app.UseStatusCodePages();
app.UseExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();