using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace dwmBard
{
    public class BoolArgument
    {
        public string propertyName = string.Empty;
        public bool propertyValue;
        public bool toggleable;
        public string description;
        
        public BoolArgument(string name, bool defaultValue, string description = "", bool toggleable = true)
        {
            propertyName = name;
            propertyValue = defaultValue;
            this.toggleable = toggleable;
            this.description = description;
        }

        public bool isToggleable()
        {
            return toggleable;
        }
        
        public void toggle()
        {
            if (isToggleable())
                propertyValue = !propertyValue;
        }

        public string getUsage()
        {
            return $"{propertyName} -\t{description}.";
        }
    }
}