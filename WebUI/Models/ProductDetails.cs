using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;

namespace WebUI.Models
{
    public class ProductDetails
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}
