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
    public class CategoryRepository : GenericRepository<Category>
    {
        public void Insert(CategoryRepository p)
        {
            throw new NotImplementedException();
        }

        DataContext db = new DataContext();

        public List<Product> CategoryDetails(int id)
        {
            
            return db.Products.Where(x => x.CategoryId == id).ToList();
        }
    }
}
