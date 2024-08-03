using BackendService.DataAccess.Context;
using BackendService.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

//------------------------[ Add services to the container ]-----------------------------
void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddDatabaseContext<ApplicationDbContext>(builder.Configuration.GetConnectionString("SQLiteConnection"));
    services.AddHelpers();
    services.AddRepositories();
    services.AddUnitOfWork();
}
//--------------------------------------------------------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHttpLog();

app.UseXssValidation();

app.Run();