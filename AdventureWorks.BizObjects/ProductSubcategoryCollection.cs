using System;
using System.Collections.Generic;
using AdventureWorks.BizObjects.Interfaces;
using AdventureWorks.Configuration;
using Csla;
using Microsoft.Practices.Unity;

namespace AdventureWorks.BizObjects
{
    [Serializable]
    public class ProductSubcategoryCollection :
      BusinessListBase<ProductSubcategoryCollection, ProductSubcategory>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(ProductSubcategoryCollection), "Role");
        }

        #endregion

        #region Factory Methods
        public static ProductSubcategoryCollection NewProductSubcategoryCollection()
        {
            return DataPortal.Create<ProductSubcategoryCollection>();
        }

        public static ProductSubcategoryCollection GetProductSubcategoryCollection(int id)
        {
            return DataPortal.Fetch<ProductSubcategoryCollection>(id);
        }

        private ProductSubcategoryCollection()
        { /* Require use of factory methods */ }
        #endregion

        #region Data Access

        private void DataPortal_Fetch(int criteria)
        {
            RaiseListChangedEvents = false;
            var dal = IOC.Container.Resolve<IProductSubcategoryDal>();
            foreach (int child in dal.RetreiveCollection(criteria))
            {
                int rowCount = dal.Read(child);

                if (rowCount != 1)
                    throw new InvalidOperationException("Invalid number of rows retrieved");
                else
                {
                    this.Add(ProductSubcategory.GetProductSubcategory(dal));
                }
            }

            RaiseListChangedEvents = true;
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            // TODO: open database, update values
            var dal = IOC.Container.Resolve<IProductSubcategoryDal>();
            base.Child_Update(dal);
        }
        #endregion
    }
}
