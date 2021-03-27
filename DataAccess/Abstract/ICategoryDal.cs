using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Entities;

namespace DataAccess.Abstract
{
    public interface ICategoryDal : IRepository<Category>
    {
        Category GetByIdWithProducts(int id);
        void DeleteFromCategory(in int categoryId, in int productId);
    }
}
