using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CodeTest.Nasa.Models;

namespace CodeTest.Nasa.Helpers
{
    public class DateHelper
    {
        public List<ParsedDate> ValidateDatesFromFile(string filePath)
        {
            var dates = ReadDates(filePath);
            List<ParsedDate> list = new List<ParsedDate>();
            foreach (var date in dates)
            {
                var parsedDate = ParseDate(date);
                if (parsedDate != string.Empty)
                {
                    list.Add(new ParsedDate { ErrorMessage = string.Empty, HasError = false, StringDate = parsedDate });
                }
                else
                {
                    list.Add(new ParsedDate { ErrorMessage = $"Unable to parse {date} from dates.txt", HasError = true, StringDate = string.Empty });
                }
            }

            return list;
        }
        private string ParseDate(string date)
        {
            DateTime dDate;
            if (DateTime.TryParse(date, out dDate))
            {
                return $"{dDate:yyyy-MM-dd}";
            }
            return string.Empty;
        }
        private List<string> ReadDates(string filePath)
        {
            var lines = new List<string>();
            using (TextReader r = new StreamReader(filePath))
            {
                string line = null;
                while ((line = r.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines;
        }
    }
}
