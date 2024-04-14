using Android.OS;
using CommunityToolkit.Mvvm.Messaging;
using DevExpress.Xpo.Logger;
using Elite.LMS.Maui.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite.LMS.Maui.WmsModules
{
    public class ModelUtils
    {

        public void SendInformationMessage(string message)
        {
            _ = SendPopupMessageAsync(new PagePopupMessage() { Message = message });
        }
        public void SendAlterMessage(string message)
        {
            _ = SendPopupMessageAsync(new PagePopupMessage() { Message = message, PopupMessageType = PopupType.Alert });
        }
        public void SendErrorMessage(string message)
        {
            _ = SendPopupMessageAsync(new PagePopupMessage() { Message = message, PopupMessageType = PopupType.Error });
        }
        public async Task<bool> SendConfirmMessage(string message)
        {
            return await SendPopupMessageAsync(new PagePopupMessage() { Message = message, PopupMessageType = PopupType.Confirm }); ;
        }

        public async Task<bool> SendPopupMessageAsync(PagePopupMessage message)
        {

            var ret = false;
            switch (message.PopupMessageType)
            {
                case PopupType.Information:
                case PopupType.Alert:
                case PopupType.Error:
                    await Shell.Current.CurrentPage.DisplayAlert(message.Title, message.Message, message.Accept);
                    break;
                case PopupType.Confirm:
                    ret = await Shell.Current.CurrentPage.DisplayAlert(message.Title, message.Message, message.Accept, message.Cancel);
                    break;
                default:
                    break;
            }
            return ret;
        }
    }
}
