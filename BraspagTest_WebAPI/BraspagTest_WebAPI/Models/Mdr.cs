using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BraspagTest_WebAPI.Models
{
    public class Mdr
    {
        public long Id { get; set; }
        public string Adquirinte { get; set; }

        public List<Taxa> Taxas { get; set; }
        //public string Bandeira { get; set; }
        //public string Tipo { get; set; }
        //public double Desconto { get; set; }
        //public string Name { get; set; }
        //public bool IsComplete { get; set; }
    }
}
