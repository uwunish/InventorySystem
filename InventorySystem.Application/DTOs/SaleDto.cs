namespace InventorySystem.Application.DTOs
{
    public class SaleItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal LineTotal => Quantity * SalesPrice;
    }

    public class SaleDto
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string CreatedByName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<SaleItemDto> Items { get; set; } = new();
        public decimal TotalAmount => Items.Sum(i => i.LineTotal);
    }

    public class CreateSaleItemRequest
    {
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal SalesPrice { get; set; }
    }

    public class CreateSaleRequest
    {
        public int? CustomerId { get; set; }
        public List<CreateSaleItemRequest> Items { get; set; } = new();
    }
}