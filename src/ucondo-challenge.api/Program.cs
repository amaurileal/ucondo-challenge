using ucondo_challenge.api.Extensions;
using ucondo_challenge.api.Middleware;
using ucondo_challenge.application.Extensions;
using ucondo_challenge.infrastructure.Extensions;
using ucondo_challenge.infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

//Seeder and migration execution (migration into UCondoChallengeSeeder)
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IUCondoChallengeSeeder>();
await seeder.Seed();

// Configure the HTTP request pipeline.
app.UseSwaggerConfiguration();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
