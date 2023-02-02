using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MealPlanner.Domain.Models;
using MealPlanner.Domain.Services;
using MealPlanner.WPF.State.Navigators;
using MealPlanner.WPF.ViewModels;

namespace MealPlanner.WPF.Commands;

public class UpdateCurrentViewModelCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    private readonly MainViewModel _mainViewModel;
    private readonly INavigator _navigator;

    public UpdateCurrentViewModelCommand(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        _navigator = mainViewModel.Navigator;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        CanExecuteChanged?.Invoke(this, new EventArgs());

        ViewType viewType = (ViewType)parameter!;

        _navigator.CurrentViewModel = viewType switch
        {
            ViewType.MainView => _mainViewModel,
            ViewType.CreateOrUpdateUserView => new CreateOrUpdateUserViewModel(_mainViewModel),
            ViewType.CreateOrUpdateFoodView => new CreateOrUpdateFoodViewModel(_mainViewModel),
            ViewType.WeightLogView => new WeightLogViewModel(_mainViewModel),
            _ => throw new Exception("ViewType not implemented!")
        };
    }
}
