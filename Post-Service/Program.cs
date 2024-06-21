using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Post_Service.Entities;
using Post_Service.Kafka;
using Post_Service.Repositories;
using Post_Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<PostDatabaseSettings>(builder.Configuration.GetSection("PostDatabase"));
builder.Services.AddSingleton<IPostDatabaseSettings>(sp => sp.GetRequiredService<IOptions<PostDatabaseSettings>>().Value);

builder.Services.AddSingleton<IPostRepository, PostRepository>();
builder.Services.AddSingleton<IPostService, PostService>();

builder.Services.AddSingleton<IMongoClient>(new MongoClient(builder.Configuration.GetValue<string>("PostDatabase:ConnectionString")));

builder.Services.AddHostedService<KafkaCommentPostConsumer>();

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

// Make the implicit Program class public so test projects can access it
public partial class Program { }