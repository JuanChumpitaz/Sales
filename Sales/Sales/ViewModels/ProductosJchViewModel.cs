﻿
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Sales.Common.Models;
using Sales.Services;
using Xamarin.Forms;

namespace Sales.ViewModels
{
    public class ProductosJchViewModel : BaseViewModel
    {
        private ApiService apiService;

        private bool isRefreshing;
        

        private ObservableCollection<TBM_PRODU_JCH> productosJch;
        public ObservableCollection<TBM_PRODU_JCH> ProductosJch
        {
            get { return this.productosJch; }
            set { this.SetValue(ref this.productosJch, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public ProductosJchViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProductosJch();
        }

        private async void LoadProductosJch()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Accept");
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();

            var response = await this.apiService.GetList<TBM_PRODU_JCH>(url, "/api","/ProductosJch");
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error",response.Message,"Accept");
                return;
            }

            var list = (List<TBM_PRODU_JCH>)response.Result;
            this.ProductosJch = new ObservableCollection<TBM_PRODU_JCH>(list);
            this.IsRefreshing = false;
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadProductosJch);
            }
        }
    }
}
