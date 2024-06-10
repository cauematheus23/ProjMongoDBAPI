using Microsoft.Extensions.Options;
using ProjMongoDBAPI.Services;
using ProjMongoDBAPI.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DataBaseSettings>(
    builder.Configuration.GetSection(nameof(DataBaseSettings)));

builder.Services.AddSingleton<IDataBaseSettings>(sp =>
    sp.GetRequiredService<IOptions<DataBaseSettings>>().Value);

builder.Services.AddSingleton<CustomerService>();
builder.Services.AddSingleton<AddressService>();

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
