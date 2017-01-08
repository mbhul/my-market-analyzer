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

            retArray = new List<String[]>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                values = line.Split(',');
                retArray.Add(values);
            }

            reader.Close();
            return retArray;
        }
    }
}
