using CatalogueApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogueApp.Services
{
    public class DbInit
    {
        public static void initData(CatalogueDbRepository catalogueDbRepository)
        {
            Console.WriteLine("Data Initialization");
            catalogueDbRepository.categories.Add(new Category { Name="Ordinateurs" });
            catalogueDbRepository.categories.Add(new Category { Name = "Imprimantes" });
            catalogueDbRepository.products.Add(new Product { Name = "Dell h5230", Price = 5000, categoryID=1 });
            catalogueDbRepository.products.Add(new Product { Name = "HP h30", Price = 3000, categoryID = 1 });
            catalogueDbRepository.products.Add(new Product { Name = "HP L350", Price = 2000, categoryID = 2 });
            catalogueDbRepository.SaveChanges();
        }
    }
}
