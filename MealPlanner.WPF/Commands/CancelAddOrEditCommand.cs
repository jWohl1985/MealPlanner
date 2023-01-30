using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take100.WPF.State.Navigators;
using Take100.WPF.ViewModels;

namespace Take100.WPF.Commands;

public class CancelAddOrEditCommand : CommandBase
{
    private readonly MainViewModel _mainViewModel;

    public CancelAddOrEditCommand(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    public override bool CanExecute(object? parameter)
    {
        return _mainViewModel.UserListViewModel.Users.Count != 0;
    }

    public override void Execute(object? parameter)
    {
        _mainViewModel.Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.MainView);
    }
}
