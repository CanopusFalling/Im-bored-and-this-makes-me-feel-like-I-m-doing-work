using System;

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Problem_A
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the lines till the end of file.
            String[] lines = ReadLines();

            // Testing output.
            /*
            foreach (String line in lines)
            {
                Console.WriteLine(line);
            }*/

            // Scan each line for hexadecimal numbers.
            List<HexNum> hexNumbers = new List<HexNum>();

            foreach (String line in lines)
            {
                hexNumbers.AddRange(GetHexNums(line));
            }

            foreach (HexNum num in hexNumbers)
            {
                Console.WriteLine(num);
            }
        }

        static String[] ReadLines(){
            List<String> result = new List<string>();

            // Loop until end of file is reached.
            String currentLine;
            while ((currentLine = Console.ReadLine()) != null) {
                result.Add(currentLine);
            }

            return result.ToArray();
        }

        static List<HexNum> GetHexNums(String input){
            List<HexNum> result = new List<HexNum>();
            //String lowerInput = input.ToLower();

            // Locate "0x" in the string.
            MatchCollection matches = Regex.Matches(
                input, "0x[0-9A-F]*", RegexOptions.IgnoreCase);

            foreach (Match m in matches){
                //Console.WriteLine("Found '{0}' at position {1}", m.Value, m.Index);
                result.Add(new HexNum(m.Value));
            }

            return result;
        }
    }

    class HexNum
    {
        long value;
        String hexString;
        public HexNum(String hex){
            this.hexString = hex;
            this.value = Convert.ToInt64(hex, 16);
        }

        public override String ToString(){
            return this.ToHex() + " " + this.ToDen();
        }

        public long ToDen(){
            return this.value;
        }

        public String ToHex(){
            return this.hexString;
        }
    }
}
