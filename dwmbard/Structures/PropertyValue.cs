using System;
using System.Reflection;

namespace wmExtender.Structures
{
    public class PropertyValue
    {
        private const string SEPARATOR = "=";
        public string property;
        public string value;
        
        public PropertyValue(string line)
        {
            var splited = line.Split(SEPARATOR);

            try
            {
                property = splited[0];
                value = splited[1];
            }
            catch
            {
                property = "";
                value = "";
                Console.WriteLine($"Invalid format in PropertyValue: [{line}]");
            }
        }

        public string toString()
        {
            if (property != null && value != null)
                return $"{property}: {value}";

            return "";
        }
    }
}