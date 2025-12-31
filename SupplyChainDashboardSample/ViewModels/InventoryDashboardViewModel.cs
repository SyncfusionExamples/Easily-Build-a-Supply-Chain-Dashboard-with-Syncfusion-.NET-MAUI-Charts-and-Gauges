using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SupplyChainDashboardSample
{

    /// <summary>
    /// ViewModel that exposes inventory KPIs, time-series, and derived datasets
    /// used by the Inventory dashboard UI. Implements change notification for bindings.
    /// </summary>
    public class InventoryDashboardViewModel : INotifyPropertyChanged
    {
        private readonly Random _rand = new Random();
        private IDispatcherTimer? _timer;

        public InventoryDashboardViewModel()
        {
            SetTopItemsMetricCommand = new Command<string>(p =>
            {
                TopItemsMetricPath = (p == "Quantity") ? "Quantity" : "Amount";
            });
        }

        /// <summary>
        /// Raised when a property value changes to notify the UI bindings.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Helper to set a backing field and raise <see cref="PropertyChanged"/> when the value actually changes.
        /// </summary>
        /// <typeparam name="T">Type of the backing field.</typeparam>
        /// <param name="field">Reference to the backing field to update.</param>
        /// <param name="value">New value to be assigned.</param>
        /// <param name="name">Caller-provided property name, auto-supplied by the compiler.</param>
        void Set<T>(ref T field, T value, [CallerMemberName] string? name = null)
        {
            if (!Equals(field, value)) { field = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); }
        }

        /// <summary>
        /// Key performance indicators displayed as summary cards in the dashboard header.
        /// </summary>
        public ObservableCollection<InventoryKpi> Kpis { get; } = new()
        {
            new InventoryKpi{ Title = "INVENTORY VALUE", Value = 20068577, SubText = "Change: 1,076,396", Min=0, Max=30000000},
            new InventoryKpi{ Title = "STOCK AVAILABLE", Value = 3790813, SubText = "Change: 58,778", Min=0, Max=6000000},
            new InventoryKpi{ Title = "TURNOVER RATIO", Value = 9.13, SubText = string.Empty, Min=0, Max=12},
            new InventoryKpi{ Title = "INV. TO SALES RATIO", Value = 0.44, SubText = string.Empty, Min=0, Max=1},
            new InventoryKpi{ Title = "AVG. DAYS OF SUPPLY", Value = 39.98, SubText = string.Empty, Min=0, Max=60},
        };

        /// <summary>
        /// Simulated monthly inventory valuation series used for trend charts.
        /// </summary>
        public ObservableCollection<MonthValue> InventoryValueOverTime { get; } = DemoInventoryData.BuildInventoryValue();

        /// <summary>
        /// Simulated monthly inventory turnover days used for performance tracking.
        /// </summary>
        public ObservableCollection<MonthValue> TurnoverDays { get; } = DemoInventoryData.BuildTurnoverDays();

        /// <summary>
        /// Simulated movement breakdown showing increases, decreases, and summary.
        /// </summary>
        public ObservableCollection<MonthValue> InventoryMovement { get; } = DemoInventoryData.BuildMovement();

        /// <summary>
        /// Simulated series correlating inventory values with sales and ratio.
        /// </summary>
        public ObservableCollection<MonthValue> InventoryToSales { get; } = DemoInventoryData.BuildInventoryToSales();

        /// <summary>
        /// Simulated list of top items contributing to inventory amount.
        /// </summary>
        public ObservableCollection<TopItem> TopItems { get; } = DemoInventoryData.BuildTopItems();

        // Top items metric switching (default is Value i.e., "Amount")
        string _topItemsMetricPath = "Amount";
        public string TopItemsMetricPath { get => _topItemsMetricPath; set => Set(ref _topItemsMetricPath, value); }

        // Command to switch between Amount and Quantity
        public ICommand SetTopItemsMetricCommand { get; }

        public void StartLiveUpdates()
        {
            if (_timer != null && _timer.IsRunning) return;
            _timer = Application.Current?.Dispatcher.CreateTimer();
            if (_timer == null) return;
            _timer.Interval = TimeSpan.FromSeconds(1.2);
            _timer.Tick += OnTick;
            _timer.Start();
        }

        public void StopLiveUpdates()
        {
            if (_timer == null) return;
            _timer.Tick -= OnTick;
            _timer.Stop();
            _timer = null;
        }

        private void OnTick(object? sender, EventArgs e) => UpdateData();

        private void UpdateData()
        {
            // KPIs - replace objects so bindings update
            ReplaceKpi(0, k =>
            {
                var delta = NextRange(-250_000, 250_000);
                var v = Clamp(k.Value + delta, k.Min, k.Max);
                return new InventoryKpi { Title = k.Title, Value = v, SubText = $"Change: {Math.Abs(delta):N0}", Min = k.Min, Max = k.Max };
            });

            ReplaceKpi(1, k =>
            {
                var delta = NextRange(-45_000, 45_000);
                var v = Clamp(k.Value + delta, k.Min, k.Max);
                return new InventoryKpi { Title = k.Title, Value = v, SubText = $"Change: {Math.Abs(delta):N0}", Min = k.Min, Max = k.Max };
            });

            ReplaceKpi(2, k =>
            {
                var delta = NextRangeDouble(-0.25, 0.25);
                var v = Clamp(k.Value + delta, k.Min, k.Max);
                return new InventoryKpi { Title = k.Title, Value = v, SubText = string.Empty, Min = k.Min, Max = k.Max };
            });

            ReplaceKpi(3, k =>
            {
                var delta = NextRangeDouble(-0.02, 0.02);
                var v = Clamp(k.Value + delta, k.Min, k.Max);
                return new InventoryKpi { Title = k.Title, Value = v, SubText = string.Empty, Min = k.Min, Max = k.Max };
            });

            ReplaceKpi(4, k =>
            {
                var delta = NextRangeDouble(-1.5, 1.5);
                var v = Clamp(k.Value + delta, k.Min, k.Max);
                return new InventoryKpi { Title = k.Title, Value = v, SubText = string.Empty, Min = k.Min, Max = k.Max };
            });

            // Inventory value over time
            for (int i = 0; i < InventoryValueOverTime.Count; i++)
            {
                var mv = InventoryValueOverTime[i];
                var newVal = Clamp(mv.Value + NextRange(-8, 8), 50, 140);
                var newChange = Clamp(mv.Change + NextRange(-5, 5), -30, 30);
                InventoryValueOverTime[i] = new MonthValue { Month = mv.Month, Value = newVal, Change = newChange };
            }

            // Turnover days by month
            for (int i = 0; i < TurnoverDays.Count; i++)
            {
                var mv = TurnoverDays[i];
                var newVal = Clamp(mv.Value + NextRange(-6, 6), 20, 70);
                TurnoverDays[i] = new MonthValue { Month = mv.Month, Value = newVal };
            }

            // Movement (waterfall)
            if (InventoryMovement.Count >= 3)
            {
                var inc = Clamp(InventoryMovement[0].Value + NextRange(-6, 6), 40, 90);
                var dec = Clamp(InventoryMovement[1].Value + NextRange(-6, 6), -90, -30);
                var total = inc + dec;
                InventoryMovement[0] = new MonthValue { Month = "Increase", Value = inc };
                InventoryMovement[1] = new MonthValue { Month = "Decrease", Value = dec };
                InventoryMovement[2] = new MonthValue { Month = "Total", Value = total, IsSummary = true };
            }

            // Inventory to Sales
            for (int i = 0; i < InventoryToSales.Count; i++)
            {
                var mv = InventoryToSales[i];
                var v = Clamp(mv.Value + NextRange(-6, 6), 50, 130);
                var s = Clamp(mv.Secondary + NextRange(-3, 3), 75, 100);
                var r = Clamp(mv.Ratio + NextRangeDouble(-0.08, 0.08), 4.2, 5.8);
                InventoryToSales[i] = new MonthValue { Month = mv.Month, Value = v, Secondary = s, Ratio = r };
            }

            // Top items
            for (int i = 0; i < TopItems.Count; i++)
            {
                var item = TopItems[i];
                var amt = Clamp(item.Amount + NextRangeDouble(-1.5, 1.5), 5, 20);
                var qty = Clamp(item.Quantity + NextRange(-4000, 4000), 60_000, 170_000);
                TopItems[i] = new TopItem { Code = item.Code, Name = item.Name, Amount = amt, Quantity = qty };
            }
        }

        private static double Clamp(double v, double min, double max) => v < min ? min : (v > max ? max : v);

        private int NextRange(int min, int max) => _rand.Next(min, max + 1);

        private double NextRangeDouble(double min, double max) => _rand.NextDouble() * (max - min) + min;

        private void ReplaceKpi(int index, Func<InventoryKpi, InventoryKpi> updater)
        {
            if (index < 0 || index >= Kpis.Count) return;
            var updated = updater(Kpis[index]);
            Kpis[index] = updated;
        }
    }
}
