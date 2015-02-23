using System;
using Csla;
using System.ComponentModel.DataAnnotations;
using Csla.Rules;

namespace AdventureWorks.BizObjects
{
    [Serializable]
    public class ProductCategory : BusinessBase<ProductCategory>
    {
        #region Business Methods
        // example with private backing field
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id, "ID", 0, RelationshipTypes.PrivateField);
        private int id = IdProperty.DefaultValue;
        [Display(Name = "ID")]
        public int Id
        {
            get { return CanReadProperty(IdProperty) ? GetProperty(IdProperty, id) : default(int); }
            private set
            {
                if (CanWriteProperty(IdProperty)) { SetProperty(IdProperty, ref id, value); }
            }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name, "Name");
        [Display(Name = "Name")]
        public string Name
        {
            get { return CanReadProperty(NameProperty) ? GetProperty(NameProperty) : String.Empty; }
            set
            {
                if (CanWriteProperty(NameProperty))
                {
                    SetProperty(NameProperty, value);
                }
            }
        }

        public static readonly PropertyInfo<Guid> RowGuidIdProperty = RegisterProperty<Guid>(c => c.RowGuidId, "Row GUID ID");
        [Display(Name = "Row GUID ID")]
        public Guid RowGuidId
        {
            get { return CanReadProperty(RowGuidIdProperty) ? GetProperty(RowGuidIdProperty) : Guid.Empty; }
            private set { LoadProperty(RowGuidIdProperty, value); }
        }

        public static readonly PropertyInfo<SmartDate> ModifiedDateProperty = RegisterProperty<SmartDate>(c => c.ModifiedDate, "Modified Date");
        [Display(Name = "Modified Date")]
        public string ModifiedDate
        {
            get { return CanReadProperty(ModifiedDateProperty) ? GetPropertyConvert<SmartDate, string>(ModifiedDateProperty) : SmartDate.EmptyValue.MinDate.ToString(); }
            private set { LoadPropertyConvert<SmartDate, string>(ModifiedDateProperty, value); }
        }
        #endregion

        #region Business Rules
        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            //BusinessRules.AddRule(new Rule(), IdProperty);
            BusinessRules.AddRule(new Csla.Rules.CommonRules.MaxLength(NameProperty, 50));
        }

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //BusinessRules.AddRule(...);
        }
        #endregion

        #region Factory Methods
        internal static ProductCategory NewProductCategory()
        {
            return DataPortal.CreateChild<ProductCategory>();
        }

        internal static ProductCategory GetProductCategory(object childData)
        {
            return DataPortal.FetchChild<ProductCategory>(childData);
        }

        private ProductCategory()
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
            // TODO: load values
        }

        private void Child_Insert(object parent)
        {
            // TODO: insert values
        }

        private void Child_Update(object parent)
        {
            // TODO: update values
        }

        private void Child_DeleteSelf(object parent)
        {
            // TODO: delete values
        }
        #endregion
    }
}
