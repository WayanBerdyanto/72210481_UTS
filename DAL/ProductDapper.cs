using System.Data.SqlClient;
using CatalogAPI.DAL.Interfaces;
using CatalogAPI.Models;
using Dapper;
namespace CatalogAPI.DAL
{
    public class ProductDapper : IProduct
    {
        private readonly IConfiguration _config;
        private readonly Connection _conn;

        public ProductDapper(IConfiguration config)
        {
            _config = config;
            _conn = new Connection(_config);
        }

        public IEnumerable<Product> GetAll()
        {
            using (SqlConnection conn = _conn.GetConnectDb())
            {
                try
                {
                    var strSql = @"SELECT * FROM Products order by Name";
                    var products = conn.Query<Product>(strSql);
                    if (products == null)
                    {
                        throw new ArgumentException("Data Null");
                    }
                    return products;
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

        public Product GetByID(int id)
        {
            using (SqlConnection conn = _conn.GetConnectDb())
            {
                var strSql = @"SELECT * FROM Products WHERE ProductID = @ProductID";
                var param = new { ProductID = id };
                try
                {
                    var product = conn.QueryFirstOrDefault<Product>(strSql, param);
                    return product;

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

        public IEnumerable<Product> GetByName(string name)
        {
            using (SqlConnection conn = _conn.GetConnectDb())
            {
                var strSql = @"SELECT * FROM Products WHERE Name LIKE @Name";
                var param = new { Name = $"%{name}%" };
                try
                {
                    var product = conn.Query<Product>(strSql, param);
                    return product;
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
            throw new NotImplementedException();
        }

        public void Insert(Product obj)
        {
            using (SqlConnection conn = _conn.GetConnectDb())
            {
                var strSql = @"INSERT INTO Products (CategoryID, Name, Description, Price, Quantity ) VALUES (@CategoryID, @Name, @Description, @Price, @Quantity)";
                var param = new
                {
                    obj.CategoryID,
                    obj.Name,
                    obj.Description,
                    obj.Price,
                    obj.Quantity,
                };
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

        public void Update(Product obj)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}