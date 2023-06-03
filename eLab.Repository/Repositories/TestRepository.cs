using eLab.Data;
using eLab.Repository.Interfaces;
using System.Linq.Expressions;

namespace eLab.Repository.Repositories
{
    public class TestRepository : Repository<Test>, ITestRepository
    {
        public TestRepository(ApplicationDbContext context) : base(context)
        {
        }

        public List<Test> GetAllTest()
        {
            Func<IQueryable<Test>, IOrderedQueryable<Test>> orderByParameters = m => m.OrderByDescending(m => m.id);

            return GetAll(orderBy: orderByParameters).ToList();
        }

        public Test GetTestById(int id)
        {
            Expression<Func<Test, bool>> filterParameter = m => m.id == id;

            return GetAll(filter: filterParameter).FirstOrDefault();
        }
    }
}
