namespace NModbus.Tools.Interfaces
{
    public interface IToolFactory
    {
        string Name { get; }

        string Description { get; }

        string ImageSource { get; }

        ITool Create(IToolCreationContext context);
    }
}