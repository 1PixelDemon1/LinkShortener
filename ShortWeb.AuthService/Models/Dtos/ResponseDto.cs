using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortModel.Models.Dtos
{
    // General purpose model that is being responded by all the api services.
    public class ResponseDto
    {
        public object? Result { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; } = true;
    }
}
