using ContactsAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<ContactsAPIDbContext>(options=>options.UseInMemoryDatabase("ContactsDb"));//now we have given enity framework everything it needs
//to create an memory database and it also knows abt the tables bcs we have given it a dbset as well.
builder.Services.AddDbContext<ContactsAPIDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ContactsAPIConnectionString")));//now we have given enity framework everything it needs


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
