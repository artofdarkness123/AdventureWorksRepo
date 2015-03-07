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

                collection = ProductCategoryCollection.GetProductCategoryCollection();
                var childCat = collection.FirstOrDefault();
                ProductSubcategoryCollection subCol = ProductSubcategoryCollection.GetProductSubcategoryCollection(childCat.Id.Value);

                foreach (ProductSubcategory subcategory in subCol)
                {
                    Console.WriteLine("cat = {0}. sub = {1}", childCat.Name, subcategory.Name);
                }
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
