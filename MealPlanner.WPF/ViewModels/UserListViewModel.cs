using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Take100.Domain.Models;
using Take100.Domain.Services;
using Take100.WPF.Commands;
using Take100.WPF.State.Navigators;

namespace Take100.WPF.ViewModels;

public class UserListViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;

    private UserViewModel _selectedUserViewModel = default!;
    public UserViewModel SelectedUserViewModel
    {
        get => _selectedUserViewModel;
        set
        {
            _selectedUserViewModel = value;
            OnPropertyChanged(nameof(SelectedUserViewModel));
            OnPropertyChanged(nameof(SelectedUser));
        }
    }

    public DietUser? SelectedUser => SelectedUserViewModel.DietUser;

    public ObservableCollection<UserViewModel> Users { get; set; }
    public ICommand AddUserCommand { get; set; }
    public ICommand EditUserCommand { get; set; }
    public ICommand DeleteUserCommand { get; set; }
    public INavigator Navigator => _mainViewModel.Navigator;

    public UserListViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        Users = new ObservableCollection<UserViewModel>();

        AddUserCommand = new AddUserCommand(_mainViewModel);
        EditUserCommand = new EditUserCommand(_mainViewModel);
        DeleteUserCommand = new DeleteUserCommand(_mainViewModel);
    }

    public void RefreshUserList()
    {
        Users.Clear();
        IEnumerable<DietUser> users = LoadUserListFromDatabase();

        foreach (DietUser user in users)
        {
            Users.Add(new UserViewModel(user));
        }

        if (Users.Count == 0)
        {
            SelectedUserViewModel = new UserViewModel(new DietUser());
            AddUserCommand.Execute(ViewType.CreateOrUpdateUserView);
        }
        else
        {
            SelectedUserViewModel = Users[0];
        }
            
    }

    private IEnumerable<DietUser> LoadUserListFromDatabase() => _mainViewModel.DietDataService.GetAllDietUsers();

}
