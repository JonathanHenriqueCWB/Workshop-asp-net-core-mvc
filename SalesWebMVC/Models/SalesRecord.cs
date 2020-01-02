using SalesWebMVC.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models
{
    public class SalesRecord
    {
        public int SalesRecordId { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        //Uma venda possue um status pendente, faturado ou cancelado
        public SaleStatus Status { get; set; }
        //Uma venda possue um vendedor
        public Seller Seller { get; set; }

        #region CONSTRUTORES
        public SalesRecord()
        {

        }
        public SalesRecord(DateTime date, double amount, SaleStatus status, Seller seller)
        {
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }
        #endregion
    }
}
