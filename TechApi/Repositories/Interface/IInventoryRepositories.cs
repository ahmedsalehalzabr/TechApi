using TechApi.Model;

namespace TechApi.Repositories.Interface
{
    public interface IInventoryRepositories
    {
        Task<Inventory> CreateAsync(Inventory inventory);

        Task<IEnumerable<Inventory>> GetAllAsync();

        Task<Inventory?> GetByIdAsync(int id);


        Task<Inventory?> UpdateAsync(Inventory inventory);

        Task<Inventory?> DeleteAsync(int id);
    }
}
