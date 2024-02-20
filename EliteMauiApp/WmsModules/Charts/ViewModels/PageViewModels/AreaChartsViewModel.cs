using System;
using System.Collections.Generic;
using Elite.LMS.Maui.ViewModels;

namespace Elite.LMS.Maui.Charts.ViewModels {
    public class AreaChartsViewModel : ChartsPageViewModelBase {
        static readonly List<ChartItemInfoContainerBase> content = new List<ChartItemInfoContainerBase>() {
            new AreaChartItemInfoContainer(
                viewModel: new RangeAreaChartViewModel(),
                type: AreaType.Range
            ),
            new AreaChartItemInfoContainer(
                viewModel: new AreaChartViewModel(),
                type: AreaType.Simple
            ),
            new AreaChartItemInfoContainer(
                viewModel: new StackedAreaChartViewModel(),
                type: AreaType.Stacked
            ),
            new AreaChartItemInfoContainer(
                viewModel: new FullStackedAreaChartViewModel(),
                type: AreaType.FullStacked
            ),
            new AreaChartItemInfoContainer(
                viewModel: new StepAreaChartViewModel(),
                type: AreaType.Step
            )
        };
        public override List<ChartItemInfoContainerBase> Content => content;
    }

    public class AreaChartItemInfoContainer : ChartItemInfoContainerBase {
        public AreaChartItemInfoContainer(AreaType type, ChartViewModelBase viewModel) {
            this.AreaType = type;
            this.ChartModel = viewModel;
        }
        public AreaType AreaType { get; set; }
        public string AreaTypeImage {
            get {
                switch (AreaType) {
                    case AreaType.Simple:
                        return GetThemedImageName("wmschartsarea");
                    case AreaType.Range:
                        return GetThemedImageName("wmschartsrangearea");
                    case AreaType.Stacked:
                        return GetThemedImageName("wmschartsstackedarea");
                    case AreaType.FullStacked:
                        return GetThemedImageName("wmschartsfullstackedarea");
                    case AreaType.Step:
                        return GetThemedImageName("wmschartssteparea");
                    default:
                        throw new ArgumentException("The selector cannot handle the passed AreaType value.");
                }
            }
        }
    }
}
