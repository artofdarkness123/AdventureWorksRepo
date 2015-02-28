using System;
using Csla;

namespace AdventureWorks.BizObjects
{
    [Serializable]
    public class ProductSubcategory : BusinessBase<ProductSubcategory>
    {
        #region Business Methods

        // TODO: add your own fields, properties and methods

        // example with private backing field
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id, RelationshipTypes.PrivateField);
        private int _Id = IdProperty.DefaultValue;
        public int Id
        {
            get { return GetProperty(IdProperty, _Id); }
            set { SetProperty(IdProperty, ref _Id, value); }
        }

        // example with managed backing field
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            //BusinessRules.AddRule(new Rule(), IdProperty);
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

        internal static ProductSubcategory GetEditableChild(object childData)
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
