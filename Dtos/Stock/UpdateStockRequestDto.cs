using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetAPI.Dtos.Stock
{
    public class UpdateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol must be at most 10 characters long")]

        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(50, ErrorMessage = "CompanyName must be at most 50 characters long")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(0, 1000000, ErrorMessage = "Price must be between 0 and 1000000")]

        public decimal Price { get; set; }

        [Required]
        [Range(0.001, 100, ErrorMessage = "LastDiv must be between 0.001 and 100")]
        public decimal LastDiv { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Industry must be at most 50 characters long")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(0, 1000000, ErrorMessage = "MarketCap must be between 0 and 1000000")]
        public long MarketCap { get; set; }
    }
}