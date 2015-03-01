using System;
using System.Collections.Generic;
using AdventureWorks.BizObjects.Interfaces;
using AdventureWorks.Configuration;
using Csla;
using Microsoft.Practices.Unity;

namespace AdventureWorks.BizObjects
{
    [Serializable]
    public class ProductCategoryCollection :
      BusinessListBase<ProductCategoryCollection, ProductCategory>
    {
        #region Authorization Rules
        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(ProductCategoryCollection), "Role");
        }
        #endregion

        #region Factory Methods

        public static ProductCategoryCollection NewProductCategoryCollection()
        {
            return DataPortal.Create<ProductCategoryCollection>();
        }
        
        public static ProductCategoryCollection GetProductCategoryCollection(int? id = null)
        {
            return DataPortal.Fetch<ProductCategoryCollection>(new SingleCriteria<int?>(id));
        }

        private ProductCategoryCollection()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access
        private void DataPortal_Fetch(object criteria)
        {
            RaiseListChangedEvents = false;

            if (criteria is SingleCriteria<int?>)
            {
                var keyCriteria = criteria as SingleCriteria<int?>;

                var dal = IOC.Container.Resolve<IProductCategoryDal>();

                foreach (int child in dal.RetreiveCollection(keyCriteria.Value))
                {
                    int rowCount = dal.Read(child);

                    if (rowCount != 1)
                        throw new InvalidOperationException("Invalid number of rows retrieved");
                    else
                    {
                        this.Add(ProductCategory.GetProductCategory(dal));
                    }
                }
            }

            RaiseListChangedEvents = true;
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            // TODO: open database, update values
            var dal = IOC.Container.Resolve<IProductCategoryDal>();
            base.Child_Update(dal);
        }
        #endregion
    }
}
