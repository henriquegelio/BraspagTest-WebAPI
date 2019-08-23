using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BraspagTest_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BraspagTest_WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly MdrContext _context;

        public TransactionController(MdrContext context)
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

        //POST: /transaction
        [HttpPost]
        public async Task<ActionResult<string>> PostMdr(Transaction transaction)
        {
            double liquid;

            if (Double.TryParse(transaction.Valor.ToString(), out liquid))
            {
                List<Mdr> lstMdr = new List<Mdr>();
                foreach (Mdr m in _context.Mdrs)
                {
                    lstMdr.Add(m);
                }
                Mdr mdr = lstMdr.Where(x => x.Adquirinte == transaction.Adquirinte || x.Adquirinte.Substring(x.Adquirinte.Length - 1, 1) == transaction.Adquirinte).FirstOrDefault();
                await _context.SaveChangesAsync();

                if (mdr != null)
                {
                    if (transaction.Bandeira == "Visa" || transaction.Bandeira == "visa")
                    {
                        Taxa taxa = (Taxa)mdr.Taxas.Where(x => x.Bandeira == "Visa").FirstOrDefault();
                        if (transaction.Tipo == "credito" || transaction.Tipo == "Credito" || transaction.Tipo == "crédito" || transaction.Tipo == "Crédito")
                        {
                            liquid = liquid * (100.0 - taxa.Credito) / 100;
                        }
                        else if (transaction.Tipo == "debito" || transaction.Tipo == "Debito" || transaction.Tipo == "débito" || transaction.Tipo == "Débito")
                        {
                            liquid = liquid * (100.0 - taxa.Debito) / 100;
                        }

                        else
                        {
                            return "Not Found";
                        }
                    }

                    else if (transaction.Bandeira == "Master" || transaction.Bandeira == "master")
                    {
                        Taxa taxa = (Taxa)mdr.Taxas.Where(x => x.Bandeira == "Master").FirstOrDefault();
                        if (transaction.Tipo == "credito" || transaction.Tipo == "Credito" || transaction.Tipo == "crédito" || transaction.Tipo == "Crédito")
                        {
                            liquid = liquid * (100.0 - taxa.Credito) / 100;
                        }
                        else if (transaction.Tipo == "debito" || transaction.Tipo == "Debito" || transaction.Tipo == "débito" || transaction.Tipo == "Débito")
                        {
                            liquid = liquid * (100.0 - taxa.Debito) / 100;
                        }

                        else
                        {
                            return "Not Found";
                        }
                    }

                    else
                    {
                        return "Not Found";
                    }

                    var obj = new
                    {
                        ValorLiquido = liquid
                    };

                    return Json(obj);
                }

                else
                {
                    return "Not Found";
                }
            }

            else
            {
                return "Incorrect input";
            }
        }
    }
}
