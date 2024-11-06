using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container (default blazor services)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Adds Azure vault key created to connect to DB
builder.Configuration.AddAzureKeyVault(
    new Uri("https://socialmediaappkv.vault.azure.net/"),
    new DefaultAzureCredential());

// Connects to DB
builder.Services.AddDbContext<SocialMediaDbContext>(options =>
    options.UseSqlServer(builder.Configuration["DefaultConnection"]));

// Builds app
var app = builder.Build();

// Default Blazor If Statement. Adds DB UI accessibility
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Default Blazor additions to app
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();