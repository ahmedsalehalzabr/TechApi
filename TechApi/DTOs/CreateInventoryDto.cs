namespace TechApi.DTOs
{
    public class CreateInventoryDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int StockAvailable { get; set; }

        public int ReorderStock { get; set; }
    }
}
