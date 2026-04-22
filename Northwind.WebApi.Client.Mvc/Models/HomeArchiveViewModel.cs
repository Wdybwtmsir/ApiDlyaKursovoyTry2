using ApiDlyaKursovoyTry2.Models;

namespace Northwind.WebApi.Client.Mvc.Models;
public class HomeProductsViewModel
{
    public string? NameContains { get; set; }
    public Uri? BaseAddress { get; set; }
    public IEnumerable<Archive>? Archives { get; set; }
    public string? ErrorMessage { get; set; }
}