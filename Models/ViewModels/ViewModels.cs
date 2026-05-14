namespace ElectroShop.Models.ViewModels;

// ── Home ──────────────────────────────────────────────────────────────────
public class HomeViewModel
{
    public List<Product> FeaturedProducts { get; set; } = new();
    public List<Product> BestSellers { get; set; } = new();
    public List<Product> NewArrivals { get; set; } = new();
}

// ── Shop ──────────────────────────────────────────────────────────────────
public class ShopViewModel
{
    public List<Product> Products { get; set; } = new();
    public List<string> Categories { get; set; } = new();

    // Bộ lọc hiện tại
    public string? Query { get; set; }
    public string? SelectedCategory { get; set; }
    public string? SortBy { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public int TotalResults => Products.Count;
}

// ── Product Detail ────────────────────────────────────────────────────────
public class ProductDetailViewModel
{
    public Product Product { get; set; } = null!;
    public List<Product> RelatedProducts { get; set; } = new();
}

// ── Cart ──────────────────────────────────────────────────────────────────
public class CartViewModel
{
    public List<CartItem> Items { get; set; } = new();
    public decimal SubTotal => Items.Sum(i => i.SubTotal);
    public decimal ShippingFee { get; set; } = 0m;   // Free shipping
    public decimal Total => SubTotal + ShippingFee;
    public int ItemCount => Items.Sum(i => i.Quantity);
}

// ── Checkout ──────────────────────────────────────────────────────────────
public class CheckoutViewModel
{
    // Billing info
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;

    // Payment method
    public string PaymentMethod { get; set; } = "cod"; // cod | card | momo

    // Summary
    public CartViewModel Cart { get; set; } = new();
}

// ── Contact ───────────────────────────────────────────────────────────────
public class ContactViewModel
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsSubmitted { get; set; }
}
public class LoginViewModel
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool RememberMe { get; set; }
    public string? ErrorMessage { get; set; }
}
public class RegisterViewModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}