using System;
using System.Collections.Generic;
using System.Linq;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.WindowsForms;
using System.Drawing;

namespace Diplom
{
    public class ChartOxiPlot : IChart
    {
        private PlotView Plot;
        private LineSeries lineSeries;
        private LineSeries markerSeries;

        public ChartOxiPlot(FormWrapper linkForm, Point locPlot, Point sizePlot, string titleName)
        {
            Plot = new PlotView();
            linkForm.Controls.Add(Plot);
            Plot.Location = new Point(locPlot.X, locPlot.Y);
            Plot.Size = new Size(sizePlot.X, sizePlot.Y);

            Plot.Model = new PlotModel
            {
                PlotType = PlotType.XY,
                Background = OxyColor.FromRgb(255, 255, 255),
                TextColor = OxyColor.FromRgb(0, 0, 0)
            };

            DateTimeAxis xAxis = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "HH:mm",
                Title = "Время",
                MinorIntervalType = DateTimeIntervalType.Days,
                IntervalType = DateTimeIntervalType.Days,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.None,
            };
            LinearAxis yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
            };
            Plot.Model.Axes.Add(xAxis);
            Plot.Model.Axes.Add(yAxis);

            //by default
            lineSeries = new LineSeries
            {
                Title = titleName,
                StrokeThickness = 1,
            };
            Plot.Model.Series.Add(lineSeries);

            markerSeries = new LineSeries
            {
                Title = "",
                StrokeThickness = 1,
                MarkerFill = OxyColors.Red,
                MarkerType = MarkerType.Circle,
                MarkerSize = 5,
                Color = OxyColor.FromRgb(255, 255, 255)
            };
            Plot.Model.Series.Add(markerSeries);
        }

        public void VisualizeData(SortedDictionary<DateTime, double> loadData, bool marker)
        {
            AddPointOnLine(loadData, lineSeries);

            //marker 1 value
            if (marker == true)
                AddPointOnLine(loadData, markerSeries);

            //for update chart
            Plot.InvalidatePlot(true);
        }

        public object AddLine(string titleName)
        {
            LineSeries newLineSeries = new LineSeries()
            {
                StrokeThickness = 1,
                Title = titleName,
            };
            Plot.Model.Series.Add(newLineSeries);

            return newLineSeries;
        }

        public void AddPointOnLine(SortedDictionary<DateTime, double> point, object objLine)
        {
            var line = (LineSeries)objLine;

            if(point.Count != 0 && line != null)
                line.Points.Add(new DataPoint(DateTimeAxis.ToDouble(point.Keys.Last()), point.Values.Last()));
        }

        public void DisposeChart()
        {
            Plot.Dispose();
        }
    }
}
