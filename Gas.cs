using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atmos
{
   
public interface IGas
      {
        public virtual bool isZ() { return false; }
        public virtual bool isX() { return false; }
        public virtual bool isC() { return false; }

        public abstract string stringGas();
       

    }
 public class Z : IGas
        {
        private static Z? instance;
        public Z getInstance()
        {
            if (instance == null)
                instance = new Z();
            return instance;
        }
        public bool isZ() { return true; }
        public string stringGas() { return "Z"; }
    }

    public class X : IGas
    {
        private static X? instance;
        public X getInstance()
        {
            if (instance == null)
                instance = new X();
            return instance;
        }
        public bool isX() { return true; }
        public string stringGas() { return "X";}
    }

    public class C : IGas
    {
        private static C? instance;
        public C getInstance()
        {
            if (instance == null)
                instance = new C();
            return instance;
        }
        public bool isC() { return true; }
        public string stringGas() { return "C";}
    }
}
