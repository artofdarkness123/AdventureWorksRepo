using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AdventureWorks.BizObjects.Interfaces;
using AdventureWorks.Configuration;
using Csla;
using Csla.Rules;
using Microsoft.Practices.Unity;

namespace AdventureWorks.BizObjects
{
    [Serializable]
    public class ProductCategoryCollection :
      BusinessListBase<ProductCategoryCollection, ProductCategory>
    {
        #region Factory Methods

        internal static ProductCategoryCollection NewProductCategoryCollection()
        {
            return DataPortal.CreateChild<ProductCategoryCollection>();
        }

        internal static ProductCategoryCollection GetProductCategoryCollection(
          object childData)
        {
            return DataPortal.FetchChild<ProductCategoryCollection>(childData);
        }

        private ProductCategoryCollection()
        { }
        #endregion

        #region Data Access
        private void Child_Fetch(object childData)
        {
            RaiseListChangedEvents = false;

            var dal = IOC.Container.Resolve<IProductCategoryDal>();

            foreach (var child in dal.RetreiveCollection())
                this.Add(ProductCategory.GetProductCategory(child));

            RaiseListChangedEvents = true;
        }
        #endregion
    }
}
