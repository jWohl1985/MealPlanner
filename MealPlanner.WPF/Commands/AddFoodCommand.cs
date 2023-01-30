using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take100.Domain.Models;
using Take100.Domain.Services;
using Take100.WPF.State.Navigators;
using Take100.WPF.ViewModels;

namespace Take100.WPF.Commands;

public class AddFoodCommand : CommandBase
{
    private readonly MainViewModel _mainViewModel;

    public AddFoodCommand(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    public override void Execute(object? parameter)
    {
        _mainViewModel.Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.CreateOrUpdateFoodView);
    }
}
