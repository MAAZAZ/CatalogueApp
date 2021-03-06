﻿using CatalogueApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogueApp.Controllers
{   [Route("/api/products")]
    public class ProductRestController : Controller
    {
        public CatalogueDbRepository catalogueDbRepository { get; set; }

        public ProductRestController(CatalogueDbRepository repository)
        {
            this.catalogueDbRepository = repository;
        }
        [HttpGet]
        public IEnumerable<Product> list()
        {
            catalogueDbRepository.products.Include(p=>p.category);
            return catalogueDbRepository.products;
        }
        [HttpPost]
        public Product add([FromBody] Product product)
        {
            catalogueDbRepository.products.Add(product);
            catalogueDbRepository.SaveChanges();
            return product;
        }
        [HttpGet("search")]
        public IEnumerable<Product> search(string kw)
        {
            return catalogueDbRepository
                .products
                .Include(p => p.category)
                .Where(p=>p.Name.Contains(kw));
        }
        [HttpGet("paginate")]
        public IEnumerable<Product> page(int page, int size)
        {
            int skip = (page - 1) * size;
            return catalogueDbRepository.products.Include(p=>p.category).Skip(skip).Take(size);
        }
        [HttpGet("{id}")]
        public Product find(int id)
        {
            return catalogueDbRepository.products.Include(p=>p.category).FirstOrDefault(s=> s.ProductID==id);
        }
        [HttpDelete("{id}")]
        public void delete(int id)
        {
            Product product =catalogueDbRepository.products.Find(id);
            catalogueDbRepository.products.Remove(product);
            catalogueDbRepository.SaveChanges();
        }
        [HttpPut("{id}")]
        public Product update(int id, [FromBody] Product product)
        {
            product.ProductID = id;
            catalogueDbRepository.products.Update(product);
            catalogueDbRepository.SaveChanges();
            return product;
        }

    }
}
