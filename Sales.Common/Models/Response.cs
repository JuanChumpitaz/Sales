using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Common.Models
{
    public class Response //Clase que nos va devolver el servicio
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}
