using SupplyChainDashboardSample.ViewModels;

namespace SupplyChainDashboardSample;

public partial class MainPage : ContentPage
{
    public MainPage(InventoryDashboardViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
