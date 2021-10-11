using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeTest.Nasa.Models
{
    public class ResponseViewModel
    {
        public ResponseViewModel()
        {
            ResponseList = new List<CallResponse>();
        }
        public List<CallResponse> ResponseList;
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
