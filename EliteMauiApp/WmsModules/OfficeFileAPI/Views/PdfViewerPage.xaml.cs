using DevExpress.Maui.Pdf;

namespace Elite.LMS.Maui.Views;

public partial class PdfViewerPage : Wms.WmsPage {
    public PdfViewerPage() {
        InitializeComponent();
    }

    void OnPageTap(object sender, PageTapEventArgs e) {
        pdfViewer.ShowToolbar = !pdfViewer.ShowToolbar;
        Microsoft.Maui.Controls.Shell.SetNavBarIsVisible(this, pdfViewer.ShowToolbar);
    }
}
