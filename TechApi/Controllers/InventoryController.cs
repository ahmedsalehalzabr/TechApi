using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using TechApi.DTOs;
using TechApi.Model;
using TechApi.Repositories.Interface;

namespace TechApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        //public ActionResult SaveInventoryData()
        //{
        //    SqlConnection connecion = new SqlConnection
        //    {
        //        ConnectionString = "Server=DESKTOP-2Q7HSLN\\SQLEXPRESS;Password=771143849;User Id=sa;Database=techDb;TrustServerCertificate=True;Persist Security Info=True;"
        //    };

        //    SqlCommand command = new SqlCommand
        //    {
        //        CommandText = "",
        //        CommandType = System.Data.CommandType.StoredProcedure,
        //        Connection = connecion
        //    };
        //    connecion.Open();
        //    command.ExecuteNonQuery();
        //    connecion.Close();

        //    return Ok("Inventory Data Saved!");
        //}
        private readonly IInventoryRepositories inventoryRepository;

        public InventoryController(IInventoryRepositories inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }


        [HttpPost]
        // [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateInventory([FromBody] CreateInventoryDto request)
        {
            var inventory = new Inventory
            {
                ProductId = request.ProductId,
                ProductName = request.ProductName,
                StockAvailable = request.StockAvailable,
                ReorderStock = request.ReorderStock,

            };

            inventory = await inventoryRepository.CreateAsync(inventory);

            var response = new InventoryDto
            {
                Id = inventory.Id,
                ProductId = inventory.ProductId,
                ProductName = inventory.ProductName,
                StockAvailable = inventory.StockAvailable,
                ReorderStock = inventory.ReorderStock,

            };

            return Ok(response);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllInventory()
        {
            var inventories = await inventoryRepository.GetAllAsync();

            var response = new List<InventoryDto>();
            foreach (var inventory in inventories)
            {
                response.Add(new InventoryDto
                {
                    Id = inventory.Id,
                    ProductId = inventory.ProductId,
                    ProductName = inventory.ProductName,
                    StockAvailable = inventory.StockAvailable,
                    ReorderStock = inventory.ReorderStock,

                });
            }

            return Ok(response);

        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetInventoryInventoryById([FromRoute] int id)
        {
            var inventories = await inventoryRepository.GetByIdAsync(id);

            if (inventories == null)
            {
                return NotFound();
            }

            //convert domain model to DTO
            var response = new InventoryDto
            {
                Id = inventories.Id,
                ProductId = inventories.ProductId,
                ProductName = inventories.ProductName,
                StockAvailable = inventories.StockAvailable,
                ReorderStock = inventories.ReorderStock,

            };
            return Ok(response);
        }




        
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateInventoryById([FromRoute] int id, CreateInventoryDto request)
        {
            var inventory = new Inventory
            {
                Id = id, // إصلاح الخطأ بتعيين الـ ID
                ProductId = request.ProductId,
                ProductName = request.ProductName,
                StockAvailable = request.StockAvailable,
                ReorderStock = request.ReorderStock,
            };

            var updatedInventory = await inventoryRepository.UpdateAsync(inventory);

            if (updatedInventory == null)
            {
                return NotFound();
            }

            var response = new InventoryDto
            {
                Id = updatedInventory.Id,
                ProductId = updatedInventory.ProductId,
                ProductName = updatedInventory.ProductName,
                StockAvailable = updatedInventory.StockAvailable,
                ReorderStock = updatedInventory.ReorderStock,
            };

            return Ok(response);
        }


        [HttpDelete]
        [Route("{id:int}")]
        //   [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteInventory([FromRoute] int id)
        {
            var deleteInventory = await inventoryRepository.DeleteAsync(id);

            if (deleteInventory == null) { return NotFound(); }

            //convert Domain model to DTO
            var response = new InventoryDto
            {
                Id = deleteInventory.Id,
                ProductId = deleteInventory.ProductId,
                ProductName = deleteInventory.ProductName,
                StockAvailable = deleteInventory.StockAvailable,
                ReorderStock = deleteInventory.ReorderStock,
            };
            return Ok(response);

        }
    }
}
