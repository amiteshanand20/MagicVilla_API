//using Serilog;

using MagicVilla_VillaAPI;
using MagicVilla_VillaAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("log/villaLogs.txt",rollingInterval: RollingInterval.Day).CreateLogger();

//builder.Host.UseSerilog();
builder.Services.AddDbContext<ApplicationDbContext>(options =>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"))
    .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning)); //ignore bug with dotnet ef migrations,if using new DateTime(),Guid.NewGuid()
});

builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddControllers().AddNewtonsoftJson();

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

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();   
app.Run();

