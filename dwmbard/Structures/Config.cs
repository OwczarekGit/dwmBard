using System;
using System.Collections.Generic;
using System.IO;
using dwmBard;

namespace wmExtender.Structures
{
    public class Config
    {
        public string configFile { get; private set; }
        private List<PropertyValue> values;
        
        public Config(string path)
        {
            configFile = path;
            values = new List<PropertyValue>();
            var exist = File.Exists(configFile);

            if (!exist)
                createDefaultConfig();
            else
                parseConfig();
        }

        private void createDefaultConfig()
        {
            Directory.CreateDirectory(Program.CONFIG_DIRECTORY_PATH);
            File.Create(configFile);
            Console.WriteLine($"Created default config in: {configFile}");
        }

        private void parseConfig()
        {
            StreamReader reader = new StreamReader(configFile);

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                values.Add(new PropertyValue(line.Trim()));
            }
        }

        public string? getValue(string property)
        {
            foreach (var configEntry in values)
            {
                if (configEntry.property == property)
                    return configEntry.value;
            }

            return null;
        }

        public string? getConfigValue(string property)
        {
            string result;
            if ((result = getValue(property)) != null)
                return result;

            return null;
        }
    }
}