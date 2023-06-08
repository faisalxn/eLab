using eLab.Data;

namespace eLab.Repository.Interfaces
{
    public interface ILabTestRepository : IRepository<LabTest>
    {
        List<LabTest> GetAllTest();

        LabTest GetTestById(int id);
    }
}
