using System.Collections.Generic;
using Elite.LMS.Maui.Models;

namespace Elite.LMS.Maui.Data
{
    public interface IWmsData
    {
        List<WmsItem> WmsItems { get; }
        string Title { get; }
    }
}
