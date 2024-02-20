using System;
using System.Collections.Generic;
using Elite.LMS.Maui.ViewModels;

namespace Elite.LMS.Maui.Charts.ViewModels {
    public class BarChartsViewModel : ChartsPageViewModelBase {
        static readonly List<ChartItemInfoContainerBase> content = new List<ChartItemInfoContainerBase>() {
            new BarChartItemInfoContainer(
                viewModel: new SideBySideRangeBarChartViewModel(),
                type: BarType.SideBySideRange
            ),
            new BarChartItemInfoContainer(
                viewModel: new RangeBarChartViewModel(),
                type: BarType.Range
            ),
            new BarChartItemInfoContainer(
                viewModel: new PopulationPyramidViewModel(),
                type: BarType.PopulationPyramid
            ),
            new BarChartItemInfoContainer(
                viewModel: new BarChartViewModel(),
                type: BarType.Simple
            ),
            new BarChartItemInfoContainer(
                viewModel: new StackedBarChartViewModel(),
                type: BarType.Stacked
            ),
            new BarChartItemInfoContainer(
                viewModel: new SideBySideStackedBarChartViewModel(),
                type: BarType.SideBySideStacked
            ),
            new BarChartItemInfoContainer(
                viewModel: new FullStackedBarChartViewModel(),
                type: BarType.FullStacked
            ),
            new BarChartItemInfoContainer(
                viewModel: new SideBySideStackedBarChartViewModel(),
                type: BarType.SideBySideFullStacked
            ),
            new BarChartItemInfoContainer(
                viewModel: new StackedBarChartViewModel(),
                type: BarType.RotatedStacked
            ),
            new BarChartItemInfoContainer(
                viewModel: new SideBySideStackedBarChartViewModel(),
                type: BarType.RotatedSideBySide
            )
        };

        public override List<ChartItemInfoContainerBase> Content => content;
    }

    public class BarChartItemInfoContainer : ChartItemInfoContainerBase {
        public BarType BarType { get; }

        public BarChartItemInfoContainer(BarType type, ChartViewModelBase viewModel) {
            BarType = type;
            ChartModel = viewModel;
        }

        public string BarTypeImage {
            get {
                switch (BarType) {
                    case BarType.Simple:
                        return GetThemedImageName("wmschartsbar");
                    case BarType.Range:
                        return GetThemedImageName("wmschartsrangebar");
                    case BarType.SideBySideRange:
                        return GetThemedImageName("wmschartssidebysiderangebar");
                    case BarType.Stacked:
                        return GetThemedImageName("wmschartsstackedbar");
                    case BarType.SideBySideStacked:
                        return GetThemedImageName("wmschartssidebysidestackedbar");
                    case BarType.FullStacked:
                        return GetThemedImageName("wmschartsfullstackedbar");
                    case BarType.SideBySideFullStacked:
                        return GetThemedImageName("wmschartssidebysidefullstackedbar");
                    case BarType.RotatedStacked:
                        return GetThemedImageName("wmschartsrotatedstackedbar");
                    case BarType.RotatedSideBySide:
                        return GetThemedImageName("wmschartsrotatedsidebysidestackedbar");
                    case BarType.PopulationPyramid:
                        return GetThemedImageName("wmschartspopulationpyramid");
                    default:
                        throw new ArgumentException("The selector cannot handle the passed BarType value.");
                }
            }
        }
    }
}
