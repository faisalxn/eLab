using eLab.Data;

namespace eLab.Repository.Interfaces
{
    public interface ITestRepository : IRepository<Test>
    {
        List<Test> GetAllTest();

        Test GetTestById(int id);
    }
}
