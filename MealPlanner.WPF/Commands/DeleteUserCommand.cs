using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Take100.Domain.Models;
using Take100.Domain.Services;
using Take100.WPF.ViewModels;

namespace Take100.WPF.Commands;

public class DeleteUserCommand : CommandBase
{
    private readonly MainViewModel _mainViewModel;
    private readonly IRepository<DietUser> _dietUserRepository;

    private UserListViewModel UserListViewModel => _mainViewModel.UserListViewModel;

    public DeleteUserCommand(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        _dietUserRepository = mainViewModel.DietDataService.DietUserRepository;
    }

    public override bool CanExecute(object? parameter)
    {
        return true;
    }

    public override async void Execute(object? parameter)
    {
        UserViewModel userViewModel = (UserViewModel)parameter!;
        DietUser dietUser = userViewModel.DietUser;

        if (ConfirmDelete(dietUser))
        {
            UserListViewModel.Users.Remove(userViewModel);
            await _dietUserRepository.DeleteAsync(dietUser.Id);
            UserListViewModel.RefreshUserList();
        }
        else
        {
            return;
        }
    }

    private bool ConfirmDelete(DietUser dietUser)
    {
        string message = $"Do you really want to delete the user {dietUser.Name}? This can't be undone.";
        string title = "Confirm delete";

        MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNo);

        return result is MessageBoxResult.Yes;
    }
}
