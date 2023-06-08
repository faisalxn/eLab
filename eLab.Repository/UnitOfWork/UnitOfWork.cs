using eLab.Data;
using eLab.Repository.Interfaces;
using eLab.Repository.Repositories;

namespace eLab.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationDbContext _context;
        private LabTestRepository _labTestRepository;


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ILabTestRepository LabTestRepository
        {
            get
            {
                return _labTestRepository ?? (_labTestRepository = new LabTestRepository(_context));
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        private bool disposed = false;

        /// <summary>
        /// Object disposing
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
