using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Nasa.Models
{
    public class ParsedDate
    {
        public string StringDate { get; set; }
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
