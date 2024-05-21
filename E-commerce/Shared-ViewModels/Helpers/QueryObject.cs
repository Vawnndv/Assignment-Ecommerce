using System.Net;

namespace Shared_ViewModels.Helpers
{
    public class QueryObject
    {
        public string? Search { get; set; } = null;
        public bool IsLatest { get; set; } = false;
        public bool IsDiscount { get; set; } = false;
        public String? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public int PageLimit { get; set; } = 5; // This size for limit products (Lastest/ Discount) show on HomePage
        public int MaxPrice { get; set; } = int.MaxValue;
        public int MinPrice { get; set; } = int.MinValue;
    }

    public static class QueryStringHelper
    {
        public static string ToQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + WebUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return string.Join("&", properties.ToArray());
        }
    }
}
