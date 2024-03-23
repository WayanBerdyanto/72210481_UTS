using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogAPI.DAL.Interfaces;
using CatalogAPI.Models;

namespace CatalogAPI.DAL
{
    public class ProductDAL : IProduct
    {

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Product GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Insert(Product obj)
        {
            throw new NotImplementedException();
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