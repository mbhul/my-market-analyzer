using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyMarketAnalyzer
{
    class CSVParser
    {
        public String CSVtoString(String fullfilepath)
        {
            return "";
        }

        public static List<String[]> CSVtoListArray(String fullfilepath, int maxRecords)
        {
            List<String[]> retArray = null;
            var reader = new StreamReader(File.OpenRead(fullfilepath));
            String[] values = null;
            int maxsize = 1;

            retArray = new List<String[]>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                values = line.Split(',');
                retArray.Add(values);
                
                if(values.Length > maxsize)
                {
                    maxsize = values.Length;
                }
            }
            reader.Close();

            //Ensure each array in the list has the same number of elements, to make column selection possible
            for(int i = 0; i < retArray.Count; i++)
            {
                if(retArray[i].Length < maxsize)
                {
                    values = retArray[i];
                    Array.Resize(ref values, maxsize);
                    retArray[i] = values;
                }
            }

            return retArray;
        }
    }
}
