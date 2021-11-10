using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ButtonTextAppendTest.ViewModel.Command;

namespace ButtonTextAppendTest.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public string op { get; set; }
        public double? LeftOperand { get; set; } // nullable

        string inputString = "";
        string displayText = "";

        public string InputString
        {
            internal set
            {
                if (inputString != value)
                {
                    inputString = value;
                    OnPropertyChanged("InputString");
                    if (value != "")
                    {
                        DisplayText = value;
                    }
                }
            }
            get { return inputString; }
        }
        public string DisplayText
        {
            internal set
            {
                if (displayText != value)
                {
                    displayText = value;
                    OnPropertyChanged("DisplayText");
                }
            }
            get { return displayText; }
        }

        public ICommand Append { protected set; get; }
        public ICommand Operator { protected set; get; }
        public ICommand Clear { protected set; get; }
        public ICommand Calculate { protected set; get; }
        public MainViewModel()
        {
            this.Append = new Append(this);
            this.Operator = new Operator(this);
            this.Clear = new Clear(this);
            this.Calculate = new Calculate(this);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
