using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SupplyChainDashboardSample.Models;

namespace SupplyChainDashboardSample.ViewModels
{
    public class InventoryDashboardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        void Set<T>(ref T field, T value, [CallerMemberName] string? name = null)
        {
            if (!Equals(field, value)) { field = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); }
        }

        public ObservableCollection<InventoryKpi> Kpis { get; } = new()
        {
            new InventoryKpi{ Title = "INVENTORY VALUE", Value = 20068577, SubText = "Change: 1,076,396", Min=0, Max=30000000},
            new InventoryKpi{ Title = "STOCK AVAILABLE", Value = 3790813, SubText = "Change: 58,778", Min=0, Max=6000000},
            new InventoryKpi{ Title = "TURNOVER RATIO", Value = 9.13, SubText = string.Empty, Min=0, Max=20},
            new InventoryKpi{ Title = "INV. TO SALES RATIO", Value = 0.44, SubText = string.Empty, Min=0, Max=1},
            new InventoryKpi{ Title = "AVG. DAYS OF SUPPLY", Value = 39.98, SubText = string.Empty, Min=0, Max=90},
        };

        public ObservableCollection<MonthValue> InventoryValueOverTime { get; } = DemoInventoryData.BuildInventoryValue();
        public ObservableCollection<MonthValue> TurnoverDays { get; } = DemoInventoryData.BuildTurnoverDays();
        public ObservableCollection<MonthValue> InventoryMovement { get; } = DemoInventoryData.BuildMovement();
        public ObservableCollection<MonthValue> InventoryToSales { get; } = DemoInventoryData.BuildInventoryToSales();
        public ObservableCollection<TopItem> TopItems { get; } = DemoInventoryData.BuildTopItems();

        bool _valueMode = true;
        public bool ValueMode { get => _valueMode; set => Set(ref _valueMode, value); }
    }
}