using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Take100.Domain.Models;
using Take100.WPF.Commands;
using Take100.WPF.Helpers;
using Take100.WPF.State.Navigators;

namespace Take100.WPF.ViewModels;

public class CreateOrUpdateFoodViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;
    public INavigator Navigator => _mainViewModel.Navigator;
    public ICommand CreateOrUpdateFoodCommand { get; set; }
    public ICommand CancelAddOrEditCommand { get; set; }

    private Food _food;
    public Food Food
    {
        get => _food;
        set
        {
            _food = value;
            SetTextBoxValuesToFoodValues();
        }
    }

    private string _nameText = String.Empty;
    public string NameText 
    {
        get => _nameText;
        set
        {
            _nameText = value;
            NameErrors = FoodValidator.GetFoodNameErrors(value);
            OnPropertyChanged(nameof(NameErrors));

            if (NameErrors == String.Empty)
                _food.Name = value;
        }
    }

    private string _calorieText = String.Empty;
    public string CalorieText
    {
        get => _calorieText;
        set
        {
            _calorieText = value;
            CalorieErrors = FoodValidator.GetCalorieErrors(value);
            OnPropertyChanged(nameof(CalorieErrors));

            if (CalorieErrors == String.Empty)
                _food.Calories = float.Parse(value);
        }
    }

    private string _proteinText = String.Empty;
    public string ProteinText
    {
        get => _proteinText;
        set
        {
            _proteinText = value;
            ProteinErrors = FoodValidator.GetMacronutrientErrors(value);
            OnPropertyChanged(nameof(ProteinErrors));
            if (ProteinErrors == String.Empty)
                _food.GramsOfProtein = float.Parse(value);
        }
    }

    private string _carbText = String.Empty;
    public string CarbText
    {
        get => _carbText;
        set
        {
            _carbText = value;
            CarbErrors = FoodValidator.GetMacronutrientErrors(value);
            OnPropertyChanged(nameof(CarbErrors));

            if (CarbErrors == String.Empty)
                _food.GramsOfCarbs = float.Parse(value);
        }
    }

    private string _fatText = String.Empty;
    public string FatText
    {
        get => _fatText;
        set
        {
            _fatText = value;
            FatErrors = FoodValidator.GetMacronutrientErrors(value);
            OnPropertyChanged(nameof(FatErrors));

            if (FatErrors == String.Empty)
                _food.GramsOfFat = float.Parse(value);
        }
    }

    private string _servingSizeText = String.Empty;
    public string ServingSizeText 
    {
        get => _servingSizeText;
        set
        {
            _servingSizeText = value;
            ServingSizeErrors = FoodValidator.GetServingSizeErrors(value);
            OnPropertyChanged(nameof(ServingSizeErrors));

            if (ServingSizeErrors == String.Empty)
                _food.ServingSize = value;
        }
    }
    public string NotesText { get; set; } = String.Empty;
    public string NameErrors { get; set; } = String.Empty;
    public string CalorieErrors { get; set; } = String.Empty;
    public string ProteinErrors { get; set; } = String.Empty;
    public string CarbErrors { get; set; } = String.Empty;
    public string FatErrors { get; set; } = String.Empty;
    public string ServingSizeErrors { get; set; } = String.Empty;
    public bool HasErrors => !string.IsNullOrEmpty(NameErrors + CalorieErrors + ProteinErrors + CarbErrors + FatErrors + ServingSizeErrors);

    
    public CreateOrUpdateFoodViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        _food = new Food();
        CreateOrUpdateFoodCommand = new CreateOrUpdateFoodCommand(_mainViewModel, this);
        CancelAddOrEditCommand = new CancelAddOrEditCommand(_mainViewModel);

        SetTextBoxValuesToFoodValues();
    }

    private void SetTextBoxValuesToFoodValues()
    {
        NameText = Food.Name ?? String.Empty;
        CalorieText = Food.Calories == 0 ? String.Empty : Food.Calories.ToString();
        ProteinText = Food.GramsOfProtein == 0 ? String.Empty : Food.GramsOfProtein.ToString();
        CarbText = Food.GramsOfCarbs == 0 ? String.Empty : Food.GramsOfCarbs.ToString();
        FatText = Food.GramsOfFat == 0 ? String.Empty : Food.GramsOfFat.ToString();
        ServingSizeText = Food.ServingSize ?? String.Empty;
        NotesText = Food.Notes ?? String.Empty;
    }
}
