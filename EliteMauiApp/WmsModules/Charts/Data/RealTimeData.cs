using System;
using System.ComponentModel;
using DevExpress.Maui.Charts;
using DevExpress.Maui.Core;
using Microsoft.Maui.Devices.Sensors;

namespace Elite.LMS.Maui.Data {
    public class RealTimeDataProvider {
        static readonly int MaxDataCount = 300;

        readonly IAccelerometer sensor;
        readonly ChartView chart;
        DateTime lastTime = DateTime.Now;

        public BindingList<DateTimeData> XAxisSeriesData { get; } = new BindingList<DateTimeData>();
        public BindingList<DateTimeData> YAxisSeriesData { get; } = new BindingList<DateTimeData>();
        public BindingList<DateTimeData> ZAxisSeriesData { get; } = new BindingList<DateTimeData>();

        public RealTimeDataProvider(ChartView chart) {
            this.chart = chart;
            sensor = Accelerometer.Default;
            sensor.ReadingChanged += Sensor_ReadingChanged;
        }

        private void Sensor_ReadingChanged(object sender, AccelerometerChangedEventArgs e) {
            if (this.lastTime.AddMilliseconds(100) > DateTime.Now)
                return;
            this.lastTime = DateTime.Now;
            chart.Dispatcher.Dispatch(() => {
                chart.SuspendRender();
                XAxisSeriesData.Add(new DateTimeData(DateTime.Now, e.Reading.Acceleration.X));
                RemoveExcessData(XAxisSeriesData);
                YAxisSeriesData.Add(new DateTimeData(DateTime.Now, e.Reading.Acceleration.Y));
                RemoveExcessData(YAxisSeriesData);
                ZAxisSeriesData.Add(new DateTimeData(DateTime.Now, e.Reading.Acceleration.Z));
                RemoveExcessData(ZAxisSeriesData);
                chart.ResumeRender();
            });
        }

        void RemoveExcessData(BindingList<DateTimeData> data) {
            if (data.Count > MaxDataCount)
                data.RemoveAt(0);
        }

        public void Stop() {
            sensor.Stop();
        }
        public void Start() {
            var sensorSpeed = ON.iOS ? SensorSpeed.Fastest : SensorSpeed.Game;
            sensor.Start(SensorSpeed.Fastest);
        }
    }
}
