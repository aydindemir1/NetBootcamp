using Bootcamp.ApplicationService;
using Bootcamp.ApplicationService.Products;
using Bootcamp.Cache;
using Bootcamp.PersistenceRepository;
using Bootcamp.PersistenceRepository.Products;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// applicationService => IProductRepository
// PersistenceRepository => ProductRepositorySqlServer
builder.Services.AddScoped<IProductRepository, ProductRepositorySqlServer>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddMemoryCache();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"),
        options => { options.MigrationsAssembly("Bootcamp.PersistenceRepository"); });
});

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
