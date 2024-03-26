using CatalogAPI.Models;

namespace CatalogAPI.DAL.Interfaces
{
    public interface IJoinTable : IJoin<Product>
    {
        IEnumerable<Product> GetByName(string name);
    }
}