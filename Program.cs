using System.Runtime.InteropServices;
using TextFile;
namespace atmos
{
    internal class Program
    {
       public static void printL(List<Layer> layers)
        {  
            for (int i = 0; i < layers.Count; i++)
            {
                Console.WriteLine("layer " + (i+1) + " : (" + layers[i].ToString()+")");
                
            }
            
       }
        static void Main(string[] args)
        {
                // setting the layers
                TextFileReader file = new TextFileReader("originaltest.txt");
               
                int count = Int32.Parse(file.ReadLine());
                 if (count < 3) { throw new Exception("NOT_ENOUGH_LAYERS"); }

                

                List<Layer> layers = new List<Layer>(count);

                for (int i = 0; i < count; i++)
                {
                    string[] line = file.ReadLine().Split(" ");
                    if (line[0] == "C")
                    {
                        layers.Add(new Layer(new C(), Double.Parse(line[1])));
                    }
                    else if (line[0] == "X")
                    {
                        layers.Add(new Layer(new X(), Double.Parse(line[1])));
                    }
                    else if (line[0] == "Z")
                    {
                        layers.Add(new Layer(new Z(), Double.Parse(line[1])));
                    }

                }

                // printing before the changes
                Console.WriteLine("the original layers:");
                printL(layers);
                Console.WriteLine("/////////////////////////////////////////////////////");
                

                /* How this works:
                 1. the atmosphere changes the layers
                 2. layers ascend in case of sth less than 0.15
                 3. in case a layer is still less than 0.15, it perishes
                 */

                //the changing happening
                char[] atmosVars = file.ReadLine().ToCharArray();
                bool allExists = true;

            while (allExists)
            {
                for (int i = 0; i < atmosVars.Length; i++)
                {
                    // if Other
                    if (atmosVars[i] == 'O')
                    {
                        Layer l = new Layer();
                        O Other = new O(layers);
                        Console.WriteLine("\nAfter Other atmosphere:");
                        Other.affect();
                        layers = Other.ascendANDperish(out bool pr, out l);
                        printL(layers);
                        allExists = allExists && Other.allStillExists();
                        if (!Other.allStillExists()) { Console.WriteLine("A certain gas has perished completely"); break; }
                    }
                    // if Sunshine
                    else if (atmosVars[i] == 'S')
                    {

                        Layer l = new Layer();
                        S Sunshine = new S(layers);
                        Console.WriteLine("\nAfter SunShine atmosphere:");
                        Sunshine.affect();
                        layers = Sunshine.ascendANDperish(out bool pr, out l);
                        
                        printL(layers);
                        
                        allExists = allExists && Sunshine.allStillExists();
                        if (!Sunshine.allStillExists()) { Console.WriteLine("some gas has perished completely finallyyyyy"); break; }

                    }
                    // if ThunderStorm
                    else if (atmosVars[i] == 'T')
                    {
                        Layer l = new Layer();
                        T ThunderStrom = new T(layers);
                        Console.WriteLine("\nAfter ThunderStorm atmosphere:");
                        ThunderStrom.affect();
                        layers = ThunderStrom.ascendANDperish(out bool pr, out l);
                       
                        printL(layers);
                       
                        allExists = allExists && ThunderStrom.allStillExists();
                        if (!ThunderStrom.allStillExists()) { Console.WriteLine("A certain gas has perished completely"); break; }
                    }
                    //
                    else { throw new Exception("UNEXPECTED!"); }
                }
            }
            

        }
    }
}
