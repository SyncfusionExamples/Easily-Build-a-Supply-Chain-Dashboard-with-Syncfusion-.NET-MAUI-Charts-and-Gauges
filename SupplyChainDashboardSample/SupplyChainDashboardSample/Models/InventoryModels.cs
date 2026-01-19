namespace SupplyChainDashboardSample
{
    /// <summary>
    /// Represents the current values displayed in KPI cards on the dashboard.
    /// </summary>
    public class KpiCards
    {
        /// <summary>
        /// Total inventory value.
        /// </summary>
        public double InventoryValue { get; set; }

        /// <summary>
        /// Formatted text describing the inventory value change.
        /// </summary>
        public string InventoryChange { get; set; } = string.Empty;

        /// <summary>
        /// Quantity/value available in stock.
        /// </summary>
        public double StockAvailableValue { get; set; }

        /// <summary>
        /// Formatted text describing the stock available change.
        /// </summary>
        public string StockAvailableChange { get; set; } = string.Empty;

        /// <summary>
        /// Inventory turnover ratio.
        /// </summary>
        public double TurnoverRatio { get; set; }

        /// <summary>
        /// Ratio of inventory to sales.
        /// </summary>
        public double InventoryToSalesRatio { get; set; }
    }

    /// <summary>
    /// Snapshot point for the Inventory Value Over Time series.
    /// </summary>
    /// <param name="timestamp">Timestamp for the data point.</param>
    /// <param name="inventoryValue">Inventory value at the timestamp.</param>
    /// <param name="valueChange">Change value associated with that timestamp.</param>
    public class InventoryValueOverTimePoint(DateTime timestamp, double inventoryValue, double valueChange)
    {
        /// <summary>
        /// The timestamp for the point.
        /// </summary>
        public DateTime Timestamp { get; set; } = timestamp;

        /// <summary>
        /// The inventory value at this point in time.
        /// </summary>
        public double InventoryOverTimeValue { get; set; } = inventoryValue;

        /// <summary>
        /// The change in inventory value at this point in time.
        /// </summary>
        public double InventoryValueOverTimeChange { get; set; } = valueChange;
    }

    /// <summary>
    /// Snapshot point for turnover hours series.
    /// </summary>
    /// <param name="timestamp">Timestamp for the data point.</param>
    /// <param name="turnoverHoursValue">Turnover hours at the timestamp.</param>
    public class TurnoverByHourPoint(DateTime timestamp, double turnoverHoursValue)
    {
        /// <summary>
        /// The timestamp for the point.
        /// </summary>
        public DateTime Timestamp { get; set; } = timestamp;
        /// <summary>
        /// Turnover hours value at this point in time.
        /// </summary>
        public double TurnoverHoursValue { get; set; } = turnoverHoursValue;
    }

    /// <summary>
    /// Snapshot point for Inventory to Sales Analysis series.
    /// </summary>
    /// <param name="timestamp">Timestamp for the data point.</param>
    /// <param name="inventoryValue">Inventory value at the timestamp.</param>
    /// <param name="salesAmount">Sales amount at the timestamp.</param>
    /// <param name="inventorySalesRatio">Inventory to sales ratio at the timestamp.</param>
    public class InventoryToSalesAnalysisPoint(DateTime timestamp, double inventoryValue, double salesAmount, double inventorySalesRatio)
    {
        /// <summary>
        /// The timestamp for the point.
        /// </summary>
        public DateTime Timestamp { get; set; } = timestamp;

        /// <summary>
        /// The inventory value at this point in time.
        /// </summary>
        public double InventoryOverTimeValue { get; set; } = inventoryValue;

        /// <summary>
        /// Sales amount at this point in time.
        /// </summary>
        public double SalesAmount { get; set; } = salesAmount;

        /// <summary>
        /// Inventory to sales ratio at this point in time.
        /// </summary>
        public double InventorySalesRatio { get; set; } = inventorySalesRatio;
    }

    /// <summary>
    /// Represents an inventory movement contribution.
    /// </summary>
    /// <param name="movementType">Type of movement (Increase, Decrease, Total).</param>
    /// <param name="movementValue">Magnitude of the movement. Decrease can be negative.</param>
    /// <param name="isSummary">True if the entry is a summary row (e.g., Total).</param>
    public class InventoryMovement(string movementType, double movementValue, bool isSummary)
    {
        /// <summary>
        /// Type of movement (Increase, Decrease, Total).
        /// </summary>
        public string MovementType { get; set; } = movementType;

        /// <summary>
        /// Movement magnitude. Decrease can be negative.
        /// </summary>
        public double MovementValue { get; set; } = movementValue;

        /// <summary>
        /// Indicates if this is a summary row.
        /// </summary>
        public bool IsSummary { get; set; } = isSummary;
    }

    /// <summary>
    /// Represents a top item in the dashboard.
    /// </summary>
    /// <param name="code">Item code.</param>
    /// <param name="itemName">Item display name.</param>
    /// <param name="value">Total value used for ranking.</param>
    /// <param name="quantity">Total quantity used for ranking.</param>
    public class TopItem(string code, string itemName, int value, int quantity)
    {
        /// <summary>
        /// Item code.
        /// </summary>
        public string Code { get; set; } = code;

        /// <summary>
        /// Item display name.
        /// </summary>
        public string ItemName { get; set; } = itemName;

        /// <summary>
        /// Total value used for ranking.
        /// </summary>
        public int Value { get; set; } = value;

        /// <summary>
        /// Total quantity used for ranking.
        /// </summary>
        public int Quantity { get; set; } = quantity;
    }
}
