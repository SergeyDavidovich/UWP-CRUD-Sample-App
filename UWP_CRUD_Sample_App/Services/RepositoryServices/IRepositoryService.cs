using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionsWorkUWPTemplate10.Models;

namespace CollectionsWorkUWPTemplate10.Services.RepositoryServices
{
   public interface IRepositoryService<T> where T : class
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(string id);

        Task<List<T>> GetAllFavoritesASync();

        Task AddAsync(T entity);

        Task DeleteAsync(string id);

        Task UpdateAsync(T entity);
    }
}
