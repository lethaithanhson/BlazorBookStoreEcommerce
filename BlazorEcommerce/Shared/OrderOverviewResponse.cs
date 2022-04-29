using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class OrderOverviewResponse
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Product { get; set; }
        public string ProductImageUrl { get; set; }
        public int QuantityItems { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public decimal Turnover { get; set; }
    }
}
