using NModbus.Tools.Interfaces;

namespace NModbus.Tools.Base
{
    public abstract class ToolFactory : IToolFactory
    {
        protected ToolFactory(string name, string description, string imageSource = null)
        {
            Name = name;
            Description = description;
            ImageSource = imageSource;
        }

        public abstract ITool Create(IToolCreationContext context);

        public string Name { get; }

        public string Description { get; }

        public virtual string ImageSource { get; }

    }
}