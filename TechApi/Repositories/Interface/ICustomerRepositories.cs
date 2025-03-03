using TechApi.Model;
using TechApi.Models;

namespace TechApi.Repositories.Interface
{
    public interface ICustomerRepositories
    {
        Task<Customer> CreateAsync(Customer customer);

        Task<IEnumerable<Customer>> GetAllAsync();

        Task<Customer?> GetByIdAsync(int id);


        Task<Customer?> UpdateAsync(Customer customer);

        Task<Customer?> DeleteAsync(int id);
    }
}
