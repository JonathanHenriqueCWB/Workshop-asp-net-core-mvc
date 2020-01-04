using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }
        #endregion
    }
}
