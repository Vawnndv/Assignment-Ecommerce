using System.Diagnostics.Contracts;

namespace Backend.Helpers
{
    public class QueryObject
    {
        public string? Search { get; set; } = null;
        public bool IsLatest { get; set; } = false;
        public bool IsDiscount { get; set; } = false;
        public String? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int PageLimit { get; set; } = 5; // This size for limit products (Lastest/ Discount) show on HomePage
    }
}
