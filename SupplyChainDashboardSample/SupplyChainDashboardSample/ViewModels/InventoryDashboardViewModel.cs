using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SupplyChainDashboardSample
{
    /// <summary>
    /// ViewModel for the supply chain inventory dashboard. Manages KPI values, chart series,
    /// top items, and provides change notifications to the UI.
    /// </summary>
    public partial class InventoryDashboardViewModel : INotifyPropertyChanged
    {
        private DateTime Date;
        private int count;
        private readonly Random random = new();
        private bool canStopTimer;

        private KpiCards? _kpis;
        private double _animatedInventoryValue;
        private string _animatedInventoryChange = string.Empty;
        private double _animatedStockAvailable;
        private string _animatedStockChange = string.Empty;
        private double _animatedTurnoverRatio;
        private double _animatedInventoryToSalesRatio;
        private string _topItemsMetricPath = nameof(TopItem.Value);
        private string _axisLabelFormat = "0$";

        /// <summary>
        /// Event raised when any bindable property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Sets a backing field and raises <see cref="PropertyChanged"/> when the value changes.
        /// </summary>
        /// <typeparam name="T">The field type.</typeparam>
        /// <param name="field">Reference to the backing field.</param>
        /// <param name="value">New value to assign.</param>
        /// <param name="name">Caller member name, supplied by the compiler.</param>
        void Set<T>(ref T field, T value, [CallerMemberName] string? name = null)
        {
            if (!Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// Current KPI card values displayed on the dashboard.
        /// </summary>
        public KpiCards? Kpis { get => _kpis; private set => Set(ref _kpis, value); }

        /// <summary>
        /// Time series for the Inventory Value Over Time chart.
        /// </summary>
        public ObservableCollection<InventoryValueOverTimePoint> InventoryValueOverTime { get; set; }

        /// <summary>
        /// Time series for the Turnover by Hour chart.
        /// </summary>
        public ObservableCollection<TurnoverByHourPoint> TurnoverByHour { get; set; }

        /// <summary>
        /// Time series for the Inventory to Sales Analysis chart.
        /// </summary>
        public ObservableCollection<InventoryToSalesAnalysisPoint> InventoryToSalesAnalysis { get; set; }

        /// <summary>
        /// Current inventory movement breakdown (increase/decrease/total).
        /// </summary>
        public ObservableCollection<InventoryMovement> InventoryMovement { get; } = new();

        /// <summary>
        /// Top items by value or quantity.
        /// </summary>
        public ObservableCollection<TopItem> TopItems { get; } = new();

        /// <summary>
        /// Animated KPI value bound to the Inventory Value card.
        /// </summary>
        public double AnimatedInventoryValue { get => _animatedInventoryValue; private set => Set(ref _animatedInventoryValue, value); }

        /// <summary>
        /// Animated KPI text for Inventory Value change.
        /// </summary>
        public string AnimatedInventoryChange { get => _animatedInventoryChange; private set => Set(ref _animatedInventoryChange, value); }

        /// <summary>
        /// Animated KPI value bound to the Stock Available card.
        /// </summary>
        public double AnimatedStockAvailable { get => _animatedStockAvailable; private set => Set(ref _animatedStockAvailable, value); }

        /// <summary>
        /// Animated KPI text for Stock Available change.
        /// </summary>
        public string AnimatedStockChange { get => _animatedStockChange; private set => Set(ref _animatedStockChange, value); }

        /// <summary>
        /// Animated KPI value for Turnover Ratio.
        /// </summary>
        public double AnimatedTurnoverRatio { get => _animatedTurnoverRatio; private set => Set(ref _animatedTurnoverRatio, value); }

        /// <summary>
        /// Animated KPI value for Inventory to Sales Ratio.
        /// </summary>
        public double AnimatedInventoryToSalesRatio { get => _animatedInventoryToSalesRatio; private set => Set(ref _animatedInventoryToSalesRatio, value); }

        /// <summary>
        /// Binding path used by the Top Items list (Value or Quantity).
        /// </summary>
        public string TopItemsMetricPath { get => _topItemsMetricPath; set => Set(ref _topItemsMetricPath, value); }

        /// <summary> 
        /// Format string applied to axis labels in charts. 
        /// </summary>
        public string AxisLabelFormat { get => _axisLabelFormat; set => Set(ref _axisLabelFormat, value); }

        /// <summary>
        /// Command that switches the <see cref="TopItemsMetricPath"/> between Value and Quantity.
        /// </summary>
        public ICommand SetTopItemsMetricCommand { get; }

        /// <summary>
        /// Initializes the ViewModel, chart collections, command handlers, and seeds TopItems.
        /// </summary>
        public InventoryDashboardViewModel()
        {
            InventoryValueOverTime = new ObservableCollection<InventoryValueOverTimePoint>();
            TurnoverByHour = new ObservableCollection<TurnoverByHourPoint>();
            InventoryToSalesAnalysis = new ObservableCollection<InventoryToSalesAnalysisPoint>();

            SetTopItemsMetricCommand = new Command<string>(p => 
            { 
                if (p == "Quantity") 
                { 
                    TopItemsMetricPath = nameof(TopItem.Quantity); 
                    AxisLabelFormat = "0,K";
                } 
                else 
                { 
                    TopItemsMetricPath = nameof(TopItem.Value); 
                    AxisLabelFormat = "0$";
                } 
            });

            TopItems.Add(new TopItem( "C100006", "C100006 - Cherry Finished Crystal Model", 14, 156000 ));
            TopItems.Add(new TopItem( "C100011", "C100011 - Winter Frost Vase", 13, 143000 ));
            TopItems.Add(new TopItem( "C100055", "C100055 - Silver Plated Photo Frame", 12, 132000 ));
            TopItems.Add(new TopItem( "C100009", "C100009 - Normandy Vase", 12, 120000 ));
            TopItems.Add(new TopItem( "C100010", "C100010 - Wisper-Cut Vase", 12, 118000 ));
            TopItems.Add(new TopItem( "C100040", "C100040 - Channel Speaker System", 9, 95000 ));
            TopItems.Add(new TopItem( "C100004", "C100004 - Walnut Medallion Plate", 8, 84000 ));
            TopItems.Add(new TopItem( "C100005", "C100005 - Cherry Finished Crystal Bowl", 8, 82000 ));
            TopItems.Add(new TopItem( "C100003", "C100003 - Cherry Finish Frame", 8, 81000 ));
            TopItems.Add(new TopItem( "C100051", "C100051 - Bamboo Digital Picture Frame", 8, 79000 ));
        }
    }
}
