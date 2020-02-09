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
            
            SalesRecord sr1 = new SalesRecord(new DateTime(2020, 01, 01), 1000, SaleStatus.Billed, s1);
            SalesRecord sr2 = new SalesRecord(new DateTime(2020, 01, 02), 1000, SaleStatus.Canceled, s2);
            SalesRecord sr3 = new SalesRecord(new DateTime(2020, 01, 03), 1000, SaleStatus.Pending, s2);
            SalesRecord sr4 = new SalesRecord(new DateTime(2020, 01, 04), 1000, SaleStatus.Pending, s2);
            SalesRecord sr5 = new SalesRecord(new DateTime(2020, 01, 05), 1000, SaleStatus.Pending, s2);
            SalesRecord sr6 = new SalesRecord(new DateTime(2020, 01, 06), 1000, SaleStatus.Pending, s2);
            SalesRecord sr7 = new SalesRecord(new DateTime(2020, 01, 07), 1000, SaleStatus.Pending, s2);
            SalesRecord sr8 = new SalesRecord(new DateTime(2020, 01, 08), 1000, SaleStatus.Pending, s2);
            SalesRecord sr9 = new SalesRecord(new DateTime(2020, 01, 09), 1000, SaleStatus.Canceled, s2);
            SalesRecord sr10 = new SalesRecord(new DateTime(2020, 01, 10), 1000, SaleStatus.Pending, s1);
            
            SalesRecord sr11 = new SalesRecord(new DateTime(2019, 01, 01), 10000, SaleStatus.Billed, s2);
            SalesRecord sr12 = new SalesRecord(new DateTime(2019, 01, 02), 10000, SaleStatus.Billed, s1);
            SalesRecord sr13 = new SalesRecord(new DateTime(2019, 01, 03), 10000, SaleStatus.Billed, s1);
            SalesRecord sr14 = new SalesRecord(new DateTime(2019, 01, 04), 10000, SaleStatus.Pending, s2);
            SalesRecord sr15 = new SalesRecord(new DateTime(2019, 01, 05), 10000, SaleStatus.Canceled, s1);
            SalesRecord sr16 = new SalesRecord(new DateTime(2019, 01, 06), 10000, SaleStatus.Pending, s2);
            SalesRecord sr17 = new SalesRecord(new DateTime(2019, 01, 07), 10000, SaleStatus.Pending, s1);
            SalesRecord sr18 = new SalesRecord(new DateTime(2019, 01, 08), 10000, SaleStatus.Canceled, s2);
            SalesRecord sr19 = new SalesRecord(new DateTime(2019, 01, 09), 10000, SaleStatus.Billed, s1);
            SalesRecord sr20 = new SalesRecord(new DateTime(2019, 01, 10), 10000, SaleStatus.Pending, s2);
            
            SalesRecord sr21 = new SalesRecord(new DateTime(2020, 02, 02), 500, SaleStatus.Pending, s1);
            SalesRecord sr22 = new SalesRecord(new DateTime(2020, 02, 03), 500, SaleStatus.Billed, s2);
            SalesRecord sr23 = new SalesRecord(new DateTime(2020, 02, 04), 500, SaleStatus.Pending, s1);
            SalesRecord sr24 = new SalesRecord(new DateTime(2020, 02, 05), 500, SaleStatus.Billed, s2);
            SalesRecord sr25 = new SalesRecord(new DateTime(2020, 02, 06), 500, SaleStatus.Pending, s1);
            
            //Grava diversos objetos de uma so vez
            _context.Departments.AddRange(d1, d2, d3);
            _context.Seller.AddRange(s1, s2);
            _context.SalesRecord.AddRange(sr1, sr2, sr3,sr4, sr5, sr6, sr7, sr8, sr9, sr10, sr11, sr12, sr13, sr14, sr15, sr16, sr17, sr18, sr19, sr20, sr21, sr22, sr23, sr24, sr25);
            _context.SaveChanges();
        }
    }
}
