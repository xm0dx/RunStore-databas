using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class AddProductViewModel : ViewModelBase
{
    private readonly IProductService<Product> _productService;
    private readonly ISupplierService<Supplier> _supplierService;

    [ObservableProperty]
    public partial string ItemNumber { get; set; } = string.Empty;
    [ObservableProperty]
    public partial string ProductName { get; set; } = string.Empty;
    [ObservableProperty]
    public partial string Supplier { get; set; } = string.Empty;
    [ObservableProperty]
    public partial string Price { get; set; } = "";
    [ObservableProperty]
    public partial Supplier? SelectedSupplier { get; set; } = new() { Name = "" };
    [ObservableProperty]
    public partial ObservableCollection<Supplier> Suppliers { get; set; } = [];

    // Endast för design och preview hantering
    /* Ta bort när allt fungerar!!! */
    // TODO: Ta bort när allt fungerar...
    public AddProductViewModel()
    {
        _productService = new ProductServices<Product>(new SupplierServices<Supplier>());
        _supplierService = new SupplierServices<Supplier>();
        PageTitle = "Ny Produkt";
        Init();
    }
    /*********************************************/

    public AddProductViewModel(IProductService<Product> productService, ISupplierService<Supplier> supplierService)
    {
        _productService = productService;
        _supplierService = supplierService;

        PageTitle = "Ny Produkt";
        Init();
    }

    private async void Init()
    {
        Suppliers = new ObservableCollection<Supplier>(await _supplierService.ListAll());
    }

    [RelayCommand]
    private async Task Save()
    {
        Product product = new()
        {
            ItemNumber = ItemNumber,
            Name = ProductName,
            Supplier = SelectedSupplier!, // den som valdes i comboboxen
            ImageUrl = "https://res.cloudinary.com/softtech-dev/image/upload/v1773483409/no-image_ymlpwk.png",
            Description = "Ny Produkt",
            Price = int.TryParse(Price, out int price) ? price : 0
        };

        await _productService.Add(product);

        ClearData();
    }

    private void ClearData()
    {
        ItemNumber = string.Empty;
        ProductName = string.Empty;
        Supplier = string.Empty;
        SelectedSupplier = null;
        Price = "";
    }
}
