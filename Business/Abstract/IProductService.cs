using System;
using System.Collections.Generic;
using System.Text;
using Entities;

namespace Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll();
        List<Product> GetPopularProducts();
        List<Product> GetProductsByCategory(string category,int page, int pageSize);
        Product GetById(int id);
        Product GetProductDetails(int id);
        void Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
        int GetCountByCategory(string category);
    }
}
