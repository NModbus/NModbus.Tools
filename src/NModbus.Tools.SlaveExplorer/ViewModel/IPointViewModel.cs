using System.ComponentModel;

namespace NModbus.Tools.SlaveExplorer.ViewModel
{
    public interface IPointViewModel<TPointValue> : IPointViewModel
    {
        /// <summary>
        /// Setting this will mark the item as dirty.
        /// </summary>
        TPointValue Value { get; set; }

        /// <summary>
        /// Set the value without marking the item as dirty.
        /// </summary>
        /// <param name="value"></param>
        void SetValue(TPointValue value);

        /// <summary>
        /// Initialize the view model.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="value"></param>
        void Initialize(ushort address, TPointValue value);
    }

    public interface IPointViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the address of the point.
        /// </summary>
        ushort Address { get; }

        /// <summary>
        /// Gets or sets whether the point is considered dirty.
        /// </summary>
        bool IsDirty { get; set; }
    }
}
