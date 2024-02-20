using System.Collections.Generic;
using Elite.LMS.Maui.Data;

namespace Elite.LMS.Maui.ViewModels {
    public class AxisLabelOptionsViewModel : ChartViewModelBase {
        readonly PopulationByCountryData data;

        public IList<QualitativeData> PopulationByCountry => data.SeriesData;

        public AxisLabelOptionsViewModel() {
            data = new PopulationByCountryData();
        }
    }
}
