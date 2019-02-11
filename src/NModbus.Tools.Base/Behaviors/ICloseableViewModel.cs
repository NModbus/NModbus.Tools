using System;

namespace NModbus.Tools.Base.Behaviors
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICloseableViewModel
    {
        /// <summary>
        /// Fired when the view model requests that its container be closed.
        /// </summary>
        event EventHandler<CloseEventArgs> Close;

        /// <summary>
        /// Returns true if the container can be closed, false otherwise.
        /// </summary>
        /// <returns></returns>
        bool CanClose();

        /// <summary>
        /// Called when the view has been closed.
        /// </summary>
        void Closed();
    }

    /// <summary>
    /// 
    /// </summary>
    public class CloseEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dialogResult"></param>
        public CloseEventArgs(bool? dialogResult)
        {
            this.DialogResult = dialogResult;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool? DialogResult { get; set; }
    }
}
