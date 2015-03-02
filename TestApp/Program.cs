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
            try
            {
                IOC.ConfigureContainer();

                var collection = ProductCategoryCollection.GetProductCategoryCollection(11);
                var childCat = collection.FirstOrDefault();
                childCat = collection.FirstOrDefault();
                collection.Remove(childCat);
                collection.Save();
            }
            catch (DataPortalException ex)
            {
                Console.WriteLine(ex.BusinessException.ToString());
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
