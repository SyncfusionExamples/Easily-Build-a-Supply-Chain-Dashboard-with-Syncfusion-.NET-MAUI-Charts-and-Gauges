using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SupplyChainDashboardSample.Models;

namespace SupplyChainDashboardSample.ViewModels
{
    /// <summary>
    /// ViewModel that exposes inventory KPIs, time-series, and derived datasets
    /// used by the Inventory dashboard UI. Implements change notification for bindings.
    /// </summary>
    public class InventoryDashboardViewModel : INotifyPropertyChanged
    {
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
            new InventoryKpi{ Title = "TURNOVER RATIO", Value = 9.13, SubText = string.Empty, Min=0, Max=20},
            new InventoryKpi{ Title = "INV. TO SALES RATIO", Value = 0.44, SubText = string.Empty, Min=0, Max=1},
            new InventoryKpi{ Title = "AVG. DAYS OF SUPPLY", Value = 39.98, SubText = string.Empty, Min=0, Max=90},
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

        bool _valueMode = true;
        /// <summary>
        /// Toggles how certain visuals present data (e.g., absolute values vs. alternate mode).
        /// </summary>
        public bool ValueMode { get => _valueMode; set => Set(ref _valueMode, value); }
    }
}