using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netrex.Frontend.Application.Commons
{
    public class ValidationError
    {
        public string Field { get; set; }
        public List<string> Errors { get; set; }

    }
}
