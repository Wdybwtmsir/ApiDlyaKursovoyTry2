using System.Net.Http.Headers;
var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient(name: "Northwind.WebApi.Service",
 configureClient: options =>
 {
     options.BaseAddress = new("https://localhost:5091/");
     options.DefaultRequestHeaders.Accept.Add(
     new MediaTypeWithQualityHeaderValue(
     "application/json", 1.0));
 });
var app = builder.Build();



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
