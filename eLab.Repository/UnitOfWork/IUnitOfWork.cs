using eLab.Repository.Interfaces;

namespace eLab.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ILabTestRepository LabTestRepository { get; }

        int Save();
    }
}
