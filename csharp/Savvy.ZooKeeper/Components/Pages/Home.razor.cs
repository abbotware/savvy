namespace Savvy.ZooKeeper.Components.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Savvy.ZooKeeper.Models;
    using Syncfusion.Blazor.Charts;

    public partial class Home
    {
        public EmptyPointMode Mode = EmptyPointMode.Gap;

        public class PieData
        {
            public PieData(AnimalStatus status, double value)
            {
                XValue = status;
                YValue = value;

                Fill = status switch
                {
                    AnimalStatus.Healthy => "#008000",
                    AnimalStatus.Injured => "#FFA500",
                    AnimalStatus.Sick => "#FFFF00",
                    AnimalStatus.Decseased => "#000000",
                    _ => "#FF0000",
                };
            }

            public AnimalStatus XValue { get; set; }
            public double YValue { get; set; }
            public string Fill { get; set; }
        }
        public List<PieData> PieChartPoints { get; set; } = new();

        protected override void OnInitialized()
        {
            var status = Database.Animals
            .Include(x => x.CurrentState)
            .ToList()
            .GroupBy(x => x.CurrentStatus);

            PieChartPoints = status.Select(g => new PieData(g.Key,g.Count())).ToList();
        }

        private void OnLabel(AccumulationTextRenderEventArgs args)
        {
            args.Text = args.Point.X + " " + args.Point.Y;
        }
    }
}