using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atmos
{
    public class Layer
    {
       private IGas gas;
       private double thickness = 0.0;
       
        public class InvalidThicknessExeption : Exception { }
       public Layer(IGas g, double th) 
        {
            gas = g;
           if (!(th > 0.5)) throw new InvalidThicknessExeption();
            thickness = th;
        }
        public Layer() { }
        public override string ToString()
        {
            return "G:" + getGas().stringGas() + " Th:" + 
                getThickness() ;
        }
        public void setGas(IGas g) {  gas = g; }
        public void setThickness(double th) {  thickness = th; }

        public IGas getGas() { return gas; }
        public double getThickness() {  return thickness; }

       

    }
}
