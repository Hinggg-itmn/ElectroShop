namespace ElectroShop.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? OldPrice { get; set; }          // Giá gốc (nếu đang sale)
    public string ImageUrl { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public int Stock { get; set; }
    public double Rating { get; set; }              // 0 – 5
    public int ReviewCount { get; set; }
    public bool IsNew { get; set; }
    public bool IsBestSeller { get; set; }
    public bool IsFeatured { get; set; }

    /// <summary>Phần trăm giảm giá (tự tính nếu có OldPrice)</summary>
    public int? DiscountPercent =>
        OldPrice.HasValue && OldPrice > 0
            ? (int)Math.Round((1 - Price / OldPrice.Value) * 100)
            : null;
}
