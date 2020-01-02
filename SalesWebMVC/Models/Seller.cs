using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int SellerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public double BaseSalary { get; set; }
        public DateTime BirthDate { get; set; }
        //Um vendedor possue um departamento
        public Departments Departments { get; set; }
        //Um vendedor possue varias vendas
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        #region CONSTRUTORES
        public Seller()
        {
        }
        public Seller(string name, string email, double baseSalary, DateTime birthDate, Departments departments)
        {
            Name = name;
            Email = email;
            BaseSalary = baseSalary;
            BirthDate = birthDate;
            Departments = departments;
        }
        #endregion
        #region OPERAÇÕES
        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }
        public void RemoveSale(SalesRecord sr)
        {
            Sales.Remove(sr);
        }
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(x => x.Date >= initial && x.Date <= final).Sum(x => x.Amount);
        }
        #endregion
    }
}
