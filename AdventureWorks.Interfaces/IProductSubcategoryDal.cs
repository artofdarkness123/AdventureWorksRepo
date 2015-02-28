using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.BizObjects.Interfaces
{
    public interface IProductSubcategoryDal
    {
        int Id { get; }
        int CategoryId { get; }
        string Name { get; }
        Guid RowGuidId { get; }
        SmartDate ModifiedDate { get; }

        int Create(int categoryId, string name);

        int Read(int id);

        int Update(int id, int categoryId, string name, Guid rowGuidId, SmartDate modifiedDate);

        int Delete(int id, SmartDate modifiedDate);
    }
}
