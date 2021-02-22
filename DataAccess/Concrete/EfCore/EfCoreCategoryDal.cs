using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DataAccess.Abstract;
using Entities;

namespace DataAccess.Concrete.EfCore
{
    public class EfCoreCategoryDal:EfCoreGenericRepository<Category,ShopContext>,ICategoryDal
    {
        
    }
}
