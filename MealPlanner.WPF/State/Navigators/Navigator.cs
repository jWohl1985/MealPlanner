using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MealPlanner.Domain.Services;
using MealPlanner.WPF.Commands;
using MealPlanner.WPF.ViewModels;

namespace MealPlanner.WPF.State.Navigators;

public class Navigator : ViewModelBase, INavigator
{
    private readonly MainViewModel _mainViewModel;

    private ViewModelBase _currentViewModel = null!;
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }

    public ICommand UpdateCurrentViewModelCommand => new UpdateCurrentViewModelCommand(_mainViewModel);

    public Navigator(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

}
