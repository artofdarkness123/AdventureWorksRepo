using System;
using System.ComponentModel.DataAnnotations;
using AdventureWorks.BizObjects.Interfaces;
using Csla;

namespace AdventureWorks.BizObjects
{
    [Serializable]
    public class ProductSubcategory : BusinessBase<ProductSubcategory>
    {
        #region Business Methods
        public static readonly PropertyInfo<int?> IdProperty = RegisterProperty<int?>(p => p.Id, "ID", null, RelationshipTypes.PrivateField);
        private int? id = IdProperty.DefaultValue;
        [Display(Name = "ID")]
        public int? Id
        {
            get { return GetProperty(IdProperty, id); }
            set { SetProperty(IdProperty, ref id, value); }
        }

        public static readonly PropertyInfo<int?> ProductCategoryIdProperty = RegisterProperty<int?>(b => b.ProductCategoryId);
        public int? ProductCategoryId
        {
            get { return GetProperty(ProductCategoryIdProperty); }
            set { SetProperty(ProductCategoryIdProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name, "Name");
        [Display(Name = "Name")]
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }
        
        public static readonly PropertyInfo<Guid> RowGuidIdProperty = RegisterProperty<Guid>(c => c.RowGuidId, "Row GUID ID");
        public Guid RowGuidId
        {
          get { return GetProperty(RowGuidIdProperty); }
          set { SetProperty(RowGuidIdProperty, value); }
        }

        public static readonly PropertyInfo<SmartDate> ModifiedDateProperty = RegisterProperty<SmartDate>(c => c.ModifiedDate, "Modified Date", new SmartDate());
        public string ModifiedDate
        {
            get { return GetPropertyConvert<SmartDate, string>(ModifiedDateProperty); }
            set { SetPropertyConvert<SmartDate, string>(ModifiedDateProperty, value); }
        }


        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(NameProperty));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.MaxLength(NameProperty, 50));
        }

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //BusinessRules.AddRule(...);
        }

        #endregion

        #region Factory Methods

        internal static ProductSubcategory NewEditableChild()
        {
            return DataPortal.CreateChild<ProductSubcategory>();
        }

        internal static ProductSubcategory GetProductSubcategory(object childData)
        {
            return DataPortal.FetchChild<ProductSubcategory>(childData);
        }

        private ProductSubcategory()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        protected override void Child_Create()
        {
            // TODO: load default values
            // omit this override if you have no defaults to set
            base.Child_Create();
        }

        private void Child_Fetch(object childData)
        {
            var dal = childData as IProductSubcategoryDal;
            if (dal != null)
            {
                this.id = dal.Id;
                this.LoadProperty(ProductCategoryIdProperty, dal.CategoryId);
                this.LoadProperty(NameProperty, dal.Name);
                this.LoadProperty(RowGuidIdProperty, dal.RowGuidId);
                this.LoadPropertyConvert(ModifiedDateProperty, dal.ModifiedDate);
            }
        }

        private void Child_Insert(object parent)
        {
            var dal = parent as IProductSubcategoryDal;
            int tempId = dal.Create(this.ReadProperty(ProductCategoryIdProperty), this.ReadProperty(NameProperty));

            if (tempId <= 0)
                throw new InvalidOperationException("Creation failed.");
            else
                this.id = tempId;

            FieldManager.UpdateChildren();
        }

        private void Child_Update(object parent)
        {
            var dal = parent as IProductSubcategoryDal;
            int rowsAffected = dal.Update(
                this.id,
                this.ReadProperty(ProductCategoryIdProperty),
                this.ReadProperty(NameProperty),
                this.ReadProperty(RowGuidIdProperty),
                this.ReadProperty(ModifiedDateProperty));

            if (rowsAffected != 1)
                throw new InvalidOperationException("Invalid number of rows updated");

            FieldManager.UpdateChildren();
        }

        private void Child_DeleteSelf(object parent)
        {
            var dal = parent as IProductSubcategoryDal;
            int rowsDeleted = dal.Delete(this.id, this.ReadProperty(ModifiedDateProperty));

            if (rowsDeleted != 1)
                throw new InvalidOperationException("Invalid number of rows deleted");

            FieldManager.UpdateChildren();
        }
        #endregion
    }
}
