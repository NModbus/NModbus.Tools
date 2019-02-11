using GalaSoft.MvvmLight;
using System;
using System.Globalization;

namespace NModbus.Tools.SlaveExplorer.ViewModel
{
    public class RegisterViewModel : ViewModelBase, IPointViewModel<ushort>
    {
        private ushort _value;
        private bool _isDirty;
        private ushort _address;

        public RegisterViewModel()
        {
        }

        public RegisterViewModel(ushort address)
        {
            Address = address;
        }

        public RegisterViewModel(ushort address, ushort value)
            : this(address)
        {
            _value = value;
        }

        public void SetValue(ushort value)
        {
            Value = value;
            IsDirty = false;
        }

        public void Initialize(ushort address, ushort value)
        {
            Address = address;
            Value = value;
            IsDirty = false;
        }

        public ushort Address
        {
            get { return _address; }
            set
            {
                _address = value;
                RaisePropertyChanged();
            }
        }

        public virtual ushort Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;

                    OnValueUpdated();

                    IsDirty = true;
                }
            }
        }

        protected void OnValueUpdated()
        {
            RaisePropertyChanged(() => Value);
            RaisePropertyChanged(() => MSB);
            RaisePropertyChanged(() => LSB);
            RaisePropertyChanged(() => Hex);
            RaisePropertyChanged(() => Binary);
            RaisePropertyChanged(() => Signed);
        }

        public byte MSB
        {
            get { return (byte)(Value >> 8); }
            set
            {
                ushort temp = value;

                temp <<= 8;

                temp += LSB;

                Value = temp;
            }
        }

        public byte LSB
        {
            get { return (byte)Value; }
            set { Value = (ushort)((ushort)value + (ushort)(Value & 0xff00)); }
        }

        public string Hex
        {
            get { return $"0x{Value:x4}"; }
            set
            {
                ushort converted;

                if (value != null)
                {
                    value = value.Replace("0x", "");
                }

                if (ushort.TryParse(value, NumberStyles.AllowHexSpecifier | NumberStyles.HexNumber, null, out converted))
                {
                    Value = converted;
                }
                else
                {
                    RaisePropertyChanged();
                }
            }
        }

        public string Binary
        {
            get { return Convert.ToString(Value, 2).PadLeft(16, '0').Insert(8, " "); }
            set
            {
                try
                {
                    if (value != null)
                        value = value.Replace(" ", "");

                    Value = Convert.ToUInt16(value, 2);
                }
                catch (Exception)
                {
                    RaisePropertyChanged();
                }

            }
        }

        public short Signed
        {
            get { return (short)Value; }
            set { Value = (ushort)value; }
        }

        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                if (_isDirty != value)
                {
                    _isDirty = value;
                    RaisePropertyChanged(() => IsDirty);
                }
            }
        }
    }
}
