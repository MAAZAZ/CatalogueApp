using CatalogueApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogueApp.Controllers
{   [Route("/api/categories")]
    public class CategoryRestController: Controller
    {
        public CatalogueDbRepository catalogueDbRepository { get; set; }

        public CategoryRestController(CatalogueDbRepository repository)
        {
            this.catalogueDbRepository = repository;
        }
        [HttpGet]
        public IEnumerable<Category> list()
        {
            return catalogueDbRepository.categories;
        }
        [HttpPost]
        public Category add([FromBody] Category category)
        {
            catalogueDbRepository.categories.Add(category);
            catalogueDbRepository.SaveChanges();
            return category;
        }
        [HttpGet("{id}")]
        public Category find(int id)
        {
            return catalogueDbRepository.categories.FirstOrDefault(s=> s.CategoryID==id);
        }
        [HttpGet("{id}/products")]
        public IEnumerable<Product> findProducts(int id)
        {
            Category category = catalogueDbRepository.categories.Include(c => c.products).FirstOrDefault(c => c.CategoryID == id);
            return category.products;
        }
        [HttpDelete("{id}")]
        public void delete(int id)
        {
            Category category=catalogueDbRepository.categories.Find(id);
            catalogueDbRepository.categories.Remove(category);
            catalogueDbRepository.SaveChanges();
        }
        [HttpPut("{id}")]
        public Category update(int id, [FromBody] Category category)
        {
            category.CategoryID = id;
            catalogueDbRepository.categories.Update(category);
            _ = catalogueDbRepository.SaveChanges();
            return category;
        }

    }
}
