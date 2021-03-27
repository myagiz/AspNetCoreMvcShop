using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DataAccess.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EfCore
{
    public class EfCoreCategoryDal:EfCoreGenericRepository<Category,ShopContext>,ICategoryDal
    {
        public Category GetByIdWithProducts(int id)
        {
            using (var context=new ShopContext())
            {
                return context.Categories
                    .Where(x => x.Id == id)
                    .Include(x => x.ProductCategories)
                    .ThenInclude(x => x.Product)
                    .FirstOrDefault();
            }
        }

        public void DeleteFromCategory(in int categoryId, in int productId)
        {
            using (var context = new ShopContext())
            {
                var cmd = @"delete from ProductCategory where ProductId=@p0 And CategoryId=@p1";
                context.Database.ExecuteSqlRaw(cmd, productId, categoryId);
            }
        }
    }
}
