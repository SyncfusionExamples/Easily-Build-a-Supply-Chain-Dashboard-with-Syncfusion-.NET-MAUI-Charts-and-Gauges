namespace SupplyChainDashboardSample;

public partial class MobileView : ContentPage
{
    public MobileView()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        InventoryDashboardViewModel.StopTimer();
        InventoryDashboardViewModel.StartTimer();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (InventoryDashboardViewModel != null)
        {
            InventoryDashboardViewModel.StopTimer();
        }

        inventoryValueChart.Handler?.DisconnectHandler();
        turnoverChart.Handler?.DisconnectHandler();
        inventoryMovementChart.Handler?.DisconnectHandler();
        salesChart.Handler?.DisconnectHandler();
    }
}

