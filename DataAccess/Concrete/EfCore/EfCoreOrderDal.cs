using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Abstract;
using Entities;

namespace DataAccess.Concrete.EfCore
{
    public class EfCoreOrderDal:EfCoreGenericRepository<Order,ShopContext>,IOrderDal
    {
    }
}
