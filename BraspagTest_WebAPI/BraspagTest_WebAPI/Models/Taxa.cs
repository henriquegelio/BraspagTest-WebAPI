using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BraspagTest_WebAPI.Models
{
    public class Taxa
    {
        public long Id { get; set; }
        public string Bandeira { get; set; }
        public double Credito { get; set; }
        public double Debito { get; set; }
    }
}
