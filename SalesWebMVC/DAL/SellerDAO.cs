using SalesWebMVC.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.DAL.Exceptions;

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
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }
        #endregion
        #region Insert
        public void Insert(Seller seller)
        {
            _context.Seller.Add(seller);
            _context.SaveChanges();
        }
        #endregion
        #region FindById
        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Departments).FirstOrDefault(x => x.SellerId == id);
        }
        #endregion
        #region Remove
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
        #endregion
        public void Update(Seller obj)
        {
            if (!_context.Seller.Any(x => x.SellerId == obj.SellerId))
            {
                throw new NotFoundException("ID Not Found!");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

        }

    }
}
