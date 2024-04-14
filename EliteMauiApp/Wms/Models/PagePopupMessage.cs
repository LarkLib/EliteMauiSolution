using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite.LMS.Maui.Models
{
    public class PagePopupMessage //: AsyncRequestMessage<bool>
    {
        private string title;
        public string Message { get; set; }
        public string Title
        {
            get
            {
                if (string.IsNullOrEmpty(title))
                {
                    title = PopupMessageType switch
                    {
                        PopupType.Information => "提示信息",
                        PopupType.Alert => "警告信息",
                        PopupType.Confirm => "确认信息",
                        PopupType.Error => "错误信息",
                        _ => "提示信息"
                    };
                }
                return title;
            }
            set { title = value; }
        }
        public string Accept { get; set; } = "确定";
        public string Cancel { get; set; } = "取消";
        public PopupType PopupMessageType { get; set; } = PopupType.Information;
    }

    public enum PopupType
    {
        Information,
        Alert,
        Confirm,
        Error
    }
}
