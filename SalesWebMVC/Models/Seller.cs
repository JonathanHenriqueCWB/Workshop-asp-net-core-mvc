using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        #region
        public int SellerId { get; set; }
        #endregion
        #region Name
        [Required(ErrorMessage ="{0} Name required")]
        [StringLength(maximumLength:60, MinimumLength =3, ErrorMessage ="{0} Min 3 max 60 char")]
        public string Name { get; set; }
        #endregion
        #region Email
        [Required(ErrorMessage ="{0} required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="Enter a valid email")]
        public string Email { get; set; }
        #endregion
        #region Base salary
        [Required(ErrorMessage ="{0} required")]
        [Display(Name="Base Salary")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        [Range(100.0,50000.0, ErrorMessage ="{0} must be from {1} to {2}")]
        public double BaseSalary { get; set; }
        #endregion
        #region Birth date
        [DataType(DataType.Date)]
        [Display(Name="Birth Date")]
        [Required(ErrorMessage ="{0} required")]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
        #endregion
        #region Departments
        //Um vendedor possue um departamento
        public Departments Departments { get; set; }
        #endregion
        #region List Sales
        //Um vendedor possue varias vendas
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();
        #endregion
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
