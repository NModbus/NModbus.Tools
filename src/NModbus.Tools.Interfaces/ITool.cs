using System.Windows;

namespace NModbus.Tools.Interfaces
{
    /// <summary>
    /// Represents an instance of a tool.
    /// </summary>
    public interface ITool
    {
        FrameworkElement View { get; }

        /// <summary>
        /// Gets the factory that was used to create this tool.
        /// </summary>
        IToolFactory Factory { get; }
    }
}