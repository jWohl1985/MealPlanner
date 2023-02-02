
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using MealPlanner.Domain.Models;
using MealPlanner.Domain.Services;
using MealPlanner.EntityFramework;
using MealPlanner.WPF.Commands;
using MealPlanner.WPF.State.Navigators;

namespace MealPlanner.WPF.ViewModels;

public class MainViewModel : ViewModelBase
{
    public INavigator Navigator { get; private set; }
    public UserListViewModel UserListViewModel { get; private set; }
    public FoodDatabaseViewModel FoodDatabaseViewModel { get; private set; }
    public DayViewModel DayViewModel { get; private set; }
    public IDietDataService DietDataService { get; private set; }
    
    public MainViewModel(IDietDataService dietDataService)
    {
        DietDataService = dietDataService;

        Navigator = new Navigator(this)
        {
            CurrentViewModel = this
        };

        UserListViewModel = new UserListViewModel(this);
        UserListViewModel.RefreshUserList();
        FoodDatabaseViewModel = new FoodDatabaseViewModel(this);
        DayViewModel = new DayViewModel(this);
    }
}
