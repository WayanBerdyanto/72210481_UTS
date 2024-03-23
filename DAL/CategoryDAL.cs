using System.Data.SqlClient;
using CatalogAPI.DAL.Interfaces;
using CatalogAPI.Models;

namespace CatalogAPI.DAL
{
    public class CategoryDAL : ICategory
    {
        private readonly IConfiguration _config;

        public CategoryDAL(IConfiguration config)
        {
            _config = config;
        }

        private string GetConnectionString()
        {
            return _config.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Category> GetAll()
        {
            List<Category> categories = new List<Category>();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"SELECT * FROM Categories order by CategoryName";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Category category = new Category();
                        category.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                        category.CategoryName = dr["CategoryName"].ToString();
                        categories.Add(category);
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();

                return categories;
            }
        }

        public Category GetByID(int id)
        {
            Category category = new Category();

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"SELECT * FROM Categories WHERE CategoryID = @categoryID";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@categoryID", id);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    category.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                    category.CategoryName = dr["CategoryName"].ToString();

                }
                dr.Close();
                cmd.Dispose();
                conn.Close();

                return category;
            }
        }

        public IEnumerable<Category> GetByName(string name)
        {
            List<Category> categories = new List<Category>();

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"SELECT * FROM Categories
                            WHERE CategoryName LIKE @CategoryName";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@CategoryName", "%" + name + "%");
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    Category category = new Category();
                    category.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                    category.CategoryName = dr["CategoryName"].ToString();
                    categories.Add(category);

                }
                dr.Close();
                cmd.Dispose();
                conn.Close();

                return categories;
            }
        }

        public void Update(Category obj)
        {
            throw new NotImplementedException();
        }

        public void Insert(Category obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}