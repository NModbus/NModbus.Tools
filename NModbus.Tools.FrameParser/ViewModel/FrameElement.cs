namespace NModbus.Tools.FrameParser.ViewModel
{
    public class FrameElement
    {
        public string Name { get; }

        public string Value { get; }

        public string Description { get; }

        public FrameElement(string name, string value, string description = null)
        {
            Name = name;
            Value = value;
            Description = description;
        }
    }
}