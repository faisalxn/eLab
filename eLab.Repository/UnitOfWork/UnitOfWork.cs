using eLab.Data;
using eLab.Repository.Interfaces;
using eLab.Repository.Repositories;

namespace eLab.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationDbContext _context;
        private TestRepository _testRepository;


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ITestRepository TestRepository
        {
            get
            {
                return _testRepository ?? (_testRepository = new TestRepository(_context));
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
