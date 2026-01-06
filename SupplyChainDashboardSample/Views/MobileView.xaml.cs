using Syncfusion.Maui.Toolkit.Charts;

namespace SupplyChainDashboardSample;

public partial class MobileView : ContentPage
{
    private int _lastDay = -1;

    public MobileView()
    {
        InitializeComponent();
    }

    private void Primary_LabelCreated(object? sender, ChartAxisLabelEventArgs e)
    {
        DateTime baseDate = new(2026, 1, 1);
        var date = baseDate.AddHours(e.Position);

        ChartAxisLabelStyle labelStyle = new();

        if (date.Day != _lastDay)
        {
            labelStyle.LabelFormat = "MMM-dd";
            labelStyle.FontAttributes = FontAttributes.Bold;
            labelStyle.TextColor = Colors.LightGray;
            labelStyle.FontSize = 12;
            _lastDay = date.Day;
        }
        else
        {
            labelStyle.LabelFormat = "h tt";
            labelStyle.TextColor = Colors.LightGray;
            labelStyle.FontSize = 12;
        }

        e.LabelStyle = labelStyle;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as InventoryDashboardViewModel)?.StartTimer();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        (BindingContext as InventoryDashboardViewModel)?.StopTimer();
    }
}

