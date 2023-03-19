using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OnlineStoreSolution.App.Base;
using OnlineStoreSolution.App.Catalog.Products;
using OnlineStoreSolution.Data.EF_Core;
using OnlineStoreSolution.ViewModels.Catalog.Products;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<OnlineStoreDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("OnlineStoreDb")));

//Swagger
builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger OnlineStore v1", Version = "v1" });
});

// DI Declaration
builder.Services.AddTransient<IStorageService, FileStorageService>();
builder.Services.AddTransient<IPublicProductService, PublicProductService>();
builder.Services.AddTransient<IAdminProductService, AdminProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineStore v1");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
