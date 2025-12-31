namespace SupplyChainDashboardSample;

public partial class MobileView : ContentPage
{
    private InventoryDashboardViewModel? _vm;

    public MobileView()
    {
        InitializeComponent();
        _vm = BindingContext as InventoryDashboardViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        (_vm ??= BindingContext as InventoryDashboardViewModel)?.StartLiveUpdates();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        (_vm ??= BindingContext as InventoryDashboardViewModel)?.StopLiveUpdates();
    }
}
