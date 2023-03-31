using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ProductRepository : GenericRepository <Product>
    {
        public List<Product> GetUrunler()
        {
            return base.GetAll().Include(x => x.Category).ToList();
        }

       
        DataContext db = new DataContext();

        public List<Product> GetPopularProduct()
        {
            return db.Products.Where(x => x.Popular == true).Take(3).ToList();
        }
    }
}
