using CatalogAPI.DAL;
using CatalogAPI.DAL.Interfaces;
using CatalogAPI.DTO.Category;
using CatalogAPI.DTO.Product;
using CatalogAPI.Models;
using CatalogServices;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICategory, CategoryDapper>();
builder.Services.AddScoped<IProduct, ProductDapper>();

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
                Url = new Uri("https://github.com/WayanBerdyanto/72210481_UTS")
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
    return Results.Ok(new { success = true, message = "request update successful", data = categoriesDto });
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
    return Results.Ok(new { success = true, message = "request update successful", data = categoryDto });
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
    return Results.Ok(new { success = true, message = "request successful", data = categoriesDto });
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
        return Results.Created($"/api/category/{category.CategoryID}", category);
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
        return Results.Ok(new { success = true, message = "request update successful", data = category });
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
        return Results.Ok(new { success = true, message = "request delete successful" });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});
// Product API
app.MapGet("/api/getAllProduct", (IProduct product) =>
{
    List<ProductDto> productDto = new List<ProductDto>();
    var products = product.GetAll();
    if (!products.Any())
    {
        return Results.NotFound(new { error = true, message = "Data Kosong" });
    }
    foreach (var data in products)
    {
        productDto.Add(new ProductDto
        {
            ProductID = data.ProductID,
            CategoryID = data.CategoryID,
            Name = data.Name,
            Description = data.Description,
            Price = data.Price,
            Quantity = data.Quantity,
        });
    }
    return Results.Ok(new { success = true, message = "request successful", data = productDto });
}).WithOpenApi();

app.MapGet("/api/getProductById/{id}", (IProduct products, int id) =>
{
    ProductDto productDto = new ProductDto();
    var product = products.GetByID(id);
    if (product == null)
    {
        return Results.NotFound(new { error = true, message = "Id Tidak Ditemukan" });
    }
    productDto.ProductID = product.ProductID;
    productDto.CategoryID = product.CategoryID;
    productDto.Name = product.Name;
    productDto.Description = product.Description;
    productDto.Price = product.Price;
    productDto.Quantity = product.Quantity;
    return Results.Ok(new { success = true, message = "request successful", data = productDto });
}).WithOpenApi();

app.MapGet("/api/getProduct/search/{name}", (IProduct products, string name) =>
{
    List<ProductDto> productDto = new List<ProductDto>();
    var product = products.GetByName(name);
    if (!product.Any())
    {
        return Results.NotFound(new { error = true, message = "Nama Tidak Ditemukan" });
    }
    foreach (var data in product)
    {
        productDto.Add(new ProductDto
        {
            ProductID = data.ProductID,
            CategoryID = data.CategoryID,
            Name = data.Name,
            Description = data.Description,
            Price = data.Price,
            Quantity = data.Quantity,
        });
    }
    return Results.Ok(new { success = true, message = "request successful", data = productDto });
}).WithOpenApi();


app.MapPost("/api/product", (IProduct productDal, CreateProductDto productDto) =>
{
    try
    {
        Product product = new Product
        {
            CategoryID = productDto.CategoryID,
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            Quantity = productDto.Quantity,
        };
        productDal.Insert(product);

        //return 201 Created
        return Results.Created($"/api/product/{product.CategoryID}", product);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapPut("/api/product", (IProduct productDal, ProductDto productDto) =>
{
    try
    {
        var product = new Product
        {
            ProductID = productDto.ProductID,
            CategoryID = productDto.CategoryID,
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            Quantity = productDto.Quantity,
        };
        productDal.Update(product);
        return Results.Ok(new { success = true, message = "request update successful", data = product });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapDelete("/api/product/{id}", (IProduct productDal, int id) =>
{
    try
    {
        productDal.Delete(id);
        return Results.Ok(new { success = true, message = "request delete successful" });
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
