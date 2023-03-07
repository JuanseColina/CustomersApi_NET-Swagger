using CustomersApi.Casos_de_uso;
using CustomersApi.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();//interfaces graficas para mostrar en el navegador
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(routing=>routing.LowercaseUrls = true);//hacer siempre al principio de un proyecto

builder.Services.AddDbContext<CustomerDataBaseContext>(mysqlBuilder =>
{
    mysqlBuilder.UseMySQL(builder.Configuration.GetConnectionString("Connection1"));
});
builder.Services.AddScoped<IUpdateCustomerUseCase, UpdateCustomerUseCase>();
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




