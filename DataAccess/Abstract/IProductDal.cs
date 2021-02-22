using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Entities;

namespace DataAccess.Abstract
{
    public interface IProductDal:IRepository<Product>
    {
        IEnumerable<Product> GetPopularProducts();
        Product GetProductDetails(int id);
        List<Product> GetProductsByCategory(string category,int page,int pageSize);
        int GetCountByCategory(string category);
    }
}
