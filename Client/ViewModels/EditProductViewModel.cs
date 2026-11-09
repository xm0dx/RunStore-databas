using System;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Client.Factories;
using Client.Helpers;
using Client.Interfaces;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class EditProductViewModel : ViewModelBase
{
    private readonly IProductService<Product>? _productService;
    private readonly ISupplierService<Supplier> _supplierService;
    private readonly PageViewFactory? _pageViewFactory;
    private readonly MainWindowViewModel _mainViewModel;

    private string _itemNumber = string.Empty;
    private string _imageUrl = string.Empty;
    private string _objectId = string.Empty;

    [ObservableProperty]
    public partial string ItemNumber { get; set; } = string.Empty;
    [ObservableProperty]
    public partial string ProductName { get; set; } = string.Empty;
    [ObservableProperty]
    public partial string Supplier { get; set; } = string.Empty;
    [ObservableProperty]
    public partial int Price { get; set; }
    [ObservableProperty]
    public partial Bitmap? Image { get; set; } = null;

    // Endast för design och preview hantering
    /* Ta bort när allt fungerar!!! */
    // TODO: Ta bort när allt fungerar...
    public EditProductViewModel()
    {
        _productService = new ProductServices<Product>(new SupplierServices<Supplier>());
        _supplierService = new SupplierServices<Supplier>();
    }

    public EditProductViewModel(IProductService<Product> productService, ISupplierService<Supplier> supplierService, PageViewFactory pageViewFactory, MainWindowViewModel mainWindowViewModel)
    {
        _productService = productService;
        _supplierService = supplierService;
        _pageViewFactory = pageViewFactory;
        _mainViewModel = mainWindowViewModel;
        PageTitle = "Uppdatera produkt";
    }

    public async void Init(string itemNumber)
    {
        _itemNumber = itemNumber;
        await GetProduct();
    }

    private async Task GetProduct()
    {
        if (_productService is not null)
        {
            var product = await _productService.Find(_itemNumber);
            _imageUrl = string.IsNullOrEmpty(product.ImageUrl) ?
            "https://res.cloudinary.com/softtech-dev/image/upload/v1773483409/no-image_ymlpwk.png" :
            product.ImageUrl;

            ItemNumber = _itemNumber;
            ProductName = product.Name;
            Supplier = product.Supplier.Name;
            Price = product.Price;
            Image = await ImageHelper.ImageFromUrl(_imageUrl);
        }
    }

    [RelayCommand]
    private async Task Save()
    {
        Supplier supplier = await _supplierService.Find(Supplier);
        if (Supplier is null) Console.WriteLine("Hittade inte leverantören");

        Product product = new()
        {
            ItemNumber = ItemNumber,
            Name = ProductName,
            Supplier = supplier,
            ImageUrl = _imageUrl,
            Description = "Uppdaterat produkten",
            Price = Price
        };

        await _productService!.Update(product);
        _mainViewModel.CurrentView = _pageViewFactory!.LoadPageViewModel(Enums.PageViewsEnum.Products);
    }
}
