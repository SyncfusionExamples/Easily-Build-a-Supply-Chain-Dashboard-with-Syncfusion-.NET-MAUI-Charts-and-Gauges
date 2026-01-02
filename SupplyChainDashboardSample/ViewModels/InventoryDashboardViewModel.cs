using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SupplyChainDashboardSample
{
    public class InventoryDashboardViewModel : INotifyPropertyChanged
    {
        private double _animatedInventoryValue;
        public double AnimatedInventoryValue { get => _animatedInventoryValue; private set => Set(ref _animatedInventoryValue, value); }

        private string _animatedInventoryChange = string.Empty;
        public string AnimatedInventoryChange { get => _animatedInventoryChange; private set => Set(ref _animatedInventoryChange, value); }

        private double _animatedStockAvailable;
        public double AnimatedStockAvailable { get => _animatedStockAvailable; private set => Set(ref _animatedStockAvailable, value); }

        private string _animatedStockChange = string.Empty;
        public string AnimatedStockChange { get => _animatedStockChange; private set => Set(ref _animatedStockChange, value); }

        private double _animatedTurnoverRatio;
        public double AnimatedTurnoverRatio { get => _animatedTurnoverRatio; private set => Set(ref _animatedTurnoverRatio, value); }

        private double _animatedInventoryToSalesRatio;
        public double AnimatedInventoryToSalesRatio { get => _animatedInventoryToSalesRatio; private set => Set(ref _animatedInventoryToSalesRatio, value); }

        private double _animatedAvgDaysOfSupply;
        public double AnimatedAvgDaysOfSupply { get => _animatedAvgDaysOfSupply; private set => Set(ref _animatedAvgDaysOfSupply, value); }

        public InventoryDashboardViewModel()
        {
            SetTopItemsMetricCommand = new Command<string>(p =>
            {
                TopItemsMetricPath = (p == "Quantity") ? nameof(TopItem.Quantity) : nameof(TopItem.Value);
            });

            SeedSnapshots();
            SeedInventoryMovement();
            SeedTopItems();

            var latest = Snapshots.Count > 0 ? Snapshots[^1] : null;
            if (latest != null)
            {
                AnimatedInventoryValue = latest.InventoryValue;
                AnimatedInventoryChange = latest.InventoryChange;
                AnimatedStockAvailable = latest.StockAvailableValue;
                AnimatedStockChange = latest.StockAvailableChange;
                AnimatedTurnoverRatio = latest.TurnoverRatioValue;
                AnimatedInventoryToSalesRatio = latest.InventoryToSalesRatioValue;
                AnimatedAvgDaysOfSupply = latest.AvgInventoryDaysOfSupplyValue;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        void Set<T>(ref T field, T value, [CallerMemberName] string? name = null)
        {
            if (!Equals(field, value)) { field = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); }
        }

        public ObservableCollection<InventoryMovement> InventoryMovement { get; } = new();

        public ObservableCollection<TopItem> TopItems { get; } = new();

        public ObservableCollection<KpiSnapshot> Snapshots { get; } = new();

        private void SeedInventoryMovement()
        {
            InventoryMovement.Add(new InventoryMovement { MovementType = "Increase", MovementValue = 70, IsSummary = false });
            InventoryMovement.Add(new InventoryMovement { MovementType = "Decrease", MovementValue = -64, IsSummary = false });
            InventoryMovement.Add(new InventoryMovement { MovementType = "Total", MovementValue = 6, IsSummary = true });
        }

        private void SeedTopItems()
        {
            TopItems.Add(new TopItem { Code = "C100006", ItemName = "C100006 - Cherry Finished Crystal Model", Value = 14, Quantity = 156000 });
            TopItems.Add(new TopItem { Code = "C100011", ItemName = "C100011 - Winter Frost Vase", Value = 13, Quantity = 143000 });
            TopItems.Add(new TopItem { Code = "C100055", ItemName = "C100055 - Silver Plated Photo Frame", Value = 12, Quantity = 132000 });
            TopItems.Add(new TopItem { Code = "C100009", ItemName = "C100009 - Normandy Vase", Value = 12, Quantity = 120000 });
            TopItems.Add(new TopItem { Code = "C100010", ItemName = "C100010 - Wisper-Cut Vase", Value = 12, Quantity = 118000 });
            TopItems.Add(new TopItem { Code = "C100040", ItemName = "C100040 - Channel Speaker System", Value = 9, Quantity = 95000 });
            TopItems.Add(new TopItem { Code = "C100004", ItemName = "C100004 - Walnut Medallion Plate", Value = 8, Quantity = 84000 });
            TopItems.Add(new TopItem { Code = "C100005", ItemName = "C100005 - Cherry Finished Crystal Bowl", Value = 8, Quantity = 82000 });
            TopItems.Add(new TopItem { Code = "C100003", ItemName = "C100003 - Cherry Finish Frame", Value = 8, Quantity = 81000 });
            TopItems.Add(new TopItem { Code = "C100051", ItemName = "C100051 - Bamboo Digital Picture Frame", Value = 8, Quantity = 79000 });
        }

        private void SeedSnapshots()
        {
            Snapshots.Clear();
            Snapshots.Add(new KpiSnapshot { Timestamp = DateTime.Today.AddHours(9), InventoryValue = 20068577, InventoryChange = "Change: 1,076,396", StockAvailableValue = 3790813, StockAvailableChange = "Change: 58,778", TurnoverRatioValue = 9.10, InventoryToSalesRatioValue = 0.42, AvgInventoryDaysOfSupplyValue = 38, InventoryOverTimeValue = 108, InventoryValueOverTimeChange = 8, TurnoverHoursValue = 60, SalesAmount = 89, InventorySalesRatio = 4.50 });
            Snapshots.Add(new KpiSnapshot { Timestamp = DateTime.Today.AddHours(10), InventoryValue = 20078577, InventoryChange = "Change: 1,234,567", StockAvailableValue = 3795813, StockAvailableChange = "Change: 42,310", TurnoverRatioValue = 9.05, InventoryToSalesRatioValue = 0.43, AvgInventoryDaysOfSupplyValue = 37, InventoryOverTimeValue = 102, InventoryValueOverTimeChange = 2, TurnoverHoursValue = 32, SalesAmount = 81, InventorySalesRatio = 4.63 });
            Snapshots.Add(new KpiSnapshot { Timestamp = DateTime.Today.AddHours(11), InventoryValue = 20088577, InventoryChange = "Change: 1,345,678", StockAvailableValue = 3800813, StockAvailableChange = "Change: 58,778", TurnoverRatioValue = 9.00, InventoryToSalesRatioValue = 0.45, AvgInventoryDaysOfSupplyValue = 36, InventoryOverTimeValue = 88, InventoryValueOverTimeChange = 25, TurnoverHoursValue = 32, SalesAmount = 88, InventorySalesRatio = 4.87 });
            Snapshots.Add(new KpiSnapshot { Timestamp = DateTime.Today.AddHours(12), InventoryValue = 20098577, InventoryChange = "Change: 1,456,789", StockAvailableValue = 3805813, StockAvailableChange = "Change: 42,310", TurnoverRatioValue = 9.20, InventoryToSalesRatioValue = 0.46, AvgInventoryDaysOfSupplyValue = 39, InventoryOverTimeValue = 117, InventoryValueOverTimeChange = 7, TurnoverHoursValue = 55, SalesAmount = 88, InventorySalesRatio = 5.00 });
            Snapshots.Add(new KpiSnapshot { Timestamp = DateTime.Today.AddHours(13), InventoryValue = 20108577, InventoryChange = "Change: 1,567,890", StockAvailableValue = 3810813, StockAvailableChange = "Change: 58,778", TurnoverRatioValue = 9.15, InventoryToSalesRatioValue = 0.44, AvgInventoryDaysOfSupplyValue = 40, InventoryOverTimeValue = 116, InventoryValueOverTimeChange = 9, TurnoverHoursValue = 56, SalesAmount = 88, InventorySalesRatio = 4.63 });
            Snapshots.Add(new KpiSnapshot { Timestamp = DateTime.Today.AddHours(14), InventoryValue = 20118577, InventoryChange = "Change: 1,678,901", StockAvailableValue = 3815813, StockAvailableChange = "Change: 42,310", TurnoverRatioValue = 8.95, InventoryToSalesRatioValue = 0.48, AvgInventoryDaysOfSupplyValue = 41, InventoryOverTimeValue = 93, InventoryValueOverTimeChange = 15, TurnoverHoursValue = 44, SalesAmount = 88, InventorySalesRatio = 5.50 });
            Snapshots.Add(new KpiSnapshot { Timestamp = DateTime.Today.AddHours(15), InventoryValue = 20128577, InventoryChange = "Change: 1,789,012", StockAvailableValue = 3820813, StockAvailableChange = "Change: 58,778", TurnoverRatioValue = 8.90, InventoryToSalesRatioValue = 0.47, AvgInventoryDaysOfSupplyValue = 42, InventoryOverTimeValue = 84, InventoryValueOverTimeChange = 25, TurnoverHoursValue = 58, SalesAmount = 88, InventorySalesRatio = 4.73 });
            Snapshots.Add(new KpiSnapshot { Timestamp = DateTime.Today.AddHours(16), InventoryValue = 20138577, InventoryChange = "Change: 1,890,123", StockAvailableValue = 3825813, StockAvailableChange = "Change: 42,310", TurnoverRatioValue = 9.05, InventoryToSalesRatioValue = 0.43, AvgInventoryDaysOfSupplyValue = 40, InventoryOverTimeValue = 102, InventoryValueOverTimeChange = 8, TurnoverHoursValue = 38, SalesAmount = 88, InventorySalesRatio = 4.52 });
            Snapshots.Add(new KpiSnapshot { Timestamp = DateTime.Today.AddHours(17), InventoryValue = 20148577, InventoryChange = "Change: 1,901,234", StockAvailableValue = 3830813, StockAvailableChange = "Change: 58,778", TurnoverRatioValue = 9.25, InventoryToSalesRatioValue = 0.45, AvgInventoryDaysOfSupplyValue = 39, InventoryOverTimeValue = 119, InventoryValueOverTimeChange = 7, TurnoverHoursValue = 59, SalesAmount = 88, InventorySalesRatio = 4.87 });
            Snapshots.Add(new KpiSnapshot { Timestamp = DateTime.Today.AddHours(18), InventoryValue = 20158577, InventoryChange = "Change: 2,012,345", StockAvailableValue = 3835813, StockAvailableChange = "Change: 42,310", TurnoverRatioValue = 9.10, InventoryToSalesRatioValue = 0.44, AvgInventoryDaysOfSupplyValue = 38, InventoryOverTimeValue = 119, InventoryValueOverTimeChange = 6, TurnoverHoursValue = 43, SalesAmount = 88, InventorySalesRatio = 4.70 });
            Snapshots.Add(new KpiSnapshot { Timestamp = DateTime.Today.AddHours(19), InventoryValue = 20168577, InventoryChange = "Change: 2,123,456", StockAvailableValue = 3840813, StockAvailableChange = "Change: 58,778", TurnoverRatioValue = 8.85, InventoryToSalesRatioValue = 0.46, AvgInventoryDaysOfSupplyValue = 37, InventoryOverTimeValue = 91, InventoryValueOverTimeChange = 18, TurnoverHoursValue = 41, SalesAmount = 88, InventorySalesRatio = 4.97 });
            Snapshots.Add(new KpiSnapshot { Timestamp = DateTime.Today.AddHours(20), InventoryValue = 20178577, InventoryChange = "Change: 2,234,567", StockAvailableValue = 3845813, StockAvailableChange = "Change: 42,310", TurnoverRatioValue = 8.95, InventoryToSalesRatioValue = 0.45, AvgInventoryDaysOfSupplyValue = 36, InventoryOverTimeValue = 97, InventoryValueOverTimeChange = 10, TurnoverHoursValue = 48, SalesAmount = 88, InventorySalesRatio = 4.65 });
        }

        private string _topItemsMetricPath = nameof(TopItem.Value);

        public string TopItemsMetricPath { get => _topItemsMetricPath; set => Set(ref _topItemsMetricPath, value); }

        public ICommand SetTopItemsMetricCommand { get; }
    }
}
