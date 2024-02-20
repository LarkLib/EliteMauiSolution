using System;
using System.Collections.Generic;
using System.Linq;
using Elite.LMS.Maui.Models;
using Elite.LMS.Maui.Views;

namespace Elite.LMS.Maui.Data {
    public class OfficeFileAPIData : IWmsData {
        public static WmsItem GetItem(Type module) {
            IEnumerable<WmsItem> items = wmsItems.Where((d) => d.Module == module);
            return items.Any() ? items.Last() : null;
        }

        static readonly List<WmsItem> wmsItems;

        static OfficeFileAPIData() {
            wmsItems = new List<WmsItem>() {
                new WmsItem() {
                    Title = "Pdf Viewer",
                    Description="The DevExpress Pdf Viewer allow you to view and search through PDF Document",
                    Module = typeof(PdfViewerPage),
                    Icon = "pdfviewer",
                    WmsItemStatus = WmsItemStatus.New
                },
                new WmsItem() {
                    Title = "Sign PDF Files",
                    Description = "Sign a PDF File with a certificate and add a hand-drawn signature.",
                    Module = typeof(SignatureWms),
                    Icon = "editing",
                },
                new WmsItem() {
                    Title = "Fill Out PDF Forms",
                    Description = "Use the DataForm Control to fill out form fields in a PDF document.",
                    Module = typeof(FillPDFMainPage),
                    Icon = "editors",
                },
                new WmsItem() {
                    Title = "Convert Files",
                    Description = "Convert DOC/DOCX, XLS/XLSX, and HTML documents to PDF, HTML, or DOCX.",
                    Module = typeof(ConverterWms),
                    Icon = "pulltorefresh",
                }
            };
        }
        public List<WmsItem> WmsItems => wmsItems;
        public string Title => TitleData.OfficeFileAPIDataTitle;
    }
}
