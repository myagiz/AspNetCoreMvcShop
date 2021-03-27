using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EfCore;
using Entities;

namespace Business.Concrete
{
    public class ProductManager:IProductService
    {
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }

        public List<Product> GetPopularProducts()
        {
            return _productDal.GetAll(p=>p.Price>1000);
        }

        public List<Product> GetProductsByCategory(string category,int page,int pageSize)
        {
            return _productDal.GetProductsByCategory(category,page,pageSize);
        }

        public Product GetById(int id)
        {
            return _productDal.GetById(id);
        }

        public Product GetProductDetails(int id)
        {
            return _productDal.GetProductDetails(id);
        }

        public bool Create(Product entity)
        {
            if (Validate(entity))
            {
                _productDal.Create(entity);
                return true;
            }

            return false;
        }

        public void Update(Product entity)
        {
            _productDal.Update(entity);
        }

        public void Delete(Product entity)
        {
           _productDal.Delete(entity);
        }

        public int GetCountByCategory(string category)
        {
            return _productDal.GetCountByCategory(category);
        }

        public Product GetByIdWithCategories(int id)
        {
            return _productDal.GetByIdWithCategories(id);
        }

        public void Update(Product entity, int[] categoryIds)
        {
            _productDal.Update(entity,categoryIds);
        }

        public string ErrorMessage { get; set; }
        public bool Validate(Product entity)
        {
            var isValid = true;
            if (string.IsNullOrEmpty(entity.Name))
            {
                ErrorMessage += "Ürün ismi girmelisiniz";
                isValid = false;
            }

            if (entity.Price<1)
            {
                ErrorMessage += "1 liradan büyük fiyat giriniz";
                isValid = false;
            }
            return isValid;
        }
    }
}
