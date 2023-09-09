using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using morshop.business.Abstract;
using morshop.business.Concrete;
using morshop.repository.Abstract;
using morshop.repository.Concrete;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ShopContext>(options => {
        options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection"));
        options.EnableSensitiveDataLogging((true));//Identity tablolarının nereye ekleneceğini belittik veritabanı
    });
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IProductService,ProductManager>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<ICategoryService,CategoryManager>();
builder.Services.AddScoped<ICardRepository,CardRepository>();
builder.Services.AddScoped<ICardService,CardManager>();
builder.Services.AddScoped<IOrderRepository,OrderRepository>();
builder.Services.AddScoped<IOrderService,OrderManager>();
string MyAllowOrigins="_myAllowOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name:MyAllowOrigins,
        builder =>{
            builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
