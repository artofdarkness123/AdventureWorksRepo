using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Configuration
{
    public static class IOC
    {
        public static IUnityContainer Container { get; private set; }
        public static void ConfigureContainer() 
        {
            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            IOC.Container = new UnityContainer().LoadConfiguration(section);
        }
    }
}
