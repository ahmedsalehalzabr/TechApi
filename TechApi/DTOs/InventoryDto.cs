using System.ComponentModel.DataAnnotations;

namespace TechApi.DTOs
{
    public class InventoryDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int StockAvailable { get; set; }

        public int ReorderStock { get; set; }
    }
}
