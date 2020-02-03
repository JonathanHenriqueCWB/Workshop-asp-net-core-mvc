using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.DAL.Exceptions
{
    /*
     * Execão persolizada de serviço para erros de integridade referencial
     * Ela ira cuidar dos possiveis delete com chave estrangeira
     */

    public class IntegrityException :ApplicationException
    {
        public IntegrityException(string message) :base(message)
        {
        }
    }
}
