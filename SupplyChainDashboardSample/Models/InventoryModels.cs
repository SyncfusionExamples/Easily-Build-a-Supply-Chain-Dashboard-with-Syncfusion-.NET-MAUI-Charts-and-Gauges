using System.Collections.ObjectModel;

namespace SupplyChainDashboardSample
{

    /// <summary>
    /// Represents a single inventory KPI with a value and optional bounds for visualizations.
    /// </summary>
    public class InventoryKpi
    {

        /// <summary>
        /// Display title of the KPI.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Numeric value of the KPI.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Optional supplemental text shown with the KPI.
        /// </summary>
        public string SubText { get; set; } = string.Empty;

        /// <summary>
        /// Suggested minimum bound for visualizers like gauges or charts.
        /// </summary>
        public double Min { get; set; } = 0;

        /// <summary>
        /// Suggested maximum bound for visualizers like gauges or charts.
        /// </summary>
        public double Max { get; set; } = 100;
    }

    /// <summary>
    /// Represents a monthly data point with optional secondary and derived metrics.
    /// </summary>
    public class MonthValue
    {

        /// <summary>
        /// Month label (e.g., Jan, Feb).
        /// </summary>
        public string Month { get; set; } = string.Empty;

        /// <summary>
        /// Primary numeric value for the month.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Optional secondary value for comparison plots (e.g., sales).
        /// </summary>
        public double Secondary { get; set; }

        /// <summary>
        /// Optional absolute change against previous value.
        /// </summary>
        public double Change { get; set; }

        /// <summary>
        /// Optional ratio metric used in combo charts.
        /// </summary>
        public double Ratio { get; set; }

        /// <summary>
        /// Flag to indicate if the item is an aggregate/summary bucket.
        /// </summary>
        public bool IsSummary { get; set; }
    }

    /// <summary>
    /// Represents an item contributing to inventory value or volume.
    /// </summary>
    public class TopItem
    {

        /// <summary>
        /// Unique item code/identifier.
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Human-readable item name or description.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Contribution amount (percentage of total when visualized as value share).
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Absolute quantity contributed by the item (used when Quantity is selected).
        /// </summary>
        public double Quantity { get; set; }
    }

    /// <summary>
    /// Provides deterministic demo data for the inventory dashboard visuals.
    /// </summary>
    public static class DemoInventoryData
    {

        /// <summary>
        /// Month labels used across demo time-series.
        /// </summary>
        private static readonly string[] Months = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        /// <summary>
        /// Builds a sequence of monthly inventory valuations including month-over-month change.
        /// </summary>
        public static ObservableCollection<MonthValue> BuildInventoryValue()
        {
            return new ObservableCollection<MonthValue> 
            { 
                new MonthValue { Month = "Jan", Value = 108, Change = 8 }, 
                new MonthValue { Month = "Feb", Value = 102, Change = 2 }, 
                new MonthValue { Month = "Mar", Value = 88, Change = 25 }, 
                new MonthValue { Month = "Apr", Value = 117, Change = 7 }, 
                new MonthValue { Month = "May", Value = 116, Change = 9 }, 
                new MonthValue { Month = "Jun", Value = 93, Change = 15}, 
                new MonthValue { Month = "Jul", Value = 84, Change = 25 }, 
                new MonthValue { Month = "Aug", Value = 102, Change = 8 }, 
                new MonthValue { Month = "Sep", Value = 119, Change = 7 }, 
                new MonthValue { Month = "Oct", Value = 119, Change = 6 }, 
                new MonthValue { Month = "Nov", Value = 91, Change = 18 }, 
                new MonthValue { Month = "Dec", Value = 97, Change = 10 }, 
            };
        }

        /// <summary>
        /// Builds monthly inventory turnover days values.
        /// </summary>
        public static ObservableCollection<MonthValue> BuildTurnoverDays()
        {
            return new ObservableCollection<MonthValue> 
            { 
                new MonthValue { Month = "Jan", Value = 64 }, 
                new MonthValue { Month = "Feb", Value = 32 }, 
                new MonthValue { Month = "Mar", Value = 32 }, 
                new MonthValue { Month = "Apr", Value = 59 }, 
                new MonthValue { Month = "May", Value = 56 }, 
                new MonthValue { Month = "Jun", Value = 44 }, 
                new MonthValue { Month = "Jul", Value = 64 }, 
                new MonthValue { Month = "Aug", Value = 38 }, 
                new MonthValue { Month = "Sep", Value = 66 }, 
                new MonthValue { Month = "Oct", Value = 43 }, 
                new MonthValue { Month = "Nov", Value = 41 }, 
            };
        }

