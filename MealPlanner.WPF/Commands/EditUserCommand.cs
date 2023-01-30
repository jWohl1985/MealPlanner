using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Take100.Domain.Models;
using Take100.WPF.State.Navigators;
using Take100.WPF.ViewModels;

namespace Take100.WPF.Commands;

public class EditUserCommand : CommandBase
{
    private readonly MainViewModel _mainViewModel;

    public EditUserCommand(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    public override bool CanExecute(object? parameter)
    {
        return _mainViewModel.UserListViewModel.SelectedUser is not null;
    }

    public override void Execute(object? parameter)
    {
        UserViewModel userViewModel = (UserViewModel)parameter!;
        DietUser dietUser = userViewModel.DietUser;

        _mainViewModel.Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.CreateOrUpdateUserView);
        CreateOrUpdateUserViewModel viewModel = (CreateOrUpdateUserViewModel)_mainViewModel.Navigator.CurrentViewModel;

        DietUser tempCopyOfUser = new()
        {
            Id = dietUser.Id,
            Name = dietUser.Name,
            Age = dietUser.Age,
            IsMale = dietUser.IsMale,
            WeightInLbs = dietUser.WeightInLbs,
            HeightFeetComponent = dietUser.HeightFeetComponent,
            HeightInchComponent = dietUser.HeightInchComponent,
            HoursExercisePerWeek = dietUser.HoursExercisePerWeek,
            Goal = dietUser.Goal,
            NeckCircumferenceInches = dietUser.NeckCircumferenceInches,
            WaistCircumferenceInches = dietUser.WaistCircumferenceInches,
            HipCircumferenceInches = dietUser.HipCircumferenceInches,
        };

        viewModel.DietUser = tempCopyOfUser;
    }
}
