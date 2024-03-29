﻿using System.Collections.Generic;
using Elite.LMS.Maui.Models;
using Elite.LMS.Maui.Views;

namespace Elite.LMS.Maui.Data {
    public class ChartsData : IWmsData {
        readonly List<WmsItem> wmsItems;

        public ChartsData() {
            this.wmsItems = new List<WmsItem>() {
                new WmsItem() {
                    Title = "Bar",
                    ControlsPageTitle = "Bar Charts",
                    Description="Bar charts allow you to illustrate numerical values and proportions between them. Our Chat View control supports stacked, full-stacked, side-by-side, and rotated bar chart types, as well as their combinations.",
                    Module = typeof(BarCharts),
                    Icon = "barcharts",
                },
                new WmsItem() {
                    Title = "Line",
                    ControlsPageTitle = "Line Charts",
                    Description="A line chart connects points in a Cartesian coordinate system to display data as lines. Our control supports line, curved line, step line, and scatter chart types.",
                    Module = typeof(LineCharts),
                    Icon = "linecharts"
                },
                new WmsItem() {
                    Title = "Area",
                    ControlsPageTitle = "Area Charts",
                    Description="You can display data as filled areas on a chart. As in the line charts, the range, stacked, full-stacked, and step chart types are supported.",
                    Module = typeof(AreaCharts),
                    Icon = "areacharts"
                },
                new WmsItem() {
                    Title = "Pie & Donut",
                    ControlsPageTitle = "Pie & Donut Charts",
                    Description="The pie and donut charts illustrate the numerical proportion of data values.",
                    Module = typeof(PieCharts),
                    Icon = "piedonutcharts",
                    ShowItemUnderline = false
                },
                new WmsItem() {
                    Title = "Point & Bubble",
                    ControlsPageTitle = "Point & Bubble Charts",
                    Description="The Chart View control can display data as points in a Cartesian coordinate system. While the point chart displays data on a two-dimensional coordinate system, the bubble chart displays three-dimensional data. The size of a bubble depends on the third coordinate.",
                    Module = typeof(PointsCharts),
                    Icon = "pointbublecharts"
                },
                new WmsItem() {
                    Title = "Financial",
                    ControlsPageTitle = "Financial Chart",
                    Description="Our Chart View control can display Open-High-Low-Close stock price statistics as Japanese Candlesticks as well as multiple financial indicators.",
                    Module = typeof(FinancialChart),
                    Icon = "financialcharts"
                }
            };
        }
        public List<WmsItem> WmsItems => this.wmsItems;
        public string Title => TitleData.ChartsDataTitle;
    }
}
