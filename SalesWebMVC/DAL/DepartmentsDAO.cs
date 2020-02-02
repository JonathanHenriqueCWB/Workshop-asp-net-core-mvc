using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.DAL
{
    public class DepartmentsDAO
    {
        #region Constructor and context
        private readonly SalesWebMVCContext _context;
        public DepartmentsDAO(SalesWebMVCContext context)
        {
            _context = context;
        }
        #endregion
        #region List
        public async Task<List<Departments>> FindAllAsync()
        {
            return await _context.Departments.OrderBy(x => x.Nome).ToListAsync();
        }
        #endregion
        #region Find To Id
        public async Task<Departments> FindToIdAsync(int? id)
        {
            return await _context.Departments.FindAsync(id);
        }
        #endregion
    }
}
