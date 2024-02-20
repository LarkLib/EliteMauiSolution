using System;
using System.Collections.Generic;
using Elite.LMS.Maui.ViewModels;

namespace Elite.LMS.Maui.Charts.ViewModels {
    public class ColorizersViewModel : ChartsPageViewModelBase {
        static readonly List<ChartItemInfoContainerBase> content = new List<ChartItemInfoContainerBase>() {
            new ColorizerItemInfoContainer(
                viewModel: new AreaGradientFillEffectViewModel(),
                type: CustomAppearanceType.AreaGradientFillEffect
            ),
            new ColorizerItemInfoContainer(
                viewModel: new BubbleColorizerViewModel(),
                type: CustomAppearanceType.Bubble
            ),
            new ColorizerItemInfoContainer(
                viewModel: new BarColorizerViewModel(),
                type: CustomAppearanceType.Bar
            ),
            new ColorizerItemInfoContainer(
                viewModel: new GradientSegmentColorizerViewModel(),
                type: CustomAppearanceType.GradientSegmentColorizer
            ),
            new ColorizerItemInfoContainer(
                viewModel: new OperationSurfaceTemperatureViewModel(),
                type: CustomAppearanceType.OperationSurfaceTemperature
            )
        };

        public override List<ChartItemInfoContainerBase> Content => content;
    }

    public class ColorizerItemInfoContainer : ChartItemInfoContainerBase {
        public ColorizerItemInfoContainer(CustomAppearanceType type, ChartViewModelBase viewModel) {
            CustomAppearanceModuleType = type;
            ChartModel = viewModel;
        }
        public CustomAppearanceType CustomAppearanceModuleType { get; set; }

        public string CustomAppearanceTypeImage {
            get {
                switch (CustomAppearanceModuleType) {
                    case CustomAppearanceType.AreaGradientFillEffect:
                        return GetThemedImageName("wmschartsareagradientfill");
                    case CustomAppearanceType.Bubble:
                        return GetThemedImageName("wmschartscolorizerbubble");
                    case CustomAppearanceType.Bar:
                        return GetThemedImageName("wmschartscolorizerbar");
                    case CustomAppearanceType.GradientSegmentColorizer:
                        return GetThemedImageName("wmschartslightspector");
                    case CustomAppearanceType.OperationSurfaceTemperature:
                        return GetThemedImageName("wmschartssurfacetemperature");
                    default:
                        throw new ArgumentException("The selector cannot handle the passed CustomAppearanceType value.");
                }
            }
        }
    }
}
