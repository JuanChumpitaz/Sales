
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
        private ObservableCollection<TBM_PRODU_JCH> tbm_Produ_Jch;
        public ObservableCollection<TBM_PRODU_JCH> Tbm_Produ_Jch
        {
            get { return this.tbm_Produ_Jch; }
            set { this.SetValue(ref this.tbm_Produ_Jch, value); }
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
            this.Tbm_Produ_Jch = new ObservableCollection<TBM_PRODU_JCH>(list);
        }
    }
}
