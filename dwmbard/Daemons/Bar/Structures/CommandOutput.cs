namespace wmExtender.Daemons.Bar.Structures
{
    public class CommandOutput
    {
        public string stdOut { get; private set; }
        public string stdErr { get; private set; }
        public int exitCode  { get; private set; }

        public CommandOutput(string output, string error, int exitCode)
        {
            stdOut = output.Trim();
            stdErr = error.Trim();
            this.exitCode = exitCode;
        }

        public string toString()
        {
            return $"[{stdOut},{stdErr},{exitCode}]";
        }

        public bool exitedAbnormally()
        {
            return exitCode != 0;
        }
    }
}