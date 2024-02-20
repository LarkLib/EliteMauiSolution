using System;
using System.IO;

namespace Elite.LMS.Maui.WmsModules.Scheduler.Data.Reminders {
    public partial class StoragePathProvider : Java.Lang.Object, IStoragePathProvider {
        public static string GetFilePath(string fileName) {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(documentsPath, fileName);
        }

        public string GetPath(string fileName) {
            return GetFilePath(fileName);
        }
    }
}

