using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.BizObjects;
using AdventureWorks.Configuration;
using Csla;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductCategoryCollection collection = null;
            try
            {
                IOC.ConfigureContainer();

                collection = ProductCategoryCollection.NewProductCategoryCollection();
                var childCat = collection.AddNew();
                childCat.Name = "Electronics";

                var childSub = childCat.SubcategoryCollection.AddNew();

                childSub.Name = "TVs";

                collection.Save();

                Console.WriteLine(childCat.ModifiedDate);
            }
            catch (DataPortalException ex)
            {
                Console.WriteLine(ex.BusinessException.ToString());
            }
            catch (Csla.Rules.ValidationException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("DONE");
            Console.ReadKey();
        }
    }
}
