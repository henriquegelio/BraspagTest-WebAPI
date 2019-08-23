using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BraspagTest_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BraspagTest_WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MdrController : Controller
    {
        private readonly MdrContext _context;
        
        public MdrController (MdrContext context)
        {
            _context = context;

            if (_context.Mdrs.Count() != 0)
            {
                _context.Mdrs.RemoveRange(_context.Mdrs);
            }

            // Cria a tabela de MDR no momento da requisição
            _context.Mdrs.Add(new Mdr { Adquirinte = "Adquirinte A", Taxas = new List<Taxa>() { new Taxa { Bandeira = "Visa", Credito = 2.25, Debito = 2.00 }, new Taxa { Bandeira = "Master", Credito = 2.35, Debito = 1.98 } } });
            _context.Mdrs.Add(new Mdr { Adquirinte = "Adquirinte B", Taxas = new List<Taxa>() { new Taxa { Bandeira = "Visa", Credito = 2.50, Debito = 2.08 }, new Taxa { Bandeira = "Master", Credito = 2.65, Debito = 1.75 } } });
            _context.Mdrs.Add(new Mdr { Adquirinte = "Adquirinte C", Taxas = new List<Taxa>() { new Taxa { Bandeira = "Visa", Credito = 2.75, Debito = 2.16 }, new Taxa { Bandeira = "Master", Credito = 3.10, Debito = 1.58 } } });

            _context.SaveChanges();
        }

        //GET: /mdr
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mdr>>> GetMdrs()
        {
            return await _context.Mdrs.ToListAsync();
        }
    }
}
