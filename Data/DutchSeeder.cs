

using DutchTreat.Controllers;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _ctx;
        private readonly ILogger<DutchSeeder> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<StoreUser> _userManager;

        public DutchSeeder(DutchContext ctx, 
            IWebHostEnvironment env, 
            UserManager<StoreUser> userManager, 
            ILogger<DutchSeeder> logger)
        {
            _ctx = ctx;
            _env = env;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            Console.WriteLine("SeedAsync method called.");
            _ctx.Database.EnsureCreated();


            var user = await _userManager.FindByEmailAsync("shawn@dutchtreat.com");
            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Shawn",
                    LastName = "Wildermuth",
                    UserName = "shawn@dutchtreat.com",
                    Email = "shawn@dutchtreat.com"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    Console.WriteLine($"Could not create new User in seeder. Errors: {errors}");
                }
            }


            if (!_ctx.Products.Any())
            {
                var filePath = Path.Combine(_env.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filePath);
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);

                _ctx.AddRange(products);

                var order = _ctx.Orders.Where(o => o.Id == 1).FirstOrDefault();

                if (order == null)
                {
                    order = new Order();
                    order.User = user;
                    order.Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 2,
                            UnitPrice = products.First().Price
                        }

                    };


                }
                _ctx.Orders.Add(order);



                _ctx.SaveChanges();
            }
        }
    }
}
