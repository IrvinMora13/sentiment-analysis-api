using SentimentApi.Data;
using Microsoft.EntityFrameworkCore;
using SentimentApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CommentsContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<InterfaceSentimentAnalyzer, SentimentAnalyzer>();
builder.Services.AddControllers();
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
app.MapControllers();

app.Run();
