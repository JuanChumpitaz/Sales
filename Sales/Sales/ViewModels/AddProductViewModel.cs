

namespace Sales.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Xamarin.Forms;
    using Services;
    using Sales.Common.Models;
    using System.Linq;
    using Plugin.Media.Abstractions;
    using System;
    using Plugin.Media;

    public class AddProductViewModel : BaseViewModel
    {
        #region Attibutes
        private MediaFile file;
        private bool isRunning;
        private bool isEnabled;
        private ApiService apiService;
        private ImageSource imageSource;
        #endregion

        #region Properties
        public string Description { get; set; }
        public string Remarks { get; set; }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { this.SetValue(ref this.isEnabled, value); }
        }
        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { this.SetValue(ref this.imageSource, value); }
        }
        #endregion

        #region Constructors
        public AddProductViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.ImageSource = "NoProduct";
        }
        #endregion

        #region Commands
        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }

        private async void ChangeImage()//METODO PARA TOMAR FOTO O SELECCIONAR IMAGEN DE LA GALERIA
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.ImageSource,
                Languages.Cancel,
                null,
                Languages.FromGallery,
                Languages.NewPicture);

            if (source == Languages.Cancel)
            {
                this.file = null;
                return;
            }

            if (source == Languages.NewPicture) // PARA ABRIR LA CAMARA Y TOMAR FOTO
            {
                this.file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync();// PARA ABRIR LA GALERIA Y ELEGIR ALGUNA IMAGEN
            }

            if (this.file != null)// SI EL USUARIO SI SELECCIONÓ LA IMAGEN DE LA GALERIA
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });
            }
        }


        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(save);
            }
        }

        private async void save()
        {
            if(string.IsNullOrEmpty(this.Description))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error, 
                    Languages.DescriptionError, 
                    Languages.Accept);
                return;
            }

            // CODIGO EJEMPLO EN EL CASO DE INGRESO DE DINERO(DECIMAL)
            //if(string.IsNullOrEmpty(this.Price))
            //{
            //    await Application.Current.MainPage.DisplayAlert(
            //        Languages.Error, 
            //        Languages.PriceError, 
            //        Languages.Accept);
            //    return;
            //}
            //var price = decimal.Parse(this.Price); 
            //if (price < 0)// si el precio es menor a 0
            //{
            //    await Application.Current.MainPage.DisplayAlert(
            //        Languages.Error,
            //        Languages.PriceError,
            //        Languages.Accept);
            //    return;
            //}


            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }

            var Tbm_produ_jch = new TBM_PRODU_JCH
            {
                DES_PRODU = this.Description,
                Remarks = this.Remarks,
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductosJchController"].ToString();

            var response = await this.apiService.Post<TBM_PRODU_JCH>(url, prefix, controller,Tbm_produ_jch);//

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);//
                return;
            }

            var newTbm_produ_jch = (TBM_PRODU_JCH)response.Result;//OBTENGO EL NUEVO OBJ DE PRODUCTO QUE SE ESTAN INGRESANDO
            var viewModel = ProductosJchViewModel.GetInstance();// OBTENGO EL OBJ DE TODOS LOS PRODUCTOS QUE YA TENIA
            viewModel.ProductosJch.Add(newTbm_produ_jch);// AL MODELO Q CONTENIA TODOS LOS DATOS(LISTADO GENERAL) LE AGREGO EL NUEVO OBJ 
            

            this.IsRunning = false;
            this.IsEnabled = true;

            await Application.Current.MainPage.Navigation.PopAsync();// desapilamiento

        }
        #endregion
    }
}
