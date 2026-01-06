namespace SupplyChainDashboardSample
{
    public class KpiSnapshot
    {
        public DateTime Timestamp { get; set; }
        public double InventoryValue { get; set; }
        public string InventoryChange { get; set; } = string.Empty;
        public double StockAvailableValue { get; set; }
        public string StockAvailableChange { get; set; } = string.Empty;
        public double TurnoverRatioValue { get; set; }
        public double InventoryToSalesRatioValue { get; set; }
        public double AvgInventoryDaysOfSupplyValue { get; set; }
        public double InventoryOverTimeValue { get; set; }
        public double InventoryValueOverTimeChange { get; set; }
        public double TurnoverHoursValue { get; set; }
        public double SalesAmount { get; set; }
        public double InventorySalesRatio { get; set; }
    }

    public class InventoryMovement
    {
        public string MovementType { get; set; } = string.Empty;
        public double MovementValue { get; set; }
        public bool IsSummary { get; set; }
    }

    public class TopItem
    {
        public string Code { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
        public int Value { get; set; }
        public int Quantity { get; set; }
    }
}
