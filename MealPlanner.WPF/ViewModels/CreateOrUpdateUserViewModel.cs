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

public class CreateOrUpdateUserViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;

    public INavigator Navigator => _mainViewModel.Navigator;
    public ICommand CreateOrUpdateUserCommand { get; set; }
    public ICommand CancelAddOrEditCommand { get; set; }

    private DietUser _dietUser;
    public DietUser DietUser
    {
        get => _dietUser;
        set
        {
            _dietUser = value;
            SetInputValuesToUserValues();
        }
    }

    private string _nameText = String.Empty;
    public string NameText
    {
        get => _nameText;
        set
        {
            _nameText = value;
            NameErrors = UserValidator.GetNameInputErrors(value);
            OnPropertyChanged(nameof(NameErrors));

            if (NameErrors == String.Empty)
                _dietUser.Name = value;
        }
    }

    private string _ageText = String.Empty;
    public string AgeText
    {
        get => _ageText;
        set
        {
            _ageText = value;
            AgeErrors = UserValidator.GetIntInputErrors(value, UserValidator.MIN_AGE, UserValidator.MAX_AGE);
            OnPropertyChanged(nameof(AgeErrors));

            if (AgeErrors == String.Empty)
                _dietUser.Age = int.Parse(value);
        }
    }

    private string _selectedSex = "Female";
    public string SelectedSex
    {
        get => _selectedSex;
        set
        {
            _selectedSex = value;
            _dietUser.IsMale = (SelectedSex == "Male");
            OnPropertyChanged(nameof(IsFemale));

            if (IsFemale)
            {
                HipErrors = UserValidator.GetFloatInputErrors(value, UserValidator.MIN_HIPS, UserValidator.MAX_HIPS);
                OnPropertyChanged(nameof(HipErrors));
            }
            else
            {
                HipErrors = String.Empty;
                OnPropertyChanged(nameof(HipErrors));
            }
        }
    }

    public bool IsFemale => _selectedSex == "Female"; // lets the view know we need the hip circumference for females

    private string _weightText = String.Empty;
    public string WeightText
    {
        get => _weightText;
        set
        {
            _weightText = value;
            WeightErrors = UserValidator.GetFloatInputErrors(value, UserValidator.MIN_WEIGHT, UserValidator.MAX_WEIGHT);
            OnPropertyChanged(nameof(WeightErrors));

            if (WeightErrors == String.Empty)
                _dietUser.WeightInLbs = int.Parse(_weightText);
        }
    }

    private string _heightFeetSelection = "5";
    public string HeightFeetSelection
    {
        get => _heightFeetSelection;
        set
        {
            _heightFeetSelection = value;
            _dietUser.HeightFeetComponent = int.Parse(value);
        }
    }

    private string _heightInchesSelection = "0";
    public string HeightInchesSelection
    {
        get => _heightInchesSelection;
        set
        {
            _heightInchesSelection = value;
            _dietUser.HeightInchComponent = int.Parse(value);
        }
    }
    
    private string _selectedHoursExercise = "0";
    public string SelectedHoursExercise
    {
        get => _selectedHoursExercise;
        set
        {
            _selectedHoursExercise = value;
            _dietUser.HoursExercisePerWeek = int.Parse(value);
        }
    }

    private string _selectedGoal = "lose weight";
    public string SelectedGoal
    {
        get => _selectedGoal;
        set
        {
            _selectedGoal = value;
            _dietUser.Goal = value switch
            {
                "Lose weight" => 0,
                "Maintain weight" => 1,
                "Gain weight" => 2,
                _ => 0,
            };
        }
    }

    private string _neckText = String.Empty;
    public string NeckText
    {
        get => _neckText;
        set
        {
            _neckText = value;
            NeckErrors = UserValidator.GetFloatInputErrors(value, UserValidator.MIN_NECK, UserValidator.MAX_NECK);
            OnPropertyChanged(nameof(NeckErrors));

            if (NeckErrors == String.Empty)
                _dietUser.NeckCircumferenceInches = float.Parse(value);
        }
    }

    private string _waistText = String.Empty;
    public string WaistText
    {
        get => _waistText;
        set
        {
            _waistText = value;
            WaistErrors = UserValidator.GetFloatInputErrors(value, UserValidator.MIN_WAIST, UserValidator.MAX_WAIST);
            OnPropertyChanged(nameof(WaistErrors));

            if (WaistErrors == String.Empty)
                _dietUser.WaistCircumferenceInches = float.Parse(value);
        }
    }

    private string _hipText = String.Empty;
    public string HipText
    {
        get => _hipText;
        set
        {
            _hipText = value;
            HipErrors = UserValidator.GetFloatInputErrors(value, UserValidator.MIN_HIPS, UserValidator.MAX_HIPS);
            if (HipErrors == String.Empty)
                _dietUser.HipCircumferenceInches = float.Parse(value);
            if (!IsFemale)
                HipErrors = String.Empty;

            OnPropertyChanged(nameof(HipErrors));
        }
    }

    public string NameErrors { get; set; } = String.Empty;
    public string AgeErrors { get; set; } = String.Empty;
    public string WeightErrors { get; set; } = String.Empty;
    public string HeightErrors { get; set; } = String.Empty;
    public string NeckErrors { get; set; } = String.Empty;
    public string WaistErrors { get; set; } = String.Empty;
    public string HipErrors { get; set; } = String.Empty;

    public bool HasErrors => !string.IsNullOrEmpty(NameErrors + AgeErrors + WeightErrors + HeightErrors + NeckErrors + WaistErrors + HipErrors);

    public CreateOrUpdateUserViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        _dietUser = new DietUser();
        CreateOrUpdateUserCommand = new CreateOrUpdateUserCommand(_mainViewModel, this);
        CancelAddOrEditCommand = new CancelAddOrEditCommand(_mainViewModel);

        SetInputValuesToUserValues();
    }

    private void SetInputValuesToUserValues()
    {
        NameText = _dietUser.Name ?? String.Empty;
        AgeText = _dietUser.Age == 0 ? String.Empty : _dietUser.Age.ToString();
        SelectedSex = _dietUser.IsMale ? "Male" : "Female";
        WeightText = _dietUser.WeightInLbs == 0 ? String.Empty : _dietUser.WeightInLbs.ToString();
        HeightFeetSelection = _dietUser.HeightFeetComponent == 0 ? "5" : _dietUser.HeightFeetComponent.ToString();
        HeightInchesSelection = _dietUser.HeightInchComponent == 0 ? "0" : _dietUser.HeightInchComponent.ToString();
        SelectedHoursExercise = _dietUser.HoursExercisePerWeek.ToString();
        SelectedGoal = _dietUser.Goal switch
        {
            0 => "Lose weight",
            1 => "Maintain weight",
            2 => "Gain weight",
            _ => "Lose weight",
        };
        NeckText = _dietUser.NeckCircumferenceInches == 0 ? String.Empty : _dietUser.NeckCircumferenceInches.ToString();
        WaistText = _dietUser.WaistCircumferenceInches == 0 ? String.Empty : _dietUser.WaistCircumferenceInches.ToString();
        HipText = _dietUser.HipCircumferenceInches == 0 ? String.Empty : _dietUser.HipCircumferenceInches.ToString();
    }
}
