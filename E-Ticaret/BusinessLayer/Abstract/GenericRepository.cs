using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public class GenericRepository<T> : IRepository<T>, IDisposable where T : class, new()
    {
       DataContext db = new DataContext();  
       DbSet<T> data;

        public GenericRepository()
        {
                data=db.Set<T>();
        }
        public void Delete(T p)
        {
           data.Remove(p);
           db.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return db.Set<T>().AsQueryable();
        }

        public T GetById(int id)
        {
            return data.Find(id);
        }

        public void Insert(T p)
        {
            data.Add(p);
            db.SaveChanges();
        }

        public List<T> List()
        {
            return data.ToList();
        }

      
        public void Update(T p)
        {
            
            db.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
