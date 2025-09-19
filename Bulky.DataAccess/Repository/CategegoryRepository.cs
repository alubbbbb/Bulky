using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class CategegoryRepository : Repository<Category>, ICategoryRepository
    {
        ApplicationDbContext _db;
        public CategegoryRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

       

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
            _db.SaveChanges();

        }
    }
}
