using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SupplyChainDashboardSample
{
    /// <summary>
    /// ViewModel backing the Inventory Dashboard. It exposes KPI snapshots, top items,
    /// and animated values for UI binding, as well as timer-driven updates.
    /// </summary>
    public class InventoryDashboardViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Backing flag used to stop the periodic timer loop.
        /// </summary>
        private bool _canStopTimer;
        
        /// <summary>
        /// Tracks the latest generated timestamp for snapshots.
        /// </summary>
        private DateTime _currentTime;

        /// <summary>
        /// Random generator used for seeding demo data.
        /// </summary>
        private readonly Random _random = new();

        private double _animatedInventoryValue;
        /// <summary>
        /// Gets the animated inventory value shown in the KPI card.
        /// </summary>
        public double AnimatedInventoryValue { get => _animatedInventoryValue; private set => Set(ref _animatedInventoryValue, value); }

        private string _animatedInventoryChange = string.Empty;
        /// <summary>
        /// Gets the formatted change text for the inventory value.
        /// </summary>
        public string AnimatedInventoryChange { get => _animatedInventoryChange; private set => Set(ref _animatedInventoryChange, value); }

        private double _animatedStockAvailable;
        /// <summary>
        /// Gets the animated stock available value.
        /// </summary>
        public double AnimatedStockAvailable { get => _animatedStockAvailable; private set => Set(ref _animatedStockAvailable, value); }

        private string _animatedStockChange = string.Empty;
        /// <summary>
        /// Gets the formatted change text for stock available.
        /// </summary>
        public string AnimatedStockChange { get => _animatedStockChange; private set => Set(ref _animatedStockChange, value); }

        private double _animatedTurnoverRatio;
        /// <summary>
        /// Gets the animated turnover ratio value.
        /// </summary>
        public double AnimatedTurnoverRatio { get => _animatedTurnoverRatio; private set => Set(ref _animatedTurnoverRatio, value); }

        private double _animatedInventoryToSalesRatio;
        /// <summary>
        /// Gets the animated inventory to sales ratio value.
        /// </summary>
        public double AnimatedInventoryToSalesRatio { get => _animatedInventoryToSalesRatio; private set => Set(ref _animatedInventoryToSalesRatio, value); }

        private double _animatedAvgDaysOfSupply;
        /// <summary>
        /// Gets the animated average days of supply value.
        /// </summary>
        public double AnimatedAvgDaysOfSupply { get => _animatedAvgDaysOfSupply; private set => Set(ref _animatedAvgDaysOfSupply, value); }

        /// <inheritdoc />
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Sets the given backing field and raises <see cref="PropertyChanged"/> when the value changes.
        /// </summary>
        /// <typeparam name="T">Type of the field.</typeparam>
        /// <param name="field">Reference to the backing field to update.</param>
        /// <param name="value">New value to assign.</param>
        /// <param name="name">Caller-supplied property name. Provided automatically.</param>
        void Set<T>(ref T field, T value, [CallerMemberName] string? name = null)
        {
            if (!Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// Collection used for inventory movement (waterfall) visualization.
        /// </summary>
        public ObservableCollection<InventoryMovement> InventoryMovement { get; } = new();

        /// <summary>
        /// Collection of top items for ranking and charts.
        /// </summary>
        public ObservableCollection<TopItem> TopItems { get; } = new();

        /// <summary>
        /// Historical KPI snapshots displayed by time-based charts.
        /// </summary>
        public ObservableCollection<KpiSnapshot> Snapshots { get; } = new();

        private string _topItemsMetricPath = nameof(TopItem.Value);
        /// <summary>
        /// Gets or sets the bound path used by UI to choose the metric displayed for TopItems (Value or Quantity).
        /// </summary>
        public string TopItemsMetricPath { get => _topItemsMetricPath; set => Set(ref _topItemsMetricPath, value); }

        /// <summary>
        /// Command to switch TopItems metric between Value and Quantity.
        /// </summary>
        public ICommand SetTopItemsMetricCommand { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryDashboardViewModel"/> class
        /// and seeds demo data collections.
        /// </summary>
        public InventoryDashboardViewModel()
        {
            SetTopItemsMetricCommand = new Command<string>(p =>
            {
                TopItemsMetricPath = (p == "Quantity") ? nameof(TopItem.Quantity) : nameof(TopItem.Value);
            });

            SeedTopItems();
            InitializeSnapshots();
        }

        /// <summary>
        /// Generates a daily series of KPI snapshots and updates the latest KPI cards.
        /// </summary>
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

        /// <summary>
        /// Creates and returns a randomized <see cref="KpiSnapshot"/> for the supplied timestamp.
        /// Also seeds the <see cref="InventoryMovement"/> collection for waterfall visualization.
        /// </summary>
        /// <param name="timestamp">The base timestamp used to generate the snapshot.</param>
        /// <returns>A populated <see cref="KpiSnapshot"/> instance.</returns>
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

        /// <summary>
        /// Updates the animated KPI values based on the provided snapshot.
        /// </summary>
        /// <param name="snapshot">The snapshot used as the source of KPI values.</param>
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

        /// <summary>
        /// Seeds the <see cref="TopItems"/> collection with sample data.
        /// </summary>
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

        /// <summary>
        /// Starts a repeating timer that appends new random snapshots and updates the KPI cards.
        /// </summary>
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

        /// <summary>
        /// Requests the running timer (if any) to stop.
        /// </summary>
        public void StopTimer()
        {
            _canStopTimer = true;
        }
    }
}
