using Microsoft.Maui.Controls;
using Shopping.App.DAL;
using Shopping.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.App.Service
{
    public class ProductService
    {
        private readonly AppDbContext _dbContext;


        // pass data across pages
        public Product CachedProduct { get; set; }


        public ProductService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();

            // InitTestData();
        }


        public IList<Product> GetProducts()
            => _dbContext.Products.ToList();


        public IList<Product> GetProducts(int page, int pageSize)
            => _dbContext.Products
                         .Skip(page * pageSize)
                         .Take(pageSize)
                         .ToList();  

        public IList<Product> FilterProducts(string name)
            => _dbContext.Products
                    .Where(p => p.Name.Contains(name))
                    .ToList();


        #region Test
        private void InitTestData()
        {
            var products = GetTestProducts().ToList();

            _dbContext.AddRange(products);
            _dbContext.SaveChanges();
        }

        private IEnumerable<Product> GetTestProducts()
        {
            yield return new Product() 
            {
                Name = "coca-cola",
                Price = 2.00,
                ImageUrl = "food_coke.png",
                // ImageData = GetBytes("food_coke.png"),
            };

            yield return new Product()
            {
                Name = "pepsi",
                Price = 1.80,
                ImageUrl = "food_coke.png",
                // ImageData = GetBytes("food_coke.png"),
            };

            yield return new Product()
            {
                Name = "coffee A",
                Price = 2.50,
                ImageUrl = "food_coffee.png",
                //ImageData = GetBytes("food_coffee.png"),
            };

            yield return new Product()
            {
                Name = "coffee B",
                Price = 3.40,
                ImageUrl = "food_coffee.png",
                //ImageData = GetBytes("food_coffee.png"),
            };

            yield return new Product()
            {
                Name = "chocolate",
                Price = 10.00,
                ImageUrl = "food_chocolate.png",
                // ImageData = GetBytes("food_chocolate.png"),
            };

            yield return new Product()
            {
                Name = "T-Shirt A",
                Price = 30.00,
                ImageUrl = "clothes_t_shirt.png",
                // ImageData = GetBytes("clothes_t_shirt.png"),
            };


            yield return new Product()
            {
                Name = "T-Shirt B",
                Price = 40.00,
                ImageUrl = "clothes_t_shirt.png",
                // ImageData = GetBytes("clothes_t_shirt.png"),
            };

            yield return new Product()
            {
                Name = "trouser A",
                Price = 25.00,
                ImageUrl = "clothes_trouser.png",
                // ImageData = GetBytes("clothes_trouser.png"),
            };


            yield return new Product()
            {
                Name = "trouser B",
                Price = 35.00,
                ImageUrl = "clothes_trouser.png",
                //ImageData = GetBytes("clothes_trouser.png"),
            };

            yield return new Product()
            {
                Name = "sweater A",
                Price = 50.00,
                ImageUrl = "clothes_sweater.png",
                // ImageData = GetBytes("clothes_sweater.png"),
            };

            yield return new Product()
            {
                Name = "sweater B",
                Price = 55.00,
                ImageUrl = "clothes_sweater.png",
                // ImageData = GetBytes("clothes_sweater.png"),
            };
        }

        private byte[] GetBytes(string file)
        {
            var dir = @"F:\PartTimeWork\dotnet\20230321\Shopping\Shopping.App\Resources\Images";
            return File.ReadAllBytes(Path.Combine(dir, file));
        }

        #endregion
    }
}
