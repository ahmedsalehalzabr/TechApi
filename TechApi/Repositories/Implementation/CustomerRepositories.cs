using Microsoft.EntityFrameworkCore;
using TechApi.Model;
using TechApi.Models;
using TechApi.Repositories.Interface;
using TechApi.Services;

namespace TechApi.Repositories.Implementation
{
    public class CustomerRepositories : ICustomerRepositories
    {
        private readonly AppDbContext db;

        public CustomerRepositories(AppDbContext db)
        {
            this.db = db;
        }
        public async Task<Customer> CreateAsync(Customer customer)
        {
            await db.customers.AddAsync(customer);
            await db.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer?> DeleteAsync(int id)
        {
            var existingCustomer = await db.customers.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCustomer != null)
            {
                db.customers.Remove(existingCustomer);
                await db.SaveChangesAsync();
                return existingCustomer;
            }
            return null;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await db.customers.ToListAsync();
        }

        public Task<Customer?> GetByIdAsync(int id)
        {
            return db.customers.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Customer?> UpdateAsync(Customer customer)
        {
            var existingCustomer = await db.customers.FirstOrDefaultAsync(x => x.Id == customer.Id);
            if (existingCustomer == null)
            {
                return null;
            }

            //update BlogPost 
            db.Entry(existingCustomer).CurrentValues.SetValues(customer);



            await db.SaveChangesAsync();

            return customer;
        }
    }
}
