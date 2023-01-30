using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Take100.WPF.ViewModels;

namespace Take100.WPF.State.Navigators;

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
