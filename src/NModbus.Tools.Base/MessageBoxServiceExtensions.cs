using System;

namespace NModbus.Tools.Base
{
    public static class MessageBoxServiceExtensions
    {
        public static void Display(this IMessageBoxService messageBoxService, Exception ex, string title = "Error")
        {
            messageBoxService.Show(ex.ToString(), title);
        }
    }
}
