using NModbus.Tools.Interfaces;

namespace NModbus.Tools.Model
{
    public class ToolCreationContext : IToolCreationContext
    {
        public ToolCreationContext(IToolFactory factory)
        {
            Factory = factory;
        }

        public IToolFactory Factory { get; }
    }
}