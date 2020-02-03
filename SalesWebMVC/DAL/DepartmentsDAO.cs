using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.DAL.Exceptions;

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
        #region Create
        public async Task CreateAsync(Departments departments)
        {
            _context.Add(departments);
            await _context.SaveChangesAsync();
        }
        #endregion
        #region Edit
        public async Task UpateAsync(Departments departments)
        {
            bool hasAny = await _context.Departments.AnyAsync(x => x.Id == departments.Id);
            if (!hasAny)
            {
                throw new NotFoundException("ID Not Found!");
            }
            try
            {
                _context.Update(departments);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

        }
        #endregion
        #region Delete
        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Departments.FindAsync(id);
            _context.Departments.Remove(obj);
            await _context.SaveChangesAsync();
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
