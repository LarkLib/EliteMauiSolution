using Android.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite.LMS.Maui.WmsModules.Models
{
    public interface IBarcodeReceiver
    {
#if ANDROID
        public void OnBarcodeReceive(string barcode);
#endif
    }
}
