using System;
using System.Collections.Generic;
using System.Linq;
using Elite.LMS.Maui.Models;
using Elite.LMS.Maui.Views;

namespace Elite.LMS.Maui.Data {
    public class SchedulerData : IWmsData {
        public static WmsItem GetItem(Type module) {
            IEnumerable<WmsItem> items = wmsItems.Where((d) => d.Module == module);
            return items.Any() ? items.Last() : null;
        }

        static readonly List<WmsItem> wmsItems;

        static SchedulerData() {
            wmsItems = new List<WmsItem>() {
                new WmsItem() {
                    Title = "Calendar",
                    Description = "A calendar that allows a user to select a date. The calendar can highlight holidays, observances, and any specific days in the year.",
                    Module = typeof(CalendarView),
                    Icon = "calendar"
                },
                new WmsItem() {
                    Title = "Day View",
                    Description="This scheduler view displays appointments for one or more days.",
                    Module = typeof(DayViewWms),
                    Icon = "dayview"
                },
                 new WmsItem() {
                    Title = "Work Week View",
                    Description="This scheduler view displays appointments for weekdays only.",
                    Module = typeof(ReceptionDesk),
                    Icon = "workweekview"
                 },
                new WmsItem() {
                    Title = "Week View",
                    Description="This view displays appointments for the entire week.",
                    Module = typeof(WeekViewWms),
                    Icon = "weekview"
                },
                new WmsItem() {
                    Title = "Month View",
                    Description="An overview of appointments for a month.",
                    Module = typeof(MonthViewWms),
                    Icon = "monthview"
                },
                
                
                
                
                
                
            };
        }
        public List<WmsItem> WmsItems => wmsItems;
        public string Title => TitleData.SchedulerDataTitle;
    }
}
