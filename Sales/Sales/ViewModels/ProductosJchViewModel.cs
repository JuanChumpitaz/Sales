
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Sales.Common.Models;
using Sales.Helpers;
using Sales.Services;
using Xamarin.Forms;

namespace Sales.ViewModels
{
    public class ProductosJchViewModel : BaseViewModel
    {
        #region Attributes
        private ApiService apiService;

        private bool isRefreshing;

        #endregion

        #region Properties
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
        #endregion
        #region Constructor

        public ProductosJchViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.LoadProductosJch();
        }

        #endregion

        #region Singleton

        private static ProductosJchViewModel instance; // SERVIRA PARA GUARDAR TODO LO QUE TENGA MI MODELO HASTA ANTES DE HACER UN INSERT POR EJEMPLO

        public static ProductosJchViewModel GetInstance()// ME DEVUELVE UNA INSTANCIA DE ProductosJchViewModel
        {
            if (instance==null)
            {
                return new ProductosJchViewModel();
            }
            return instance;
        }

        #endregion

        #region Methods
        private async void LoadProductosJch()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductosJchController"].ToString();

            var response = await this.apiService.GetList<TBM_PRODU_JCH>(url, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var list = (List<TBM_PRODU_JCH>)response.Result;
            this.ProductosJch = new ObservableCollection<TBM_PRODU_JCH>(list);
            this.IsRefreshing = false;
        }
        #endregion
        #region Commands

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadProductosJch);
            }
        } 
        #endregion
    }
}
