﻿using System.Collections.Generic;
using Elite.LMS.Maui.Models;
using Elite.LMS.Maui.Views;

namespace Elite.LMS.Maui.Data {
    public class ChartsDataAdvanced : IWmsData {
        readonly List<WmsItem> wmsItems;

        public ChartsDataAdvanced() {
            this.wmsItems = new List<WmsItem>() {
                new WmsItem() {
                    Title = "Axis Label Options",
                    Description = "Our Chart View control can display axis labels at a specific rotation angle and in staggered order.",
                    Module = typeof(AxisLabelOptions),
                    Icon = "labelmode"
                },
                new WmsItem() {
                    Title = "Series Template",
                    Description = "A template can be used to generate multiple series from a single data source.",
                    Module = typeof(SeriesTemplate),
                    Icon = "seriestemplateadapter"
                },
                new WmsItem() {
                    Title = "Custom Appearance",
                    Description = "Shows how to customize the appearance of series and other elements of the chart.",
                    Module = typeof(Colorizer),
                    Icon = "chartscustomappearance"
                },
                new WmsItem() {
                    Title = "Spectrum Analysis",
                    Description = "Shows multiple charts that allow real-time frequency spectrum analysis.",
                    Module = typeof(SpectrumAnalyzer),
                    Icon = "spectrumanalysis",
                },
                new WmsItem() {
                    Title = "Logarithmic Scale",
                    Description = "Wmsnstrates a chart axis that uses the logarithmic scale to display numeric values.",
                    Module = typeof(LogarithmicScale),
                    Icon = "logarithmicscale",
                },
                new WmsItem() {
                    Title = "Oscillator",
                    Description="This chart displays a wave generated by an electronic oscillator. The data source contains series points for the wave at a specific moment in time. When the data source changes, the chart updates the series.",
                    Module = typeof(Oscillator),
                    Icon = "oscillator",
                },
                new WmsItem() {
                    Title = "Real-Time Data Updates",
                    Description="This module wmsnstrates an auto-refreshed line chart bound to a frequently updated dataset.",
                    Module = typeof(RealTimeData),
                    Icon = "realtimedatacharts"
                },
                new WmsItem() {
                    Title = "Selection",
                    Description="This module contains pie and line charts. The pie chart shows sales by region, while the line chart displays sales by year. You can select a sector on the pie chart to see details on the line chart.",
                    Module = typeof(Selection),
                    Icon = "selection"
                },
                new WmsItem() {
                    Title = "Multiple Axes",
                    Description="You can display multiple axes within a single chart.",
                    Module = typeof(MultipleAxes),
                    Icon = "multipleaxes"
                },
                new WmsItem() {
                    Title = "Large Dataset",
                    ControlsPageTitle = "Blazing Fast Native Rendering",
                    Description="This chart is bound to a data source that contains more than 100K, 250K, or 500K records. See how fast the chart renders data.",
                    Module = typeof(LargeDataset),
                    Icon = "largedataset"
                }
            };
        }
        public List<WmsItem> WmsItems => this.wmsItems;
        public string Title => TitleData.ChartsDataAdvancedTitle;
    }
}
