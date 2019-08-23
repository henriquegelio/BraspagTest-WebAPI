using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BraspagTest_WebAPI.Models
{
    public class Transaction
    {
        public long Id { get; set; }
        public double Valor { get; set; }
        public String Adquirinte { get; set; }
        public String Bandeira { get; set; }
        public string Tipo { get; set; }
    }
}
