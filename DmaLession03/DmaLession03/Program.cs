var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "account-profile",
    pattern: "ho-so-cua-toi/{id:int}",
    defaults: new
    {
        controller = "Account",
        action = "Profile"
    });

app.MapControllerRoute(
    name: "account",
    pattern: "account",
    defaults: new
    {
        controller = "Account",
        action = "Index"
    });

app.MapControllerRoute(
    name: "product-category",
    pattern: "san-pham/danh-muc/{categoryId:int}",
    defaults: new
    {
        controller = "Product",
        action = "Category"
    });

app.MapControllerRoute(
    name: "product-detail",
    pattern: "san-pham/{id:int}",
    defaults: new
    {
        controller = "Product",
        action = "Detail"
    });

app.MapControllerRoute(
    name: "product",
    pattern: "san-pham",
    defaults: new
    {
        controller = "Product",
        action = "Index"
    });


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();