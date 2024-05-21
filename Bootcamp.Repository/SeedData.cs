﻿using Bootcamp.Repository.Categories;
using NetBootcamp.API.Categories;

namespace Bootcamp.Repository
{
    public class SeedData
    {
        public static void SeedDatabase(AppDbContext context)
        {
            context.Database.EnsureCreated(); // db var mı check ettik.

            if (context.Categories.Any())
            {
                return;
            }

            var categories = new[]
            {
                new Category{Id = Guid.NewGuid(), Name = "Electronics"},
                new Category{Id = Guid.NewGuid(), Name = "Clothing"},
                new Category{Id = Guid.NewGuid(), Name = "Grocery"},
                new Category{Id = Guid.NewGuid(), Name = "Books"},
                new Category{Id = Guid.NewGuid(), Name = "Furniture"}
            };

            context.Categories.AddRange(categories);

            context.SaveChanges();


        }
    }
}