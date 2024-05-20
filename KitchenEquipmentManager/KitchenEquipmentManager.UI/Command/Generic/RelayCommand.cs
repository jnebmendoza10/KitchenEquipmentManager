using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace KitchenEquipmentManager.UI.Command.Generic
{
    public class RelayCommand<T> : ICommand
    {
        //
        // Summary:
        //     The System.Action to invoke when Microsoft.Toolkit.Mvvm.Input.RelayCommand`1.Execute(`0)
        //     is used.
        private readonly Action<T> execute;

        //
        // Summary:
        //     The optional action to invoke when Microsoft.Toolkit.Mvvm.Input.RelayCommand`1.CanExecute(`0)
        //     is used.
        private readonly Predicate<T> canExecute;

        public event EventHandler CanExecuteChanged;

        //
        // Summary:
        //     Initializes a new instance of the Microsoft.Toolkit.Mvvm.Input.RelayCommand`1
        //     class that can always execute.
        //
        // Parameters:
        //   execute:
        //     The execution logic.
        //
        // Remarks:
        //     Due to the fact that the System.Windows.Input.ICommand interface exposes methods
        //     that accept a nullable System.Object parameter, it is recommended that if T is
        //     a reference type, you should always declare it as nullable, and to always perform
        //     checks within execute.
        public RelayCommand(Action<T> execute)
        {
            this.execute = execute;
        }

        //
        // Summary:
        //     Initializes a new instance of the Microsoft.Toolkit.Mvvm.Input.RelayCommand`1
        //     class.
        //
        // Parameters:
        //   execute:
        //     The execution logic.
        //
        //   canExecute:
        //     The execution status logic.
        //
        // Remarks:
        //     See notes in Microsoft.Toolkit.Mvvm.Input.RelayCommand`1.#ctor(System.Action{`0}).
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        //
        // Summary:
        //     Notifies that the System.Windows.Input.ICommand.CanExecute(System.Object) property
        //     has changed.
        public void NotifyCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CanExecute(T parameter)
        {
            return canExecute?.Invoke(parameter) ?? true;
        }

        public bool CanExecute(object parameter)
        {
            if (default(T) != null && parameter == null)
            {
                return false;
            }

            return CanExecute((T)parameter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Execute(T parameter)
        {
            if (CanExecute(parameter))
            {
                execute(parameter);
            }
        }

        public void Execute(object parameter)
        {
            Execute((T)parameter);
        }
    }
}
