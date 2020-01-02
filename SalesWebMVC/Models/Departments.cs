using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models
{
    public class Departments
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        //Um departamento tem uma lista de vendedor
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        #region CONSTRUTORES
        public Departments()
        {
        }
        public Departments(string nome)
        {
            Nome = nome;
        }
        #endregion
        #region OPERAÇÕES
        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(x => x.TotalSales(initial, final));
        }
        #endregion
    }
}
