using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using rContentMan.Models;
using rContentMan.Services;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<CosmosDbService>(InitializeCosmosClientInstanceAsync(builder.Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());

builder.Services.AddSingleton<PublishDbService>(InitializeCosmosPublishClientInstanceAsync(builder.Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("https://rmake.azurewebsites.net",
                                "https://localhost:7152",
                                "https://localhost:7061",
                                "https://localhost:5197/",
                                "https://rmake.co")
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials();
        });
});
builder.Services.AddSignalR();
builder.Services.AddResponseCompression(o =>
{
    o.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseCors();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<CollaborativeHub>("/collabHub");
});

app.Run();


static async Task<CosmosDbService> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
{
    string databaseName = configurationSection.GetSection("DatabaseName").Value;
    string containerName = configurationSection.GetSection("ContainerName").Value;
    string account = configurationSection.GetSection("Account").Value;
    string key = configurationSection.GetSection("Key").Value;

    
    Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
    
    CosmosDbService cosmosDbService = new CosmosDbService(client, databaseName, containerName);
    //PublishDbService cosmosDbServicePublish = new PublishDbService(client, databaseName, "publish");


    Microsoft.Azure.Cosmos.DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
    
    await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");
    //await database.Database.CreateContainerIfNotExistsAsync("publish", "/id");

    return cosmosDbService;
}

static async Task<PublishDbService> InitializeCosmosPublishClientInstanceAsync(IConfigurationSection configurationSection)
{
    string databaseName = configurationSection.GetSection("DatabaseName").Value;
    string containerName = configurationSection.GetSection("PublishContainerName").Value;
    string account = configurationSection.GetSection("Account").Value;
    string key = configurationSection.GetSection("Key").Value;


    Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);

    PublishDbService cosmosDbServicePublish = new PublishDbService(client, databaseName, containerName);

    Microsoft.Azure.Cosmos.DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);


    //var containertemp = database.Database.GetContainer(containerName);

    //await containertemp.DeleteContainerAsync();

    var container = await database.Database.CreateContainerIfNotExistsAsync(containerName, "/PortfolioId");
    //await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

    return cosmosDbServicePublish;
}