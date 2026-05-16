namespace ElectroShop.Models;

/// <summary>
/// Helper định dạng tiền Việt Nam Đồng.
/// Dùng: @price.ToVnd()  →  "27.500.000 ₫"
/// </summary>
public static class CurrencyHelper
{
    public static string ToVnd(this decimal amount)
        => string.Format("{0:N0}", amount).Replace(",", ".") + " ₫";
}
