using EshopAguekeng.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EshoAguekeng.Repository
{
    public class ProductRepository
    {
        private readonly EshopAguekengEntities db;
        public ProductRepository()
        {
            db = new EshopAguekengEntities();
        }
        public  Product Get(int id)
        {
            return db.Products.FirstOrDefault(x => x.Id == id);
        }
        public Product Get(string code)
        {
            return db.Products.FirstOrDefault(x => x.Code.ToLower() == code.ToLower());
        }

        public Product Add(Product Product)
        {
            if (Product == null)
                throw new ArgumentNullException(nameof(Product));
            var p = Get(Product.Code);
            if (p != null)
                throw new DuplicateWaitObjectException($"Product code {Product.Code} already exist ! ");
            db.Products.Add(Product);
            db.SaveChanges();
            return Product;
        }
        public Product Set(Product Product)
        {
            if (Product == null)
                throw new KeyNotFoundException(nameof(Product));
            var currentDb = new EshopAguekengEntities();
            var oldProduct = currentDb.Products.Find(Product.Id);
            if (oldProduct != null)
                throw new DuplicateWaitObjectException($"Product not exist ! ");

            var u = currentDb.Products.FirstOrDefault(x => x.Code.ToLower() == Product.Code.ToLower()) ;
            if (u != null && u.Id != oldProduct.Id)
                throw new DuplicateWaitObjectException($"Product code {Product.Code} already exist ! ");
            db.Entry(Product).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Product;
        }

        public Product Delete(int id)
        {
            var product = Get(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return product;
        }
        public IEnumerable<Product> Find(Func<Product, bool> predicate)
        {
            return db.Products.Where(predicate).ToArray();

        }
    }
}
