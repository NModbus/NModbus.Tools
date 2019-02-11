using System;

namespace NModbus.Tools.Base
{
    public static class MessageBoxServiceExtensions
    {
        public static void Show(this IMessageBoxService messageBoxService, Exception ex, string title)
        {
            messageBoxService.Show(ex.ToString(), title);
        }
    }
}
