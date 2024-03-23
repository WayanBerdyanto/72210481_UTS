using CatalogAPI.DAL;
using CatalogAPI.DAL.Interfaces;
using CatalogAPI.DTO;
using CatalogAPI.Models;
using CatalogServices;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICategory, CategoryDapper>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Version = "v1",
            Title = "Catalog API",
            Description = "Simple API documentation for OpenApi | DESAIN ARSITEKTUR MICROSERVICES",
            Contact = new OpenApiContact
            {
                Name = "Wayan Berdyanto",
                Url = new Uri("https://www.linkedin.com/in/wayanberdyanto/")
            },
            License = new OpenApiLicense
            {
                Name = "Wayan Berdyanto",
                Url = new Uri("https://github.com/WayanBerdyanto/Eclass")
            }
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

app.MapGet("/api/getAllCategory", (ICategory categoryDal) =>
{
    List<CategoryDTO> categoriesDto = new List<CategoryDTO>();
    var categories = categoryDal.GetAll();
    if (!categories.Any())
    {
        return Results.NotFound(new { error = true, message = "Data Kosong" });
    }
    foreach (var category in categories)
    {
        categoriesDto.Add(new CategoryDTO
        {
            CategoryID = category.CategoryID,
            CategoryName = category.CategoryName
        });
    }
    return Results.Ok(categoriesDto);
}).WithOpenApi();

app.MapGet("/api/getCategoryById/{id}", (ICategory categoryDal, int id) =>
{
    CategoryDTO categoryDto = new CategoryDTO();
    var category = categoryDal.GetByID(id);
    if (category == null)
    {
        return Results.NotFound(new { error = true, message = "Id Tidak Ditemukan" });
    }
    categoryDto.CategoryID = category.CategoryID;
    categoryDto.CategoryName = category.CategoryName;
    return Results.Ok(categoryDto);
}).WithOpenApi();

app.MapGet("/api/getCategory/search/{categoryName}", (ICategory categoryDal, string name) =>
{
    List<CategoryDTO> categoriesDto = new List<CategoryDTO>();
    var categories = categoryDal.GetByName(name);

    if (!categories.Any())
    {
        return Results.NotFound(new { error = true, message = "Nama Tidak Ditemukan" });
    }
    foreach (var category in categories)
    {
        categoriesDto.Add(new CategoryDTO
        {
            CategoryID = category.CategoryID,
            CategoryName = category.CategoryName
        });
    }
    return Results.Ok(categoriesDto);
});

app.MapPost("/api/category", (ICategory categoryDal, CategoryCreateDto categoryCreateDto) =>
{
    try
    {
        Category category = new Category
        {
            CategoryName = categoryCreateDto.CategoryName
        };
        categoryDal.Insert(category);

        //return 201 Created
        return Results.Created($"/api/categories/{category.CategoryID}", category);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapPut("/api/category", (ICategory categoryDal, CategoryUpdateDto categoryUpdateDto) =>
{
    try
    {
        var category = new Category
        {
            CategoryID = categoryUpdateDto.CategoryID,
            CategoryName = categoryUpdateDto.CategoryName
        };
        categoryDal.Update(category);
        return Results.Ok(new { success = true, message = "Data Berhasil Di Update" });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapDelete("/api/category/{id}", (ICategory categoryDal, int id) =>
{
    try
    {
        categoryDal.Delete(id);
        return Results.Ok(new { success = true, message = "Data Berhasil Di hapus" });

    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
