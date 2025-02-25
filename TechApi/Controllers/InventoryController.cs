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
        public async Task<IActionResult> CreateRating([FromBody] CreateInventoryDto request)
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
        public async Task<IActionResult> GetAllRating()
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
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetBlogPostById([FromRoute] int id)
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
        // [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateBlogPostById([FromRoute] int id, CreateInventoryDto request)
        {
            //Convert DTO to Domain Model

            var inventory = new Inventory
            {

                ProductId = request.ProductId,
                ProductName = request.ProductName,
                StockAvailable = request.StockAvailable,
                ReorderStock = request.ReorderStock,


            };



            // call repository to update BlogPost domain model
            var updateRating = await inventoryRepository.UpdateAsync(inventory);

            if (updateRating == null)
            {
                return NotFound();
            }

            //convert domain model back to DTO
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

        [HttpDelete]
        [Route("{id:int}")]
        //   [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] int id)
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
