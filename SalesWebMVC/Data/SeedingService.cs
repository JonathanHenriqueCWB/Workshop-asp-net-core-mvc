using SalesWebMVC.Models;
using SalesWebMVC.Models.Enums;
using System;
using System.Linq;

namespace SalesWebMVC.Data
{
    public class SeedingService
    {
        private SalesWebMVCContext _context;
        public SeedingService(SalesWebMVCContext context)
        {
            _context = context;
        }

        //Método/funcão responsavel por popular a base de dados
        public void Seed()
        {
            //Testa se existe algum registro na tabela
            if (_context.Departments.Any() ||
                _context.Seller.Any() ||
                _context.SalesRecord.Any())
            {
                return;
            }

            Departments d1 = new Departments("Computers");
            Departments d2 = new Departments("Electronics");
            Departments d3 = new Departments("Books");

            Seller s1 = new Seller("Bob", "bob@email.com", 1000, new DateTime(1998, 4, 21), d1);
            Seller s2 = new Seller("Alex", "alex@email.com", 1500, new DateTime(1990, 7, 15), d1);

            SalesRecord sr1 = new SalesRecord(DateTime.Now, 11000, SaleStatus.Billed, s1);
            SalesRecord sr2 = new SalesRecord(DateTime.Now, 11000, SaleStatus.Canceled, s2);
            SalesRecord sr3 = new SalesRecord(DateTime.Now, 11000, SaleStatus.Pending, s2);

            //Grava diversos objetos de uma so vez
            _context.Departments.AddRange(d1, d2, d3);
            _context.Seller.AddRange(s1, s2);
            _context.SalesRecord.AddRange(sr1, sr2, sr3);
            _context.SaveChanges();
        }
    }
}
