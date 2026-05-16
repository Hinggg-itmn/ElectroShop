namespace ElectroShop.Models;

/// <summary>
/// Mock data service — thay bằng DbContext + EF Core khi kết nối database thật.
/// Giá đã đổi sang VND (đồng Việt Nam).
/// </summary>
public class ProductDataService
{
    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Apple iPad Mini G2356", Category = "Tablets",
            Brand = "Apple", Price = 27_500_000m, OldPrice = 32_500_000m,
            ImageUrl = "/img/product-1.png",
            Rating = 4.5, ReviewCount = 128, IsNew = true, IsBestSeller = true,
            Stock = 15, Description = "iPad Mini thế hệ mới với chip M2, màn hình Liquid Retina 8.3 inch." },

        new Product { Id = 2, Name = "Samsung Galaxy S23 Ultra", Category = "Smartphones",
            Brand = "Samsung", Price = 29_990_000m, OldPrice = 34_990_000m,
            ImageUrl = "/img/product-2.png",
            Rating = 4.8, ReviewCount = 342, IsBestSeller = true, IsFeatured = true,
            Stock = 8, Description = "Flagship cao cấp nhất của Samsung với camera 200MP." },

        new Product { Id = 3, Name = "Sony Alpha A7 IV", Category = "Cameras",
            Brand = "Sony", Price = 62_490_000m,
            ImageUrl = "/img/product-3.png",
            Rating = 4.9, ReviewCount = 89, IsFeatured = true,
            Stock = 5, Description = "Máy ảnh mirrorless full-frame 33MP chuyên nghiệp." },

        new Product { Id = 4, Name = "Apple Watch Series 9", Category = "Wearables",
            Brand = "Apple", Price = 9_990_000m, OldPrice = 12_490_000m,
            ImageUrl = "/img/product-4.png",
            Rating = 4.7, ReviewCount = 215, IsNew = true, IsBestSeller = true,
            Stock = 20, Description = "Smartwatch mạnh mẽ nhất của Apple với chip S9." },

        new Product { Id = 5, Name = "MacBook Pro 14\" M3", Category = "Laptops",
            Brand = "Apple", Price = 49_990_000m, OldPrice = 54_990_000m,
            ImageUrl = "/img/product-5.png",
            Rating = 4.9, ReviewCount = 456, IsFeatured = true,
            Stock = 12, Description = "Laptop chuyên nghiệp với chip M3 Pro siêu mạnh." },

        new Product { Id = 6, Name = "Sony WH-1000XM5", Category = "Audio",
            Brand = "Sony", Price = 8_690_000m, OldPrice = 9_990_000m,
            ImageUrl = "/img/product-6.png",
            Rating = 4.6, ReviewCount = 678, IsBestSeller = true,
            Stock = 30, Description = "Tai nghe chống ồn hàng đầu thế giới." },

        new Product { Id = 7, Name = "Canon EOS R50 Kit", Category = "Cameras",
            Brand = "Canon", Price = 21_990_000m, OldPrice = 24_990_000m,
            ImageUrl = "/img/product-7.png",
            Rating = 4.4, ReviewCount = 143, IsNew = true,
            Stock = 7, Description = "Máy ảnh mirrorless dành cho người mới bắt đầu." },

        new Product { Id = 8, Name = "Dell XPS 15 OLED", Category = "Laptops",
            Brand = "Dell", Price = 44_990_000m,
            ImageUrl = "/img/product-8.png",
            Rating = 4.5, ReviewCount = 201, IsFeatured = true,
            Stock = 9, Description = "Laptop cao cấp màn hình OLED 15.6 inch 4K." },

        new Product { Id = 9, Name = "Bose QuietComfort 45", Category = "Audio",
            Brand = "Bose", Price = 6_990_000m, OldPrice = 8_290_000m,
            ImageUrl = "/img/product-9.png",
            Rating = 4.5, ReviewCount = 389, IsBestSeller = true,
            Stock = 25, Description = "Tai nghe over-ear chống ồn cực kỳ thoải mái." },

        new Product { Id = 10, Name = "iPad Pro 12.9\" M2", Category = "Tablets",
            Brand = "Apple", Price = 32_490_000m, OldPrice = 37_490_000m,
            ImageUrl = "/img/product-10.png",
            Rating = 4.8, ReviewCount = 312, IsBestSeller = true, IsFeatured = true,
            Stock = 11, Description = "iPad Pro mạnh nhất với màn hình Liquid Retina XDR." },

        new Product { Id = 11, Name = "Samsung 4K QLED TV 55\"", Category = "TVs",
            Brand = "Samsung", Price = 22_490_000m, OldPrice = 27_490_000m,
            ImageUrl = "/img/product-11.png",
            Rating = 4.6, ReviewCount = 156, IsFeatured = true,
            Stock = 6, Description = "TV 4K QLED 55 inch với công nghệ Quantum Dot." },

        new Product { Id = 12, Name = "DJI Mini 4 Pro", Category = "Drones",
            Brand = "DJI", Price = 18_990_000m,
            ImageUrl = "/img/product-12.png",
            Rating = 4.7, ReviewCount = 98, IsNew = true,
            Stock = 14, Description = "Drone nhỏ gọn quay 4K/60fps chuyên nghiệp." },
    };

    public List<Product> GetAll() => _products;

    public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

    public List<Product> GetFeatured() => _products.Where(p => p.IsFeatured).Take(8).ToList();

    public List<Product> GetBestSellers() => _products.Where(p => p.IsBestSeller).Take(8).ToList();

    public List<Product> GetNewArrivals() => _products.Where(p => p.IsNew).Take(8).ToList();

    public List<string> GetCategories() =>
        _products.Select(p => p.Category).Distinct().OrderBy(c => c).ToList();

    public List<Product> Search(string? query, string? category, string? sortBy,
                                decimal? minPrice, decimal? maxPrice)
    {
        var result = _products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query))
            result = result.Where(p =>
                p.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                p.Brand.Contains(query, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(category))
            result = result.Where(p => p.Category == category);

        if (minPrice.HasValue) result = result.Where(p => p.Price >= minPrice);
        if (maxPrice.HasValue) result = result.Where(p => p.Price <= maxPrice);

        result = sortBy switch
        {
            "price_asc"  => result.OrderBy(p => p.Price),
            "price_desc" => result.OrderByDescending(p => p.Price),
            "rating"     => result.OrderByDescending(p => p.Rating),
            "newest"     => result.Where(p => p.IsNew).Concat(result.Where(p => !p.IsNew)),
            _            => result.OrderByDescending(p => p.IsBestSeller)
        };

        return result.ToList();
    }
}
