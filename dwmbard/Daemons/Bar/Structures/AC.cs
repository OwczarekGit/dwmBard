using dwmBard.Helpers;

namespace dwmBard.Handlers
{
    public class AC
    {
        private string acDirectory;

        public AC(string name)
        {
            acDirectory = name;
        }

        public bool isPlugged()
        {
            string tmp = CommandRunner.getStringFromFile($"{acDirectory}/online");

            return tmp.Contains("1");
        }
    }
}