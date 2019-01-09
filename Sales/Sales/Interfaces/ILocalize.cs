using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Interfaces
{
    using System.Globalization;

    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();//Informacion del telefono(devuelve el idioma del telefono)

        void SetLocale(CultureInfo ci);//Para cambiar la configuracion del telefono
    }
}
