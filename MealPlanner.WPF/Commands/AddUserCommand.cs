using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealPlanner.Domain.Models;
using MealPlanner.Domain.Services;
using MealPlanner.WPF.State.Navigators;
using MealPlanner.WPF.ViewModels;

namespace MealPlanner.WPF.Commands;

public class AddUserCommand : CommandBase
{
    private readonly MainViewModel _mainViewModel;
    
    public AddUserCommand(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    public override void Execute(object? parameter)
    {
        _mainViewModel.Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.CreateOrUpdateUserView);
    }
}
