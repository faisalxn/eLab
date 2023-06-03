using eLab.Repository.Interfaces;

namespace eLab.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ITestRepository TestRepository { get; }

        int Save();
    }
}
