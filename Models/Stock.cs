namespace StupidStockMarket.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public decimal CurrentPrice { get; set; }
    }
}