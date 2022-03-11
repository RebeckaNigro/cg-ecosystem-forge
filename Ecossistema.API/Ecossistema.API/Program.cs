using Ecossistema.API.Models.DTO;
using Ecossistema.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuracaoEmail = builder.Configuration.GetSection("ConfiguracaoEmail")
    .Get<ConfiguracaoEmail>();

builder.Services.AddSingleton(configuracaoEmail);
builder.Services.AddControllers();

builder.Services.AddScoped<IFaleConoscoService, FaleConoscoService>();
builder.Services.AddScoped<IEmailService, EmailService>();


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
