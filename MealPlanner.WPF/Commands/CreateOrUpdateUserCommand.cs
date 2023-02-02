using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealPlanner.Domain.Models;
using MealPlanner.WPF.State.Navigators;
using MealPlanner.WPF.ViewModels;

namespace MealPlanner.WPF.Commands;

public class CreateOrUpdateUserCommand : CommandBase
{
    private readonly MainViewModel _mainViewModel;
    private readonly UserListViewModel _userListViewModel;
    private readonly CreateOrUpdateUserViewModel _createOrUpdateUserViewModel;

    public CreateOrUpdateUserCommand(MainViewModel mainViewModel, CreateOrUpdateUserViewModel createOrUpdateUserViewModel)
    {
        _mainViewModel = mainViewModel;
        _userListViewModel = _mainViewModel.UserListViewModel;
        _createOrUpdateUserViewModel = createOrUpdateUserViewModel;

        _createOrUpdateUserViewModel.PropertyChanged += OnViewModelChanged;
    }

    public override bool CanExecute(object? parameter)
    {
        return !_createOrUpdateUserViewModel.HasErrors;
    }

    public override async void Execute(object? parameter)
    {
        DietUser dietUser = (DietUser)parameter!;

        DietUser? existingUser = await _mainViewModel.DietDataService.DietUserRepository.GetByIdAsync(dietUser.Id);

        if (existingUser is null)
        {
            await CreateNewUser(dietUser);
        }
        else
        {
            await UpdateExistingUser(dietUser);
        }
        
        _mainViewModel.Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.MainView);
        _userListViewModel.RefreshUserList();
        _userListViewModel.SelectedUserViewModel = _userListViewModel.Users.Where(u => u.DietUser.Id == dietUser.Id).First();
    }

    private void OnViewModelChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_createOrUpdateUserViewModel.NameErrors)
            || e.PropertyName == nameof(_createOrUpdateUserViewModel.AgeErrors)
            || e.PropertyName == nameof(_createOrUpdateUserViewModel.WeightErrors)
            || e.PropertyName == nameof(_createOrUpdateUserViewModel.NeckErrors)
            || e.PropertyName == nameof(_createOrUpdateUserViewModel.WaistErrors)
            || e.PropertyName == nameof(_createOrUpdateUserViewModel.HipErrors))
            OnCanExecuteChanged();
    }

    private async Task CreateNewUser(DietUser dietUser)
    {
        await _mainViewModel.DietDataService.DietUserRepository.CreateAsync(dietUser);

        WeightLogEntry weightLogEntry = new WeightLogEntry()
        {
            DietUserId = dietUser.Id,
            Date = DateTime.Now,
            WeightInLbs = dietUser.WeightInLbs,
        };

        await _mainViewModel.DietDataService.WeightLogRepository.CreateAsync(weightLogEntry);
    }

    private async Task UpdateExistingUser(DietUser dietUser)
    {
        await _mainViewModel.DietDataService.DietUserRepository.UpdateAsync(dietUser.Id, dietUser);

        WeightLogEntry weightLogEntry = new WeightLogEntry()
        {
            DietUserId = dietUser.Id,
            Date = DateTime.Now,
            WeightInLbs = dietUser.WeightInLbs,
        };

        await _mainViewModel.DietDataService.WeightLogRepository.CreateAsync(weightLogEntry);
    }
}
