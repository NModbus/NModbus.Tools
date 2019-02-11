using System.Windows;

namespace NModbus.Tools.Base
{
    public class MessageBoxService : IMessageBoxService
    {
        public void Show(string caption, string title)
        {
            MessageBox.Show(caption, title);
        }
    }
}
