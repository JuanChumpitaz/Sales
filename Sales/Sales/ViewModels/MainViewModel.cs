using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.ViewModels
{
    public class MainViewModel
    {
        public ProductosJchViewModel ProductosJch { get; set; }

        public MainViewModel()
        {
            this.ProductosJch = new ProductosJchViewModel();
        }
    }
}
