namespace SupplyChainDashboardSample
{
    /// <summary>
    /// Represents a single snapshot of KPI metrics captured at a specific time.
    /// </summary>
    public class KpiSnapshot
    {
        /// <summary>
        /// Gets or sets the timestamp for this KPI snapshot.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the total inventory value at the time of the snapshot.
        /// </summary>
        public double InventoryValue { get; set; }

        /// <summary>
        /// Gets or sets the formatted change string for the inventory value since the previous snapshot.
        /// Example: "Change: 125,000".
        /// </summary>
        public string InventoryChange { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the value of stock available at the time of the snapshot.
        /// </summary>
        public double StockAvailableValue { get; set; }

        /// <summary>
        /// Gets or sets the formatted change string for stock available since the previous snapshot.
        /// </summary>
        public string StockAvailableChange { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the inventory turnover ratio value.
        /// </summary>
        public double TurnoverRatioValue { get; set; }

        /// <summary>
        /// Gets or sets the inventory to sales ratio value.
        /// </summary>
        public double InventoryToSalesRatioValue { get; set; }

        /// <summary>
        /// Gets or sets the average number of days of supply available.
        /// </summary>
        public double AvgInventoryDaysOfSupplyValue { get; set; }

        /// <summary>
        /// Gets or sets the indexed inventory metric used for over-time charting.
        /// </summary>
        public double InventoryOverTimeValue { get; set; }

        /// <summary>
        /// Gets or sets the percent change for the inventory over time metric.
        /// </summary>
        public double InventoryValueOverTimeChange { get; set; }

        /// <summary>
        /// Gets or sets the turnover in hours value.
        /// </summary>
        public double TurnoverHoursValue { get; set; }

        /// <summary>
        /// Gets or sets the sales amount associated with this snapshot period.
        /// </summary>
        public double SalesAmount { get; set; }

        /// <summary>
        /// Gets or sets the inventory to sales ratio represented as a numeric value.
        /// </summary>
        public double InventorySalesRatio { get; set; }
    }

    /// <summary>
    /// Represents a single inventory movement item used for waterfall visualization.
    /// </summary>
    public class InventoryMovement
    {
        /// <summary>
        /// Gets or sets the movement category (e.g., Increase, Decrease, Total).
        /// </summary>
        public string MovementType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the numeric value of the movement. Decreases are expected to be negative.
        /// </summary>
        public double MovementValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this item represents a summary/total in a waterfall chart.
        /// </summary>
        public bool IsSummary { get; set; }
    }

    /// <summary>
    /// Represents a top-selling inventory item used to populate ranking lists and charts.
    /// </summary>
    public class TopItem
    {
        /// <summary>
        /// Gets or sets the unique product code.
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the display name of the inventory item.
        /// </summary>
        public string ItemName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the metric value used for ranking in charts (e.g., revenue or score).
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the quantity sold or moved for this inventory item.
        /// </summary>
        public int Quantity { get; set; }
    }
}
