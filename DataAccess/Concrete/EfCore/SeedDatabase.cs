using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EfCore
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            var context = new ShopContext();
            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Categories.Count() == 0)
                {
                    context.Categories.AddRange(Categories);
                }

                if (context.Products.Count() == 0)
                {
                    context.Products.AddRange(Products);
                    context.AddRange(ProductCategory);
                }

                context.SaveChanges();
            }
        }

        private static Category[] Categories =
        {
            new Category() {Name = "Bilgisayar"},
            new Category() {Name = "Telefon"},
            new Category() {Name = "Elektronik"},
        };

        private static Product[] Products =
        {
            new Product() {Name = "Iphone 6", Price = 4000, ImageUrl = "1.jpg",Description = "<p>Güzel Telefon</p>"},
            new Product() {Name = "Iphone 7", Price = 5000, ImageUrl = "1.jpg",Description = "<p>Güzel Telefon</p>"},
            new Product() {Name = "Iphone 8", Price = 6000, ImageUrl = "1.jpg",Description = "<p>Güzel Telefon</p>"},
            new Product() {Name = "Iphone 11", Price = 8000, ImageUrl = "1.jpg",Description = "<p>Güzel Telefon</p>"},
            new Product() {Name = "Iphone 12", Price = 12000, ImageUrl = "1.jpg",Description = "<p>Güzel Telefon</p>"},
            new Product() {Name = "Samsung s36", Price = 36000, ImageUrl = "1.jpg",Description = "<p>Güzel Telefon</p>"},
        };

        private static ProductCategory[] ProductCategory =
        {
            new ProductCategory() {Product = Products[0], Category = Categories[0]},
            new ProductCategory() {Product = Products[0], Category = Categories[1]},
            new ProductCategory() {Product = Products[1], Category = Categories[0]},
            new ProductCategory() {Product = Products[1], Category = Categories[2]},
            new ProductCategory() {Product = Products[2], Category = Categories[0]},
            new ProductCategory() {Product = Products[2], Category = Categories[2]},
        };
    }
}
