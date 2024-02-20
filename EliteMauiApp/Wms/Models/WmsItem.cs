using System;
using System.Collections.Generic;
using Elite.LMS.Maui.Data;

namespace Elite.LMS.Maui.Models {
    public class WmsItem {
        string pageTitle;
        string icon;
        string controlsPageTitle;
        Type module;
        List<WmsItem> wmsItems;

        public string Icon {
            get => this.icon; 
            set => this.icon = value;
        }
        public bool IsHeader { get; set; }
        public string Title { get; set; }
        public string PageTitle {
            get => this.pageTitle ?? ControlsPageTitle;
            set => this.pageTitle = value;
        }
        public string ControlsPageTitle {
            get => this.controlsPageTitle ?? Title;
            set => this.controlsPageTitle = value;
        }
        public string Description { get; set; }

        public Type Module {
            get => this.module;
            set {
                this.module = value;
                if (value != null && value.GetInterface("IWmsData") != null) {
                    this.wmsItems = ((IWmsData)Activator.CreateInstance(value)).WmsItems;
                }
            }
        }
        public List<WmsItem> WmsItems => this.wmsItems;

        public bool ShowItemUnderline { get; set; } = true;
        public WmsItemStatus WmsItemStatus { get; set; } = WmsItemStatus.None;

        public bool ShowBadge => WmsItemStatus != WmsItemStatus.None;
        public string BadgeText => WmsItemStatus switch {
            WmsItemStatus.Updated => "UPD",
            WmsItemStatus.New => "NEW",
            WmsItemStatus.None => string.Empty,
            _ => throw new ArgumentException($"Unknown {nameof(WmsItemStatus)}."),
        };

        public string BadgeIcon {
            get {
                return WmsItemStatus switch {
                    WmsItemStatus.Updated => "badgeupd",
                    WmsItemStatus.New => "badgenew",
                    _ => null,
                };
            }
        }
    }
}
