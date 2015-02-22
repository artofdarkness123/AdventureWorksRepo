using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.BizObjects.Interfaces
{
    public interface IProductCategory
    {
        int Id { get; }
        string Name { get; }
        Guid RowGuidId { get; }
        SmartDate ModifiedDate { get; }

        int Create(string name);

        int Read(int id);

        int Update(int id, string name, Guid rowGuidId, SmartDate modifiedDate);

        int Delete(int id, SmartDate modifiedDate);
    }
}
