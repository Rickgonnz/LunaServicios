using Models;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private MantenedorApiContext _context;
        public IRepository<Cliente> _cliente;

        public UnitOfWork(MantenedorApiContext context)
        {
            _context = context;
        }

        public IRepository<Cliente> Cliente
        {
            get
            {
                return _cliente == null ?
                    _cliente = new Repository<Cliente>(_context) :
                    _cliente;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
