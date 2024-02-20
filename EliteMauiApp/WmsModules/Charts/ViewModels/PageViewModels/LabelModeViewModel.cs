using System;
using System.Collections.Generic;
using Elite.LMS.Maui.ViewModels;

namespace Elite.LMS.Maui.Charts.ViewModels {
    public class LabelModeViewModel : ChartsPageViewModelBase {
        static readonly List<ChartItemInfoContainerBase> content = new List<ChartItemInfoContainerBase>() {
            new AxisLabelOptionsItemInfoContainer(
                viewModel: new AxisLabelOptionsViewModel(),
                type: AxisLabelOptionsType.RotatedAndStaggered
            ),
            new AxisLabelOptionsItemInfoContainer(
                viewModel: new CryptocurrencyPortfolioViewModel(),
                type: AxisLabelOptionsType.CryptocurrencyPortfolio
            )
        };

        public override List<ChartItemInfoContainerBase> Content => content;
    }

    public class AxisLabelOptionsItemInfoContainer : ChartItemInfoContainerBase {
        public AxisLabelOptionsItemInfoContainer(AxisLabelOptionsType type, ChartViewModelBase viewModel) {
            LabelModeType = type;
            ChartModel = viewModel;
        }
        public AxisLabelOptionsType LabelModeType { get; }
        public string LabelModelTypeImage {
            get {
                switch (LabelModeType) {
                    case AxisLabelOptionsType.RotatedAndStaggered:
                        return GetThemedImageName("wmschartsrotatedlabels");
                    case AxisLabelOptionsType.CryptocurrencyPortfolio:
                        return GetThemedImageName("wmschartscryptocurrencyportfolio");
                    default:
                        throw new ArgumentException("The selector cannot handle the passed AxisLabelOptionsType value.");
                }
            }
        }
    }
}
