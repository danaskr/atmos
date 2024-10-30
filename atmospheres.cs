using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace atmos
{
    public abstract class atmospheres
    {
        public atmospheres(List<Layer> l ) { layers = l; }
       
        public List<Layer> layers = new List<Layer>(); 
        public abstract List<Layer> affect();
        
        public List<Layer> ascendANDperish(out bool perished, out Layer perishedLayer)
        {
            bool similarFound = false;
            perished = false;
            perishedLayer = new Layer();
            
            for(int i = layers.Count-1; i  >= 0; i--)
            {
                if (layers[i].getThickness() < 0.5)  // only when less than 0.15
                {
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (layers[i].getGas() == layers[j].getGas())
                        {
                            layers[j].setThickness(layers[j].getThickness() + layers[i].getThickness());
                            similarFound = true;

                        }

                        
                    }
                    if (!similarFound)
                    {
                        perished = true;
                        perishedLayer.setGas(layers[i].getGas());
                        perishedLayer.setThickness(layers[i].getThickness());
                        Console.WriteLine("update : This layer has been removed" + perishedLayer);
                        layers.RemoveAt(i);
                    }

                }
                
            } return layers;
        }

        public bool allStillExists()
        {
            bool z = false;
            bool x = false;
            bool c = false;
            for(int i=0; i < layers.Count; i++)
            {
                if (layers[i].getGas().isZ()) z = true;
                else if (layers[i].getGas().isX()) x = true;
                else if (layers[i].getGas().isC()) c= true;
                
            }
            if(z&&x&&c) return true;
            return false;
        }
        
       
    }
    public class T : atmospheres
    {
        public T(List<Layer> l) : base(l) { }

        public override List<Layer> affect()
        {
            bool theresZ = false;
            for(int i = layers.Count -1; i >= 0; i--)
            {
                if (layers[i].getGas().isX())
                {
                    for(int z = i; z >= 0; z--)
                    {
                        if (layers[z].getGas().isZ())
                        {
                            theresZ = true;
                            double li = layers[i].getThickness();
                            double lz = layers[z].getThickness();
                            layers[i].setThickness(li * 0.5);
                            layers[z].setThickness((li * 0.5) + lz);
                        }
                        
                    }
                    if (!theresZ)
                    {
                        double li = layers[i].getThickness();
                        layers[i].setThickness(li * 0.5);
                        Layer newL = new Layer();
                        newL.setGas(new Z());
                        newL.setThickness(li*0.05);
                        layers.Insert((i), newL);
                        Console.WriteLine("update : new layer was added : " + layers[i]);
                        // if no ozone was found , insert above the oxygen
                    }
                }
            }
            
            return layers;
        }
    }

   public class S : atmospheres
    {
       public S(List<Layer> l) : base(l) { }

        public override List<Layer> affect() 
        {
            bool theresZ = false;
            bool theresX = false;
            for(int i = layers.Count-1; i >= 0; i--)
            {
                if (layers[i].getGas().isX()) // when seeing OX
                {
                    for (int z = i; z >= 0; z--)
                    {
                        if (layers[z].getGas().isZ())
                        {
                            theresZ = true;
                            double li = layers[i].getThickness();
                            double lz = layers[z].getThickness();
                            layers[i].setThickness(li * 0.95);
                            layers[z].setThickness((li * 0.05) + lz);
                        }

                    }
                    if (!theresZ)
                    {
                        double li = layers[i].getThickness();
                        layers[i].setThickness(li * 0.95);
                        layers.Insert((i - 1), new Layer(new Z(), li * 0.05));
                        Console.WriteLine("update : new layer was added : " + layers[i]);// if no ozone was found, insert above the oxygen 
                    }
                }
                else if (layers[i].getGas().isC()) // when seeing CD
                {
                    for (int z = i; z >= 0; z--)
                    {
                        if (layers[z].getGas().isX())
                        {
                            theresX = true;
                            double li = layers[i].getThickness();
                            double lz = layers[z].getThickness();
                            layers[i].setThickness(li * 0.95);
                            layers[z].setThickness((li * 0.05) + lz);
                        }

                    }
                    if (!theresX)
                    {
                        double li = layers[i].getThickness();
                        layers[i].setThickness(li * 0.95);
                        layers.Insert(i, new Layer(new Z(), li * 0.05)); // if no ozone was found, insert above the oxygen 
                        Console.WriteLine("update : new layer was added : " + layers[i]);
                    }
                }
            }
           
            return layers;
            
        }

    }

    public class O : atmospheres
    {
       public O(List<Layer> l) : base(l) { }

        public override List<Layer> affect()
        {
            bool theresX = false;
            bool theresC = false;
            for (int i = layers.Count - 1; i >= 0; i--)
            {
                if (layers[i].getGas().isZ()) // when seeing OX
                {
                    for (int z = i; z >= 0; z--)
                    {
                        if (layers[z].getGas().isX())
                        {
                            theresX = true;
                            double li = layers[i].getThickness();
                            double lz = layers[z].getThickness();
                            layers[i].setThickness(li * 0.95);
                            layers[z].setThickness((li * 0.05) + lz);
                        }

                    }
                    if (!theresX)
                    {
                        double li = layers[i].getThickness();
                        layers[i].setThickness(li * 0.95);
                        Layer nLayer = new Layer();
                        nLayer.setGas(new X()); nLayer.setThickness(li * 0.05);
                        layers.Insert(i, nLayer);
                        Console.WriteLine(" update : new layer was added : " + layers[i]);
                        // to avoid the exeption
                        // if no oxygen was found, insert above the ozone 
                    }
                }
                else if (layers[i].getGas().isX()) // when seeing ox to turn it to carbon-dioxide
                {
                    for (int z = i; z >= 0; z--)
                    {
                        if (layers[z].getGas().isC())
                        {
                            theresC = true;
                            double li = layers[i].getThickness();
                            double lz = layers[z].getThickness();
                            layers[i].setThickness(li * 0.9);
                            layers[z].setThickness((li * 0.1) + lz);
                        }

                    }
                    if (!theresC)
                    {
                        double li = layers[i].getThickness();
                        layers[i].setThickness(li * 0.9);
                        Layer nlayers = new Layer();
                        nlayers.setGas(new Z());
                        nlayers.setThickness(li * 0.1);
                        layers.Insert((i),nlayers);
                        Console.WriteLine("update : new layer was added : " + layers[i]);// if no ox was found, insert above the cd 
                    }
                }
            }
           
            return layers;
        }
     }

    }
