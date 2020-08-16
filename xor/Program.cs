using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xor
{
    class Program
    {
        public static string newBitString;
        static void Main(string[] args)
        {
            Program prg = new Program();
            prg.DoStuff();
            Console.WriteLine(Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("New key = " + newBitString);
            Console.WriteLine(Environment.NewLine);
        }

        private void DoStuff()
        {
            Console.WriteLine("Enter tap sequence: ");
            string t = Console.ReadLine();
            Console.WriteLine("Enter initial register: ");
            string r = Console.ReadLine();
            Console.WriteLine("Enter number of levels: ");
            int layers = Convert.ToInt32(Console.ReadLine());
            string newBit;

            char[] tapBitArray = t.ToCharArray();
            //char[] regBitArray = r.ToCharArray();

            List<string> registerArray = new List<string>();
            string newBitComp = string.Empty;
            string newBitCompRaw = string.Empty;
            Console.WriteLine("Tap: " + t);
            Console.Write(Environment.NewLine);
            Console.Write(Environment.NewLine);
            Console.WriteLine("Register\tKi\tNewBitComputation\tOutput\tNew R");
            Console.WriteLine("______________________________________________________________");

            for (int i = 0; i < layers; i++)
            {
                for (int j = 0; j < tapBitArray.Length; j++)
                {
                    if (j == 0)
                    {
                        newBitComp = r[j].ToString() + t[j].ToString() + "(+)";
                        newBitCompRaw = r[j].ToString() + t[j].ToString();
                    }
                    else
                    {
                        newBitComp = newBitComp + r[j].ToString() + t[j].ToString() + "(+)";
                        newBitCompRaw = newBitCompRaw + r[j].ToString() + t[j].ToString();
                    }                    
                }

                registerArray.Add(newBitComp.Substring(0, newBitComp.Length - 3));
                                
                newBit = NewBitComputation(newBitCompRaw);
                newBitString += newBit;
                string popRegister = r.Substring(0, 3);
                string newRegister = newBit + popRegister;                
                Console.WriteLine(r + "\t\t" + r.Substring(3,1) + "\t" + newBitComp.Substring(0, newBitComp.Length - 3) + "\t" + newBit + "\t" + newRegister);
                Console.WriteLine("______________________________________________________________");
                r = newRegister;
            }
        }

        static IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        private string NewBitComputation(string bitSequence)
        {
            string bitSequence2 = string.Empty;

            IEnumerable<string> getValuePairs = Split(bitSequence, 2);

            foreach (var pair in getValuePairs)
            {
                char[] pair1Char = pair.ToCharArray();
                int comp1 = Convert.ToInt32(pair1Char[0].ToString());
                int comp2 = Convert.ToInt32(pair1Char[1].ToString());
                int andResult = comp1 & comp2;
                string newBitString = Convert.ToString(andResult, 2);
                bitSequence2 = bitSequence2 + newBitString;
            }

            int[] fc1 = bitSequence2.Select(c => c - '0').ToArray();
            int xorme = (fc1[0] ^ fc1[1]) ^ (fc1[2] ^ fc1[3]);

            return xorme.ToString();

        }
    }
}
