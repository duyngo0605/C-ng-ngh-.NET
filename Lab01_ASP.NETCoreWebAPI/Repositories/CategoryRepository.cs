using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public List<Category> GetCategories()
        {
            using (var context = new MyStoreContext())
            {
                return context.Categories.ToList();
            }
        }
    }

}
