﻿

namespace Sales.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Views;
    using Xamarin.Forms;

    public class MainViewModel
    {
        public ProductosJchViewModel ProductosJch { get; set; }

        public MainViewModel()
        {
            this.ProductosJch = new ProductosJchViewModel();
        }
        public ICommand  AddProductCommand
        {
            get
            {
                return new RelayCommand(GotoAddProduct); 
            }
        }

        private void GotoAddProduct()
        {
            Application.Current.MainPage.Navigation.PushAsync(new AddProductPage());
        }
    }
}
