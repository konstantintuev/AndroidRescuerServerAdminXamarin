using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServeAdmin
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<string> SendNotification(string title, string description, bool important);
    }
}
