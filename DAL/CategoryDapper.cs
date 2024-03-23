using System.Data.SqlClient;
using CatalogAPI.DAL.Interfaces;
using CatalogAPI.Models;
using Dapper;

namespace CatalogServices;

public class CategoryDapper : ICategory
{
    private readonly IConfiguration _config;
    public CategoryDapper(IConfiguration config)
    {
        _config = config;
    }

    private string GetConnectionString()
    {
        return _config.GetConnectionString("DefaultConnection");
    }

    public IEnumerable<Category> GetAll()
    {
        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            var strSql = @"SELECT * FROM Categories order by CategoryName";
            var categories = conn.Query<Category>(strSql);
            return categories;
        }
    }

    public Category GetByID(int id)
    {
        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            var strSql = @"SELECT * FROM Categories WHERE CategoryID = @CategoryID";
            var param = new { CategoryID = id };
            try
            {
                var category = conn.QueryFirstOrDefault<Category>(strSql, param);
                if (category == null)
                {
                    throw new ArgumentException("Data tidak ditemukan");
                }
                return category;
            }
            catch (SqlException sqlEx)
            {
                throw new ArgumentException($"Error: {sqlEx.Message} - {sqlEx.Number}");
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error: {ex.Message}");
            }
        }
    }

    public IEnumerable<Category> GetByName(string name)
    {
        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            var strSql = @"SELECT * FROM Categories
                            WHERE CategoryName LIKE @CategoryName";
            var param = new { CategoryName = $"%{name}%" };
            try
            {
                var categories = conn.Query<Category>(strSql, param);
                return categories;
            }
            catch (SqlException sqlEx)
            {
                throw new ArgumentException($"Error: {sqlEx.Message} - {sqlEx.Number}");
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error: {ex.Message}");
            }
        }
    }

    public void Insert(Category obj)
    {
        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            var strSql = @"INSERT INTO Categories (CategoryName) VALUES (@CategoryName)";
            var param = new { CategoryName = obj.CategoryName };
            try
            {
                conn.Execute(strSql, param);
            }
            catch (SqlException sqlEx)
            {
                throw new ArgumentException($"Error: {sqlEx.Message} - {sqlEx.Number}");
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error: {ex.Message}");
            }
        }
    }

    public void Update(Category obj)
    {
        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            var strSql = @"UPDATE Categories SET CategoryName = @CategoryName 
                            WHERE CategoryID = @CategoryID";
            var param = new { CategoryName = obj.CategoryName, CategoryID = obj.CategoryID };
            try
            {
                int rowsAffected = conn.Execute(strSql, param);

                if (rowsAffected == 0)
                {
                    throw new InvalidOperationException("Tidak ada baris yang di Update.");
                }
            }
            catch (SqlException sqlEx)
            {
                throw new ArgumentException($"Error: {sqlEx.Message} - {sqlEx.Number}");
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error: {ex.Message}");
            }
        }
    }

    public void Delete(int id)
    {
        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            var strSql = @"DELETE FROM Categories WHERE CategoryID = @CategoryID";
            var param = new { CategoryID = id };
            try
            {
                int rowsAffected = conn.Execute(strSql, param);

                if (rowsAffected == 0)
                {
                    throw new InvalidOperationException("Id Tidak Ditemukan");
                }
            }
            catch (SqlException sqlEx)
            {
                throw new ArgumentException($"Error: {sqlEx.Message} - {sqlEx.Number}");
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error: {ex.Message}");
            }
        }
    }
}
