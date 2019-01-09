
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Sales.Common.Models;
using Sales.Services;
using Xamarin.Forms;

namespace Sales.ViewModels
{
    public class ProductosJchViewModel : BaseViewModel
    {
        private ApiService apiService;
        private ObservableCollection<TBM_PRODU_JCH> productosJch;
        public ObservableCollection<TBM_PRODU_JCH> ProductosJch
        {
            get { return this.productosJch; }
            set { this.SetValue(ref this.productosJch, value); }
        }

        public ProductosJchViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProductosJch();
        }

        private async void LoadProductosJch()
        {
            var response = await this.apiService.GetList<TBM_PRODU_JCH>("http://apimyperjuan.azurewebsites.net", "/api","/ProductosJch");
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error",response.Message,"Accept");
                return;
            }

            var list = (List<TBM_PRODU_JCH>)response.Result;
            this.ProductosJch = new ObservableCollection<TBM_PRODU_JCH>(list);
        }
    }
}
