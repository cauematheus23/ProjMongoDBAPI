using Microsoft.Extensions.Options;
using ProjMongoDBAPI.Services;
using ProjMongoDBAPI.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Vai em settings de DataBase e faz a configura��o com base na se��o do appsettings.json
builder.Services.Configure<DataBaseSettings>(
    builder.Configuration.GetSection(nameof(DataBaseSettings)));
// Pega as configura��es e cria uma instancia unica com o Singleton para ser usada em toda a aplica��o e pegando os servi�os requeridos
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
