using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application.DTOs
{
    public class PurchaseItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal LineTotal => Quantity * PurchasePrice;
    }

    public class PurchaseDto
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string CreatedByName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<PurchaseItemDto> Items { get; set; } = new();
        public decimal TotalAmount => Items.Sum(i => i.LineTotal);
    }

    public class CreatePurchaseItemRequest
    {
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalesPrice { get; set; }
    }

    public class CreatePurchaseRequest
    {
        public int VendorId { get; set; }
        public List<CreatePurchaseItemRequest> Items { get; set; } = new();
    }
}
