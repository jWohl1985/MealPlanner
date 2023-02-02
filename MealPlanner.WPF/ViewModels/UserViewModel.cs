using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealPlanner.Domain.Models;

namespace MealPlanner.WPF.ViewModels;

public class UserViewModel : ViewModelBase
{
    private DietUser _dietUser;

    public DietUser DietUser 
    {
        get => _dietUser;
        set
        {
            _dietUser = value;
            OnPropertyChanged(nameof(DietUser));
            OnPropertyChanged(nameof(Name));
        }
    }

    public string Name => DietUser.Name;

    public UserViewModel(DietUser dietUser)
    {
        _dietUser = dietUser;
    }
}
