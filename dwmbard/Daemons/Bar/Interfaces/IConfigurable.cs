using System.Reflection;

namespace dwmBard.Interfaces
{
    public interface IConfigurable
    {
        // This will be abstract and each handler that is IConfigurable
        // will have to implement what can be configured.
        public abstract void configure();

        
    }
}