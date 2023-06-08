using eLab.Data;
using eLab.Repository.Interfaces;
using System.Linq.Expressions;

namespace eLab.Repository.Repositories
{
    public class LabTestRepository : Repository<LabTest>, ILabTestRepository
    {
        public LabTestRepository(ApplicationDbContext context) : base(context)
        {
        }

        public List<LabTest> GetAllTest()
        {
            Func<IQueryable<LabTest>, IOrderedQueryable<LabTest>> orderByParameters = m => m.OrderByDescending(m => m.CreatedAt);

            Expression<Func<LabTest, bool>> filterParameter = m => m.IsDeleted == false;

            List<Expression<Func<LabTest, object>>> includeParameters = new List<Expression<Func<LabTest, object>>>
            {
                m => m.CreatedByUser, m => m.UpdatedByUser
            };

            return GetAll(orderBy: orderByParameters, filter: filterParameter, includeProperties: includeParameters).ToList();
        }

        public LabTest GetTestById(int id)
        {
            Expression<Func<LabTest, bool>> filterParameter = m => m.Id == id && m.IsActive == true && m.IsDeleted == false;

            return GetAll(filter: filterParameter).FirstOrDefault();
        }
    }
}
