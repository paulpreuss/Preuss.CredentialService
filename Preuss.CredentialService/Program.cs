using Preuss.CredentialService.Abstracts.Processors;
using Preuss.CredentialService.Cryptography;
using Preuss.CredentialService.Cryptography.Abstracts;
using Preuss.CredentialService.MongoAccess.Abstracts.Repositories;
using Preuss.CredentialService.MongoAccess.Repositories;
using Preuss.CredentialService.Processors;
using Preuss.CredentialService.Validation;
using Preuss.CredentialService.Validation.Abstracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("CredentialDatabase")!;

builder.Services.AddScoped<ICredentialRepository>(repository => new CredentialRepository(connectionString));
builder.Services.AddScoped<IMd5Factory, Md5Factory>();
builder.Services.AddScoped<ICredentialsValidator, CredentialsValidator>();
builder.Services.AddScoped<ICredentialProcessor, CredentialProcessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

