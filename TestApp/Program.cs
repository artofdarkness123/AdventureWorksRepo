using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.BizObjects;
using AdventureWorks.Configuration;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IOC.ConfigureContainer();

                var collection = ProductCategoryCollection.GetProductCategoryCollection(null);

                foreach (ProductCategory c in collection)
                {
                    Console.WriteLine(c.RowGuidId);
                }
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
