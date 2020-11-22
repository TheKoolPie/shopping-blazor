using Shopping.Shared.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.Services.Interfaces
{
    public interface ICRUDAccess<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(string id);
        Task<T> CreateAsync(T item);
        Task<T> UpdateAsync(string id, T item);
        Task<bool> DeleteByIdAsync(string id);

        bool ItemAlreadyExists(T item);
        bool ItemCanBeUpdated(T item);
    }
}
