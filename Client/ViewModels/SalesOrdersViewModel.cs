using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class SalesOrdersViewModel : ViewModelBase
{
    private readonly ISalesOrdersService _salesOrderService;

    [ObservableProperty]
    public partial ObservableCollection<SalesOrder> SalesOrders { get; set; } = [];

    public SalesOrdersViewModel()
    {
        _salesOrderService = new SalesOrderService();
        PageTitle = "Beställningar";
        Init();
    }

    public SalesOrdersViewModel(ISalesOrdersService salesOrdersService)
    {
        _salesOrderService = salesOrdersService;
        PageTitle = "Beställningar";
        Init();
    }

    private async void Init()
    {
        await LoadSalesOrders();
    }

    private async Task LoadSalesOrders()
    {
        try
        {
            var orders = await _salesOrderService.List();
            SalesOrders = new ObservableCollection<SalesOrder>(orders);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
