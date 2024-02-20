using System;
using Microsoft.Maui.ApplicationModel;

namespace Elite.LMS.Maui.Services {
    public class MauiUriOpener : IOpenUriService {
        public void Open(string uri) {
            Launcher.OpenAsync(new Uri(uri));
        }
    }
}
