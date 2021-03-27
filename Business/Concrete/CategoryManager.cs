using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using DataAccess.Abstract;
using Entities;

namespace Business.Concrete
{
    public class CategoryManager:ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public List<Category> GetAll()
        {
            return _categoryDal.GetAll();
        }

        public Category GetById(int id)
        {
            return _categoryDal.GetById(id);
        }

        public Category GetByIdWithProducts(int id)
        {
            return _categoryDal.GetByIdWithProducts(id);
        }

        public void Create(Category entity)
        {
            _categoryDal.Create(entity);
        }

        public void Update(Category entity)
        {
            _categoryDal.Update(entity);
        }

        public void Delete(Category entity)
        {
            _categoryDal.Delete(entity);
        }

        public void DeleteFromCategory(int categoryId, int productId)
        {
            _categoryDal.DeleteFromCategory(categoryId,productId);
        }
    }
}
