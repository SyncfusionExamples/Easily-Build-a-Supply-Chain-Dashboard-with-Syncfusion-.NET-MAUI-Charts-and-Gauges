using System;
using System.Collections.ObjectModel;

namespace SupplyChainDashboardSample.Models
{
    public class InventoryKpi
    {
        public string Title { get; set; } = string.Empty;
        public double Value { get; set; }
        public string SubText { get; set; } = string.Empty;
        public double Min { get; set; } = 0;
        public double Max { get; set; } = 100;
    }

    public class MonthValue
    {
        public string Month { get; set; } = string.Empty;
        public double Value { get; set; }
        public double Secondary { get; set; }
        public double Change { get; set; }
        public double Ratio { get; set; }
        public bool IsSummary { get; set; }
    }

    public class TopItem
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public double Amount { get; set; }
    }

    public static class DemoInventoryData
    {
        private static readonly string[] Months = new []{"Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"};
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

        public static ObservableCollection<MonthValue> BuildTurnoverDays()
        {
            var rand = new Random(42);
            var data = new ObservableCollection<MonthValue>();
            foreach (var m in Months)
            {
                data.Add(new MonthValue{ Month = m, Value = rand.Next(15, 85)});
            }
            return data;
        }

        public static ObservableCollection<MonthValue> BuildMovement()
        {
            return new ObservableCollection<MonthValue>
            {
                new MonthValue{ Month = "Increase", Value = 70 },
                new MonthValue{ Month = "Decrease", Value = -64 },
                new MonthValue{ Month = "Total", Value = 4, IsSummary = true },
            };
        }

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