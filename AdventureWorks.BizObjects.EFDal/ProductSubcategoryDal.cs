using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.BizObjects.Interfaces;

namespace AdventureWorks.BizObjects.EFDal
{
    class ProductSubcategoryDal : IProductSubcategoryDal
    {
        public int? Id
        {
            get;
            private set;
        }

        public int? CategoryId
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

        public Csla.SmartDate ModifiedDate
        {
            get;
            private set;
        }

        public int Create(int? categoryId, string name)
        {
            using (var entities = new AdventureWorks2014Entities())
            {
                entities.Database.Log += WriteLog;
                var model = entities.ProductSubcategories.Add(new ProductSubcategory { Name = name, ProductCategoryID = categoryId.Value, rowguid = Guid.NewGuid(), ModifiedDate = DateTime.Now });
                entities.SaveChanges();
                return model.ProductSubcategoryID;
            }
        }

        public int Read(int? id)
        {
            using (var entities = new AdventureWorks2014Entities())
            {
                entities.Database.Log += WriteLog;
                var model = entities.ProductSubcategories.SingleOrDefault(b => b.ProductSubcategoryID == id);
                this.Id = model.ProductSubcategoryID;
                this.CategoryId = model.ProductCategoryID;
                this.Name = model.Name;
                this.RowGuidId = model.rowguid;
                this.ModifiedDate = model.ModifiedDate;

                return 1;
            }
        }

        public int Update(int? id, int? categoryId, string name, Guid rowGuidId, Csla.SmartDate modifiedDate)
        {
            using (var entities = new AdventureWorks2014Entities())
            {
                try
                {
                    entities.Database.Log += WriteLog;
                    var model = entities.ProductSubcategories.SingleOrDefault(b => b.ProductSubcategoryID == id);
                    model.ProductCategoryID = categoryId.Value;
                    model.Name = name;
                    model.rowguid = rowGuidId;
                    model.ModifiedDate = DateTime.Now;
                    return entities.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    var temp = ex.EntityValidationErrors.Select(b => b.ValidationErrors.Select(c => c.ErrorMessage)).ToList();
                    throw;
                }
            }
        }

        public int Delete(int? id, Csla.SmartDate modifiedDate)
        {
            using (var entities = new AdventureWorks2014Entities())
            {
                entities.Database.Log += WriteLog;
                var model = entities.ProductSubcategories.SingleOrDefault(b => b.ProductSubcategoryID == id && b.ModifiedDate == modifiedDate.Date);
                entities.ProductSubcategories.Remove(model);
                return entities.SaveChanges();
            }
        }

        public List<int> RetreiveCollection(int? id = null)
        {
            using (var entities = new AdventureWorks2014Entities())
            {
                entities.Database.Log += WriteLog;
                return entities.ProductSubcategories
                    .Where(b => b.ProductCategoryID == id || !id.HasValue)
                    .Select(b => b.ProductSubcategoryID)
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
