using NModbus.Tools.Base;
using NModbus.Tools.FrameParser.View;
using NModbus.Tools.FrameParser.ViewModel;
using NModbus.Tools.Interfaces;

namespace NModbus.Tools.FrameParser
{
    public class FrameParserToolFactory : ToolFactory
    {
        public FrameParserToolFactory() 
            : base("Frame Parser", "Description")
        {
        }

        public override ITool Create(IToolCreationContext context)
        {
            var viewModel = new FrameParserViewModel();

            var view = new FrameParserView
            {
                DataContext = viewModel
            };

            return new Tool(view, context.Factory);
        }
    }
}