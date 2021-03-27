using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;

        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View(new ProductListModel()
            {
                Products = _productService.GetAll()
            });
        }
        [HttpGet]
        public ActionResult CreateProduct()
        {
            return View(new ProductModel());
        }
        [HttpPost]
        public ActionResult CreateProduct(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Product()
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl
                };

                if (_productService.Create(entity))
                {
                    return RedirectToAction("Index");
                }
                ViewBag.ErrorMessage = _productService.ErrorMessage;
                return View(model);

            }

            return View(model);
        }
        [HttpGet]
        public ActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _productService.GetByIdWithCategories((int)id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = new ProductModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                SelectedCategories = entity.ProductCategories.Select(x => x.Category).ToList()
            };
            ViewBag.Categories = _categoryService.GetAll();
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> EditProduct(ProductModel model, int[] categoryIds, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var entity = _productService.GetById(model.Id);

                if (entity == null)
                {
                    return NotFound();
                }

                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.Price = model.Price;

                if (file != null)
                {
                    entity.ImageUrl = file.FileName;

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                _productService.Update(entity, categoryIds);
                return RedirectToAction("Index");
            }
            ViewBag.ErrorMessage = _productService.ErrorMessage;
            ViewBag.Categories = _categoryService.GetAll();

            return View(model);
        }

        public ActionResult DeleteProduct(int ProductId)
        {
            var entity = _productService.GetById(ProductId);
            if (entity != null)
            {
                _productService.Delete(entity);
            }

            return RedirectToAction("Index");
        }

        public ActionResult CategoryList()
        {
            return View(new CategoryListModel()
            {
                Categories = _categoryService.GetAll(),
            });
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategory(CategoryModel model)
        {
            var entity = new Category()
            {
                Name = model.Name,
            };
            _categoryService.Create(entity);
            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var entity = _categoryService.GetByIdWithProducts(id);
            if (entity == null)
            {
                NotFound();
            }
            var model = new CategoryModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Products = entity.ProductCategories.Select(p => p.Product).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult EditCategory(CategoryModel model)
        {
            var entity = _categoryService.GetById(model.Id);
            if (entity == null)
            {
                NotFound();
            }
            entity.Name = model.Name;
            _categoryService.Update(entity);
            return RedirectToAction("CategoryList");
        }

        public ActionResult DeleteCategory(int categoryId)
        {
            var entity = _categoryService.GetById(categoryId);
            if (entity != null)
            {
                _categoryService.Delete(entity);
            }
            return RedirectToAction("CategoryList");
        }

        public ActionResult DeleteFromCategory(int categoryId, int productId)
        {
            _categoryService.DeleteFromCategory(categoryId, productId);
            return Redirect("/admin/editcategory/" + categoryId);
        }
    }
}

