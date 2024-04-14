using DevExpress.Maui.Controls;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AndroidX.AutoFill.Inline.V1.InlineSuggestionUi;

namespace Elite.LMS.Maui.Controls
{
    public class MessagePopupView : DXPopup
    {
        public MessagePopupView()
        {
            Content = new StackLayout
            {
                WidthRequest = 200,
                HeightRequest = 200,
                Children = { new Label { Text = "Hello, DXPopup!" } }
            };
            AllowScrim = true;
        }
    }
}
