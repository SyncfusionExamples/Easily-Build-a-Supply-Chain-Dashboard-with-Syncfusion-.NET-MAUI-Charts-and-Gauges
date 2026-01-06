using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SupplyChainDashboardSample
{
    public class InventoryDashboardViewModel : INotifyPropertyChanged
    {
        private bool _canStopTimer;
        
        private DateTime _currentTime;

        private readonly Random _random = new();

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

        public event PropertyChangedEventHandler? PropertyChanged;
        void Set<T>(ref T field, T value, [CallerMemberName] string? name = null)
        {
            if (!Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        public ObservableCollection<InventoryMovement> InventoryMovement { get; } = new();
        public ObservableCollection<TopItem> TopItems { get; } = new();
        public ObservableCollection<KpiSnapshot> Snapshots { get; } = new();

        private string _topItemsMetricPath = nameof(TopItem.Value);
        public string TopItemsMetricPath { get => _topItemsMetricPath; set => Set(ref _topItemsMetricPath, value); }
        public ICommand SetTopItemsMetricCommand { get; }

        public InventoryDashboardViewModel()
        {
            SetTopItemsMetricCommand = new Command<string>(p =>
            {
                TopItemsMetricPath = (p == "Quantity") ? nameof(TopItem.Quantity) : nameof(TopItem.Value);
            });

            SeedTopItems();
            InitializeSnapshots();
        }

        private void InitializeSnapshots()
        {
            Snapshots.Clear();

            var baseDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 9, 0, 0);
            for (int i = 0; i < 12; i++)
            {
                var timestamp = baseDate.AddHours(i);
                Snapshots.Add(CreateRandomSnapshot(timestamp));
            }

            if (Snapshots.Count > 0)
            {
                UpdateKpis(Snapshots[^1]);
            }
        }

        private KpiSnapshot CreateRandomSnapshot(DateTime timestamp)
        {
            var inventoryValue = Math.Round(_random.NextDouble() * (21000000 - 20000000) + 20000000);
            var inventoryChange = Math.Round(_random.NextDouble() * (2500000 - 1000000) + 1000000);
            var stockAvailableValue = Math.Round(_random.NextDouble() * (3900000 - 3700000) + 3700000);
            var stockAvailableChange = Math.Round(_random.NextDouble() * (60000 - 40000) + 40000);
            var turnoverRatio = Math.Round(_random.NextDouble() * (9.5 - 8.5) + 8.5, 2);
            var inventoryToSalesRatio = Math.Round(_random.NextDouble() * (0.5 - 0.4) + 0.4, 2);
            var avgInventoryDaysOfSupply = _random.Next(35, 43);
            var inventoryOverTimeValue = _random.Next(80, 120);
            var inventoryValueOverTimeChange = _random.Next(2, 26);
            var turnoverHoursValue = _random.Next(30, 55);
            var salesAmount = _random.Next(80, 90);
            var inventorySalesRatio = Math.Round(_random.NextDouble() * (5.5 - 4.5) + 4.5, 2);
            var increase = _random.Next(60, 70); 
            var decrease = -_random.Next(54, 57); 
            var total = increase + decrease;

            var snapshot = new KpiSnapshot
            {
                Timestamp = timestamp.AddHours(1),
                InventoryValue = inventoryValue,
                InventoryChange = $"Change: {inventoryChange:N0}",
                StockAvailableValue = stockAvailableValue,
                StockAvailableChange = $"Change: {stockAvailableChange:N0}",
                TurnoverRatioValue = turnoverRatio,
                InventoryToSalesRatioValue = inventoryToSalesRatio,
                AvgInventoryDaysOfSupplyValue = avgInventoryDaysOfSupply,
                InventoryOverTimeValue = inventoryOverTimeValue,
                InventoryValueOverTimeChange = inventoryValueOverTimeChange,
                TurnoverHoursValue = turnoverHoursValue,
                SalesAmount = salesAmount,
                InventorySalesRatio = inventorySalesRatio
            };

            InventoryMovement.Clear();
            InventoryMovement.Add(new InventoryMovement { MovementType = "Increase", MovementValue = increase, IsSummary = false });
            InventoryMovement.Add(new InventoryMovement { MovementType = "Decrease", MovementValue = decrease, IsSummary = false });
            InventoryMovement.Add(new InventoryMovement { MovementType = "Total", MovementValue = total, IsSummary = true });

            return snapshot;
        }

        private void UpdateKpis(KpiSnapshot snapshot)
        {
            AnimatedInventoryValue = snapshot.InventoryValue;
            AnimatedInventoryChange = snapshot.InventoryChange;
            AnimatedStockAvailable = snapshot.StockAvailableValue;
            AnimatedStockChange = snapshot.StockAvailableChange;
            AnimatedTurnoverRatio = snapshot.TurnoverRatioValue;
            AnimatedInventoryToSalesRatio = snapshot.InventoryToSalesRatioValue;
            AnimatedAvgDaysOfSupply = snapshot.AvgInventoryDaysOfSupplyValue;
        }

        private void SeedTopItems()
        {
            TopItems.Clear();
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

        public void StartTimer()
        {
            Snapshots.Clear();
            _currentTime = DateTime.Today.AddHours(9);

            for (int i = 0; i < 3; i++)
            {
                Snapshots.Add(CreateRandomSnapshot(_currentTime));
                _currentTime = _currentTime.AddHours(1);
            }

            _canStopTimer = false;

            Application.Current?.Dispatcher.StartTimer(
                TimeSpan.FromSeconds(0.5),
                () =>
                {
                    if (_canStopTimer) return false;

                    _currentTime = _currentTime.AddHours(1);
                    Snapshots.Add(CreateRandomSnapshot(_currentTime));

                    UpdateKpis(Snapshots[^1]);
                    return true;
                });
        }

        public void StopTimer()
        {
            _canStopTimer = true;
        }
    }
}
