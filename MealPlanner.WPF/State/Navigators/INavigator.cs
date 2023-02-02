using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MealPlanner.WPF.ViewModels;

namespace MealPlanner.WPF.State.Navigators;

public enum ViewType
{
    MainView,
    CreateOrUpdateFoodView,
    CreateOrUpdateUserView,
    WeightLogView,
}

public interface INavigator
{
    ViewModelBase CurrentViewModel { get; set; }
    ICommand UpdateCurrentViewModelCommand { get; }
}