        /// <summary>
        /// Builds a simple increase/decrease/total breakdown used for waterfall/summary visuals.
        /// </summary>
        public static ObservableCollection<MonthValue> BuildMovement()
        {
            return new ObservableCollection<MonthValue>
            {
                new MonthValue{ Month = "Increase", Value = 70 },
                new MonthValue{ Month = "Decrease", Value = -64 },
                new MonthValue{ Month = "Total", Value = 4, IsSummary = true },
            };
        }

        /// <summary>
        /// Builds paired monthly series for inventory value and sales plus a smoothed ratio.
        /// </summary>
        public static ObservableCollection<MonthValue> BuildInventoryToSales()
        {
            return new ObservableCollection<MonthValue> 
            { 
                new MonthValue { Month = "Jan", Value = 117, Secondary = 89, Ratio = 4.5 }, 
                new MonthValue { Month = "Feb", Value = 117, Secondary = 81, Ratio = 4.63 }, 
                new MonthValue { Month = "Mar", Value = 61, Secondary = 88, Ratio = 4.87 }, 
                new MonthValue { Month = "Apr", Value = 114, Secondary = 88, Ratio = 5.0 }, 
                new MonthValue { Month = "May", Value = 96, Secondary = 88, Ratio = 4.63 }, 
                new MonthValue { Month = "Jun", Value = 116, Secondary = 88, Ratio = 5.5 }, 
                new MonthValue { Month = "Jul", Value = 64, Secondary = 88, Ratio = 4.73 }, 
                new MonthValue { Month = "Aug", Value = 92, Secondary = 88, Ratio = 4.52 }, 
                new MonthValue { Month = "Sep", Value = 101, Secondary = 88, Ratio = 4.87 }, 
                new MonthValue { Month = "Oct", Value = 74, Secondary = 88, Ratio = 4.70 }, 
                new MonthValue { Month = "Nov", Value = 115, Secondary = 88, Ratio = 4.97 }, 
                new MonthValue { Month = "Dec", Value = 90, Secondary = 88, Ratio = 4.65 }, 
            };
        }

        /// <summary>
        /// Builds a ranked list of top items with contribution amounts.
        /// </summary>
        public static ObservableCollection<TopItem> BuildTopItems()
        {
            return new ObservableCollection<TopItem>
            {
                new TopItem { Code = "C100006", Name = "C100006 - Cherry Finished Crystal Model", Amount = 14, Quantity = 156000 },
                new TopItem { Code = "C100011", Name = "C100011 - Winter Frost Vase", Amount = 13, Quantity = 143000 },
                new TopItem { Code = "C100055", Name = "C100055 - Silver Plated Photo Frame", Amount = 12, Quantity = 132000 },
                new TopItem { Code = "C100009", Name = "C100009 - Normandy Vase", Amount = 12, Quantity = 120000 },
                new TopItem { Code = "C100010", Name = "C100010 - Wisper-Cut Vase", Amount = 12, Quantity = 118000 },
                new TopItem { Code = "C100040", Name = "C100040 - Channel Speaker System", Amount = 9, Quantity = 95000 },
                new TopItem { Code = "C100004", Name = "C100004 - Walnut Medallion Plate", Amount = 8, Quantity = 84000 },
                new TopItem { Code = "C100005", Name = "C100005 - Cherry Finished Crystal Bowl", Amount = 8, Quantity = 82000 },
                new TopItem { Code = "C100003", Name = "C100003 - Cherry Finish Frame", Amount = 8, Quantity = 81000 },
                new TopItem { Code = "C100051", Name = "C100051 - Bamboo Digital Picture Frame", Amount = 8, Quantity = 79000 },
            };
        }
    }
}