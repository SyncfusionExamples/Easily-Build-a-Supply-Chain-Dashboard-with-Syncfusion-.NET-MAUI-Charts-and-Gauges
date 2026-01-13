using System.ComponentModel;

namespace SupplyChainDashboardSample
{
    public partial class InventoryDashboardViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Timer callback that advances the time window and appends new random demo points to all charts.
        /// Also refreshes InventoryMovement and KPI cards.
        /// </summary>
        /// <returns>False to stop the timer; true to continue.</returns>
        private bool UpdateVerticalData()
        {
            if (canStopTimer) return false;

            Date = Date.Add(TimeSpan.FromHours(1));
            InventoryValueOverTime.Add(new InventoryValueOverTimePoint( Date, random.Next(80, 120), random.Next(2, 26) ));
            TurnoverByHour.Add(new TurnoverByHourPoint( Date, random.Next(30, 55) ));
            InventoryToSalesAnalysis.Add(new InventoryToSalesAnalysisPoint( Date, random.Next(80, 120), random.Next(80, 90), Math.Round(random.NextDouble() * (5.5 - 4.5) + 4.5, 2) ));
            SeedInventoryMovement();
            KPICards();
            count++;
            return true;
        }

        /// <summary>
        /// Seeds InventoryMovement with randomized Increase, Decrease, and Total values.
        /// </summary>
        private void SeedInventoryMovement()
        {
            InventoryMovement.Clear();
            int increase = random.Next(55, 60);
            int decrease = -random.Next(45, 50);
            int total = increase + decrease;
            InventoryMovement.Add(new InventoryMovement( "Increase", increase, false ));
            InventoryMovement.Add(new InventoryMovement( "Decrease", decrease, false ));
            InventoryMovement.Add(new InventoryMovement( "Total", total, true ));
        }

        /// <summary>
        /// Generates and assigns new randomized KPI values for the KPI cards.
        /// </summary>
        private void KPICards()
        {
            Kpis = new KpiCards
            {
                InventoryValue = Math.Round(random.NextDouble() * (21000000 - 20000000) + 20000000),
                InventoryChange = $"Change: {Math.Round(random.NextDouble() * (2500000 - 1000000) + 1000000):N0}",
                StockAvailableValue = Math.Round(random.NextDouble() * (3900000 - 3700000) + 3700000),
                StockAvailableChange = $"Change: {Math.Round(random.NextDouble() * (60000 - 40000) + 40000):N0}",
                TurnoverRatio = Math.Round(random.NextDouble() * (9.5 - 8.5) + 8.5, 2),
                InventoryToSalesRatio = Math.Round(random.NextDouble() * (0.5 - 0.4) + 0.4, 2),
                AvgInventoryDaysOfSupply = random.Next(35, 43),
            };
        }

        /// <summary>
        /// Clears existing series data, seeds initial points, and starts the UI timer to continuously add data.
        /// </summary>
        public void StartTimer()
        {
            InventoryValueOverTime.Clear();
            TurnoverByHour.Clear();
            InventoryToSalesAnalysis.Clear();
            Date = new DateTime(2026, 1, 1, 01, 00, 00);

            for (int i = 0; i < 3; i++)
            {
                var currentDate = Date.Add(TimeSpan.FromHours(i));
                InventoryValueOverTime.Add(new InventoryValueOverTimePoint( currentDate, random.Next(80, 120), random.Next(2, 26) ));
                TurnoverByHour.Add(new TurnoverByHourPoint( currentDate, random.Next(30, 55) ));
                InventoryToSalesAnalysis.Add(new InventoryToSalesAnalysisPoint( currentDate, random.Next(80, 120), random.Next(80, 90), Math.Round(random.NextDouble() * (5.5 - 4.5) + 4.5, 2) ));
            }

            SeedInventoryMovement();
            KPICards();
            Date = Date.Add(TimeSpan.FromHours(2));
            canStopTimer = false;
            count = 1;

            if (Application.Current != null)
            {
                Application.Current.Dispatcher.StartTimer(
                    new TimeSpan(0, 0, 0, 2, 0),
                    UpdateVerticalData
                );
            }
        }

        /// <summary>
        /// Requests the timer to stop adding new data.
        /// </summary>
        public void StopTimer()
        {
            canStopTimer = true;
            count = 1;
        }
    }
}
