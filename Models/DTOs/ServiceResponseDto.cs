using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URL_shortening_Service.Models.DTOs
{
    public class ServiceResponseDto<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } 

    }
}