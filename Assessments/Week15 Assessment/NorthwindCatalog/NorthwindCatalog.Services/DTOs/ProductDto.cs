namespace NorthwindCatalog.Services.DTOs
{
    public class ProductDto
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }

        public decimal InventoryValue => UnitPrice * UnitsInStock;
    }

}
