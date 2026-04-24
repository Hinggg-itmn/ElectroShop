using ElectroShop.Models;

var builder = WebApplication.CreateBuilder(args);

// ── Đăng ký services ──────────────────────────────────────────────────────
builder.Services.AddControllersWithViews();

// Đăng ký session (dùng cho giỏ hàng)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Đăng ký mock data service (sau này thay bằng DbContext)
builder.Services.AddSingleton<ProductDataService>();

var app = builder.Build();

// ── Middleware pipeline ───────────────────────────────────────────────────
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/Index");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

// Xử lý 404
app.UseStatusCodePagesWithReExecute("/Error/{0}");

// ── Routes ────────────────────────────────────────────────────────────────
app.MapControllerRoute(
    name: "productDetail",
    pattern: "shop/product/{id}",
    defaults: new { controller = "Shop", action = "Detail" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
