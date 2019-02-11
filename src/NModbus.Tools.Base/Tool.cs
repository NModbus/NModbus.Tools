using System;
using System.Windows;
using NModbus.Tools.Interfaces;

namespace NModbus.Tools.Base
{
    public class Tool : ITool
    {
        public Tool(FrameworkElement view, IToolFactory factory)
        {
            Factory = factory ?? throw new ArgumentNullException(nameof(factory));
            View = view ?? throw new ArgumentNullException(nameof(view));
        }

        public FrameworkElement View { get; }

        public IToolFactory Factory { get; }
    }
}