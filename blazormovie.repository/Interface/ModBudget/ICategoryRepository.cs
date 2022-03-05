using blazormovie.Shared.SeedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.repository.Interface.ModBudget
{
    public interface ICategoryRepository
    {

        Task<IEnumerable<Category>> GetAll();
        Task<bool> Insert(Category category);
        Task<Category> GetById(int id);
        Task<bool> Delete(int id);
        Task<bool> Update(Category category);

    }
}
