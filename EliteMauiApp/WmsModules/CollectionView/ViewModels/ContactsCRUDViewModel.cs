﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Elite.LMS.Maui.WmsModules.CollectionView.Data;
using Microsoft.Maui.Controls;

namespace Elite.LMS.Maui.ViewModels {
    public class ContactsCRUDViewModel : NotificationObject {
        bool isRefreshing;
        ObservableCollection<Contact> contacts;

        public ICommand RefreshDataCommand { get; }

        public ObservableCollection<Contact> Contacts {
            get => this.contacts;
            set => SetProperty(ref this.contacts, value);
        }
        public bool IsRefreshing {
            get => this.isRefreshing;
            set => SetProperty(ref this.isRefreshing, value);
        }

        public ContactsCRUDViewModel() {
            LoadData();
            RefreshDataCommand = new Command(LoadData);
        }

        async void LoadData() {
            IsRefreshing = true;
            IEnumerable<Contact> retrievedItems = await Task.Run(() => DBContactService.Instance.GetItems());
            Contacts = new ObservableCollection<Contact>(retrievedItems);
            IsRefreshing = false;
        }
    }
}