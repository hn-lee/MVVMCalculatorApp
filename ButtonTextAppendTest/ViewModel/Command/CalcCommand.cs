using System;
using System.Windows.Input;

namespace ButtonTextAppendTest.ViewModel.Command
{
    class Append : ICommand
    {
        private MainViewModel c;
        public event EventHandler CanExecuteChanged;

        public Append(MainViewModel c)
        {
            this.c = c;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            c.InputString += parameter;
        }
    }

    class Clear : ICommand
    {
        private MainViewModel c;
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public Clear(MainViewModel c)
        {
            this.c = c;
        }

        public bool CanExecute(object parameter)
        {
            return c.DisplayText.Length > 0;
        }

        public void Execute(object parameter)
        {
            c.InputString = c.DisplayText = "";
        }
    }

    class Operator : ICommand
    {
        private MainViewModel c;
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public Operator(MainViewModel c)
        {
            this.c = c;
        }

        public bool CanExecute(object parameter)
        {
            return 0 < c.InputString.Length;
        }

        public void Execute(object parameter)
        {
            string op = parameter.ToString();
            double LeftOperand;

            if (double.TryParse(c.InputString, out LeftOperand))
            {
                if (c.LeftOperand != null)
                {
                    LeftOperand = Calculate.calculate(op, (double)c.LeftOperand, LeftOperand);
                    c.DisplayText = LeftOperand.ToString();
                }
                c.LeftOperand = LeftOperand;
                c.op = op;
                c.InputString = "";
            }
        }
    }
    
    class Calculate : ICommand
    {
        private MainViewModel c;
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public Calculate(MainViewModel c)
        {
            this.c = c;
        }

        public bool CanExecute(object parameter)
        {
            double RightOperand;
            return c.LeftOperand != null && double.TryParse(c.InputString, out RightOperand);
        }

        public void Execute(object parameter)
        {
            double RightOperand = double.Parse(c.InputString);
            c.InputString = calculate(c.op, (double)c.LeftOperand, RightOperand).ToString();
            c.LeftOperand = null;
        }

        public static double calculate(string op, double leftOperand, double rightOperand)
        {
            switch (op)
            {
                case "+": return leftOperand + rightOperand;
                case "-": return leftOperand - rightOperand;
                case "*": return leftOperand * rightOperand;
                case "/": return leftOperand / rightOperand;
            }

            return 0;
        }
    }
}