using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using NModbus.Tools.FrameParser.Model;

namespace NModbus.Tools.FrameParser.ViewModel
{
    public class FrameParserViewModel : ViewModelBase
    {
        private string _frame;
        private FunctionServiceResult _result;

        public FrameParserViewModel()
        {
            ParseCommand = new RelayCommand(Parse, CanParse);
            ParseClipboardCommand = new RelayCommand(ParseClipboard, CanParseClipboard);
        }

        public ICommand ParseCommand { get; }

        public ICommand ParseClipboardCommand { get; }

        private void Parse()
        {
            try
            {
                var commaSplit = Frame.Split(',');

                List<byte> bytes = new List<byte>(commaSplit.Length);

                foreach (var part in commaSplit)
                {
                    if (!byte.TryParse(part, out var temp))
                    {
                        throw new InvalidOperationException($"Unable to parse '{part}'");
                    }

                    bytes.Add(temp);
                }

                byte[] frame = bytes.ToArray();

                Result = FunctionServiceManager.Process(frame);
            }
            catch (Exception ex)
            {
                Result = new FunctionServiceResult(error: ex.Message);
            }
        }
      
        private bool CanParse()
        {
            return !string.IsNullOrWhiteSpace(Frame);
        }

        private void ParseClipboard()
        {
            Frame = Clipboard.GetText();

            Parse();
        }

        private bool CanParseClipboard()
        {
            return Clipboard.ContainsText();
        }

        public string Frame
        {
            get { return _frame; }
            set
            {
                _frame = value; 
                RaisePropertyChanged();
            }
        }

        public FunctionServiceResult Result
        {
            get { return _result; }
            private set
            {
                _result = value; 
                RaisePropertyChanged();
            }
        }
    }
}