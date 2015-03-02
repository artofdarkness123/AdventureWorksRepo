using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.BizObjects.Interfaces;
using Csla;

namespace AdventureWorks.BizObjects.EFDal
{
    class ProductCategoryDal : IProductCategoryDal
    {
        public int? Id
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public Guid RowGuidId
        {
            get;
            private set;
        }

        public SmartDate ModifiedDate
        {
            get;
            private set;
        }

        public int Create(string name)
        {
            using (var entities = new AdventureWorks2014Entities())
            {
                entities.Database.Log += WriteLog;
                var model = entities.ProductCategories.Add(new ProductCategory { Name = name, rowguid = Guid.NewGuid(), ModifiedDate = DateTime.Now });
                entities.SaveChanges();
                return model.ProductCategoryID;
            }
        }

        public int Read(int? id)
        {
            using (var entities = new AdventureWorks2014Entities())
            {
                entities.Database.Log += WriteLog;
                var model = entities.ProductCategories.SingleOrDefault(b => b.ProductCategoryID == id);
                this.Id = model.ProductCategoryID;
                this.Name = model.Name;
                this.RowGuidId = model.rowguid;
                this.ModifiedDate = model.ModifiedDate;

                return 1;
            }
        }

        public int Update(int? id, string name, Guid rowGuidId, SmartDate modifiedDate)
        {
            using (var entities = new AdventureWorks2014Entities())
            {
                try
                {
                    entities.Database.Log += WriteLog;
                    var model = entities.ProductCategories.SingleOrDefault(b => b.ProductCategoryID == id);
                    model.ModifiedDate = DateTime.Now;
                    model.rowguid = rowGuidId;
                    model.Name = name;
                    return entities.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    var temp = ex.EntityValidationErrors.Select(b => b.ValidationErrors.Select(c => c.ErrorMessage)).ToList();
                    throw;
                }
            }
        }

        public int Delete(int? id, SmartDate modifiedDate)
        {
            using (var entities = new AdventureWorks2014Entities())
            {
                entities.Database.Log += WriteLog;
                var model = entities.ProductCategories
                    .SingleOrDefault(b => b.ProductCategoryID == id && b.ModifiedDate == modifiedDate.Date);
                entities.ProductCategories.Remove(model);
                return entities.SaveChanges();
            }
        }

        public List<int> RetreiveCollection(int? id = null)
        {
            using (var entities = new AdventureWorks2014Entities())
            {
                entities.Database.Log += WriteLog;
                return entities.ProductCategories
                    .Where(b => b.ProductCategoryID == id || !id.HasValue)
                    .Select(b => b.ProductCategoryID)
                    .ToList();
            }
        }

        private void WriteLog(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
