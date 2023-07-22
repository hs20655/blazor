using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.CreationalPatterns.Singleton
{
    public class SingletonExample
    {
        private static SingletonExample _instance;

        private SingletonExample(){}

        public static SingletonExample GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SingletonExample();
            }
            return _instance;
        }

        public void BusinessLogic() 
        {
        }
    }
}
