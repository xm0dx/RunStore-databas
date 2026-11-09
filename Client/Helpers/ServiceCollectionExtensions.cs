using System;
using Client.Enums;
using Client.Factories;
using Client.Interfaces;
using Client.Services;
using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Helpers;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection collection)
    {
        collection.AddSingleton<MainWindowViewModel>();
        collection.AddSingleton<PageViewFactory>();
        collection.AddTransient<HomeViewModel>();
        collection.AddTransient<ProductsViewModel>();
        collection.AddTransient<CustomersViewModel>();
        collection.AddTransient<AddProductViewModel>();
        collection.AddTransient<EditProductViewModel>();
        collection.AddTransient<SalesOrdersViewModel>();
        // generiska klasser registreras med typeof(...<>)
        collection.AddTransient(typeof(IProductService<>), typeof(ProductServices<>));
        collection.AddTransient(typeof(ICustomerService<>), typeof(CustomerServices<>));
        collection.AddTransient(typeof(ISalesOrdersService<>), typeof(SalesOrderService<>));
        collection.AddTransient(typeof(ISupplierService<>), typeof(SupplierServices<>));

        collection.AddSingleton<Func<PageViewsEnum, ViewModelBase>>(c => name => name switch
        {
            PageViewsEnum.AddProduct => c.GetRequiredService<AddProductViewModel>(),
            PageViewsEnum.Home => c.GetRequiredService<HomeViewModel>(),
            PageViewsEnum.Customers => c.GetRequiredService<CustomersViewModel>(),
            PageViewsEnum.Products => c.GetRequiredService<ProductsViewModel>(),
            PageViewsEnum.EditProduct => c.GetRequiredService<EditProductViewModel>(),
            PageViewsEnum.SalesOrder => c.GetRequiredService<SalesOrdersViewModel>(),
            _ => throw new InvalidOperationException()
        });
    }
}
