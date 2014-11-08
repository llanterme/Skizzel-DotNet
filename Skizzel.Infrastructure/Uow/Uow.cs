using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skizzel.Infrastructure.Catalog;
using Skizzel.Infrastructure.Repository;

namespace Skizzel.Infrastructure.Uow
{
    public class Uow<T> : IDisposable where T : class
    {
        private Repository<T> _repository;
        private readonly SkizzelEntities _context = new SkizzelEntities();

        public Repository<T> Repository
        {
            get
            {
                if (this._repository == null)
                    this._repository = new Repository<T>(_context);
                return _repository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
