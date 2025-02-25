using Microsoft.EntityFrameworkCore;
using TechApi.Model;
using TechApi.Repositories.Interface;
using TechApi.Services;

namespace TechApi.Repositories.Implementation
{
    public class InventoryRepositories : IInventoryRepositories
    {
        private readonly AppDbContext db;

        public InventoryRepositories(AppDbContext db)
        {
            this.db = db;
        }
        public async Task<Inventory> CreateAsync(Inventory inventory)
        {
            await db.inventories.AddAsync(inventory);
            await db.SaveChangesAsync();
            return inventory;
        }

        public async Task<Inventory?> DeleteAsync(int id)
        {
            var existingInventory = await db.inventories.FirstOrDefaultAsync(x => x.Id == id);
            if (existingInventory != null)
            {
                db.inventories.Remove(existingInventory);
                await db.SaveChangesAsync();
                return existingInventory;
            }
            return null;
        }

        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            return await db.inventories.ToListAsync();
        }

        public Task<Inventory?> GetByIdAsync(int id)
        {
            return db.inventories.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Inventory?> UpdateAsync(Inventory inventory)
        {
            var existingInventory = await db.inventories.FirstOrDefaultAsync(x => x.Id == inventory.Id);
            if (existingInventory == null)
            {
                return null;
            }

            //update BlogPost 
            db.Entry(existingInventory).CurrentValues.SetValues(inventory);



            await db.SaveChangesAsync();

            return inventory;
        }
    }
}
