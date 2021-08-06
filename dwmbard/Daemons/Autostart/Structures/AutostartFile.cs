using System;
using System.Collections.Generic;
using System.IO;

namespace dwmBard.Daemons
{
    public class AutostartFile
    {
        public List<AutostartEntry> autostartEntries { get; private set; }
        public string autostartFile { get; private set; }

        public AutostartFile(string path)
        {
            autostartFile = path;
            autostartEntries = new List<AutostartEntry>();

            var exists = File.Exists(autostartFile);

            if (!exists)
                createDefaultAutostartFile();
            else
                parseFile();

        }

        private void parseFile()
        {
            StreamReader reader = new StreamReader(autostartFile);

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                try
                {
                    var lineSplit = line.Trim().Split(';');

                    string tmpProcName = null;
                    string tmpName = lineSplit[0];
                    string tmpKeepRunning = lineSplit[1];

                    if (lineSplit.Length > 2)
                        tmpProcName = lineSplit[2];

                    var tmpEntry = new AutostartEntry(tmpName, bool.Parse(tmpKeepRunning), tmpProcName);
                    Console.WriteLine(tmpEntry.toString());
                    autostartEntries.Add(tmpEntry);
                }
                catch{ Console.WriteLine($"Syntax error in autostart entry: {line}"); }
            }
        }
        
        private void createDefaultAutostartFile()
        {
            Directory.CreateDirectory(Autostart.AUTOSTART_DIRECTORY_PATH);
            File.Create(autostartFile);
            Console.WriteLine($"Created default autostart file in: {autostartFile}");
        }
    }
}