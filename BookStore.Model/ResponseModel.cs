using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model
{
    public class ResponseModel
    {
        public string ResponseMessage { get; set; }
        public HttpStatusCode ResponseCode { get; set; }
        public object ResponseData { get; set; }
    }
}
