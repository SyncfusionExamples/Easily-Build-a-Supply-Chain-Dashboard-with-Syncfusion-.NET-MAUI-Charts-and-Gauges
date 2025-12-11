using System;
using System.Collections.ObjectModel;

namespace SupplyChainDashboardSample.Models
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
        /// Contribution amount (percentage or absolute depending on context).
        /// </summary>
        public double Amount { get; set; }
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
            var rand = new Random(7);
            var data = new ObservableCollection<MonthValue>();
            double? prev = null;
            foreach (var m in Months)
            {
                var val = rand.Next(80, 120);
                var item = new MonthValue { Month = m, Value = val };
                item.Change = prev.HasValue ? Math.Abs(val - prev.Value) : 0;
                data.Add(item);
                prev = val;
            }
            return data;
        }

        /// <summary>
        /// Builds monthly inventory turnover days values.
        /// </summary>
        public static ObservableCollection<MonthValue> BuildTurnoverDays()
        {
            var rand = new Random(42);
            var data = new ObservableCollection<MonthValue>();
            foreach (var m in Months)
            {
                data.Add(new MonthValue { Month = m, Value = rand.Next(15, 85) });
            }
            return data;
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
            var rand = new Random(99);
            var data = new ObservableCollection<MonthValue>();
            for (int i = 0; i < Months.Length; i++)
            {
                var month = Months[i];
                var inventoryValue = rand.Next(60, 120);
                var salesAmount = rand.Next(40, 90);
                var ratio = 5.0 + 1.0 * Math.Sin((i - 6) * (Math.PI / 6));

                data.Add(new MonthValue
                {
                    Month = month,
                    Value = inventoryValue,
                    Secondary = salesAmount,
                    Ratio = Math.Round(ratio, 2)
                });
            }
            return data;
        }

        /// <summary>
        /// Builds a ranked list of top items with contribution amounts.
        /// </summary>
        public static ObservableCollection<TopItem> BuildTopItems()
        {
            return new ObservableCollection<TopItem>
            {
                new TopItem { Code = "C100006", Name = "C100006 - Cherry Finished Crystal Model", Amount = 0.14 },
                new TopItem { Code = "C100011", Name = "C100011 - Winter Frost Vase", Amount = 0.13 },
                new TopItem { Code = "C100055", Name = "C100055 - Silver Plated Photo Frame", Amount = 0.12 },
                new TopItem { Code = "C100009", Name = "C100009 - Normandy Vase", Amount = 0.12 },
                new TopItem { Code = "C100010", Name = "C100010 - Wisper-Cut Vase", Amount = 0.12 },
                new TopItem { Code = "C100040", Name = "C100040 - Channel Speaker System", Amount = 0.09 },
                new TopItem { Code = "C100004", Name = "C100004 - Walnut Medallion Plate", Amount = 0.08 },
                new TopItem { Code = "C100005", Name = "C100005 - Cherry Finished Crystal Bowl", Amount = 0.08 },
                new TopItem { Code = "C100003", Name = "C100003 - Cherry Finish Frame", Amount = 0.08 },
                new TopItem { Code = "C100051", Name = "C100051 - Bamboo Digital Picture Frame", Amount = 0.08 },
            };
        }
    }
}