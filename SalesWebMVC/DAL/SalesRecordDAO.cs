using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SalesWebMVC.DAL
{
    public class SalesRecordDAO
    {
        #region Constructor
        private readonly SalesWebMVCContext _context;
        public SalesRecordDAO(SalesWebMVCContext context)
        {
            _context = context;
        }
        #endregion

        //Retorna uma lista de vendas comforme as datas parametrizadas
        public async  Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;

            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            //Retornando valor cruazado com outras tabelas
            return await  result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Departments)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        //Retorno de grupo (Igrouping) 
        public List<IGrouping<Departments, SalesRecord>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;

            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Departments)
                .OrderByDescending(x => x.Date)
                .AsEnumerable()
                .GroupBy(x => x.Seller.Departments)
                .ToList();
        }
    }
}
