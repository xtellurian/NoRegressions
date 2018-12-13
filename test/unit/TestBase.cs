using Microsoft.VisualStudio.TestTools.UnitTesting;
using core.Dependencies;

namespace unit
{

    public class TestBase
    {
        public TestBase()
        {
            
        }

        protected T Resolve<T>() where T:class
        {
            return Dependencies.Resolve<T>();
        }
    }
}
