using Android.App;
using Android.Content;
using Elite.LMS.Maui.WmsModules.Models;
using Microsoft.Maui.Controls;

[BroadcastReceiver(Enabled = true, Exported = true)]
[IntentFilter(["android.intent.action.SCANRESULT"])]
public class BarcodeBroadcastReceiver : BroadcastReceiver
{
    public override void OnReceive(Context context, Intent intent)
    {
        string value = intent.GetStringExtra("value");
        //string length = intent.GetStringExtra("length");
        Page p = Shell.Current.CurrentPage;

        if (p is IBarcodeReceiver)
        {
            (p as IBarcodeReceiver).OnBarcodeReceive(value);
        }
    }
}
