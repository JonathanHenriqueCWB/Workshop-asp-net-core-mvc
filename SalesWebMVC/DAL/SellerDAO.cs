using SalesWebMVC.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.DAL.Exceptions;
using System.Threading.Tasks;

namespace SalesWebMVC.DAL
{
    public class SellerDAO
    {
        #region Constructor and context
        private readonly SalesWebMVCContext _context;
        public SellerDAO(SalesWebMVCContext context)
        {
            _context = context;
        }
        #endregion
        #region List
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.OrderBy(x=> x.Name).ToListAsync();
        }
        #endregion
        #region Insert
        public async Task InsertAsync(Seller seller)
        {
            _context.Seller.Add(seller);
            await _context.SaveChangesAsync();
        }
        #endregion
        #region FindById
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Departments).FirstOrDefaultAsync(x => x.SellerId == id);
        }
        #endregion
        #region Remove
        public async Task RemoveAsync(int id)
        {
            /*
             * Quando ocorre uma violação de integridade refencial o 
             * entity framework lança uma exeção DbUpdateException 
            */

            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                //Lançamento de uma exeção personalizada criada (IntegrityException)
                throw new IntegrityException("Can't delete seller because he/she has salles");
            }
        }
        #endregion
        #region Update
        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.SellerId == obj.SellerId);
            if (!hasAny)
            {
                throw new NotFoundException("ID Not Found!");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

        }
        #endregion

    }
}
