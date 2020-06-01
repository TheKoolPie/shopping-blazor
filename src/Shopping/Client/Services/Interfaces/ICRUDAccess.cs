using Shopping.Shared.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Client.Services.Interfaces
{
    public interface ICRUDAccess<T>
    {
        string BaseAddress { get; }

        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(string id);
        Task<T> CreateAsync(T item);
        Task<T> UpdateAsync(string id, T item);
        Task<bool> DeleteAsync(T item);
        Task<bool> DeleteByIdAsync(string id);
    }
}
