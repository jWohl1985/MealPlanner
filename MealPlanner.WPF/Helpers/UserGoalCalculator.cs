using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take100.Domain.Models;

namespace Take100.WPF.Helpers;

public class UserGoalCalculator
{
    public static readonly int CALORIE_WIGGLE_ROOM = 50; // how many calories the user is allowed to be over/under their goal
    public static readonly int MACRONUTRIENT_WIGGLE_ROOM = 7; // how many grams the user is allowed to be over/under on each macronutrient
    public static readonly int PROTEIN_CALORIES_PER_GRAM = 4;
    public static readonly int CARB_CALORIES_PER_GRAM = 4;
    public static readonly int FAT_CALORIES_PER_GRAM = 9;

    private readonly DietUser _dietUser;

    private float BodyFatPercentage;
    private bool UserIsObese;
    private float LeanBodyMassInPounds;
    private float BaseMetabolicRate;
    private float TotalDailyEnergyExpenditure;
    
    
    public int CalorieGoal { get; private set; }
    public int ProteinGramGoal { get; private set; }
    public int CarbGramMinimum { get; private set; }
    public int CarbGramMaximum { get; private set; }
    public int FatGramGoal { get; private set; }

    public UserGoalCalculator(DietUser dietUser)
    {
        _dietUser = dietUser;

        BodyFatPercentage = CalculateBodyFatPercentage();
        UserIsObese = (_dietUser.IsMale && BodyFatPercentage > 25) || (!_dietUser.IsMale && BodyFatPercentage > 35);
        LeanBodyMassInPounds = CalculateLeanBodyMassInPounds();
        BaseMetabolicRate = CalculateBaseMetabolicRate();
        TotalDailyEnergyExpenditure = CalculateTotalDailyEnergyExpenditure();

        CalorieGoal = CalculateCalorieGoal();
        ProteinGramGoal = CalculateProteinGrams();
        FatGramGoal = CalculateFatGrams();

        CarbGramMinimum = CalculateCarbMinimumGrams();
        CarbGramMaximum = CalculateCarbMaximumGrams();
    }

    private float CalculateBodyFatPercentage()
    {
        return _dietUser.IsMale ? CalculateMaleBodyFatPercentage() : CalculateFemaleBodyFatPercentage();
    }

    private float CalculateMaleBodyFatPercentage()
    {
        // US Navy formula for male body fat
        float term1 = 86.010f * (float)Math.Log10(_dietUser.WaistCircumferenceInches - _dietUser.NeckCircumferenceInches);
        float term2 = 70.041f * (float)Math.Log10(_dietUser.HeightInInches);
        float term3 = 36.76f;

        return (term1 - term2 + term3)/100;
    }

    private float CalculateFemaleBodyFatPercentage()
    {
        // US Navy formula for female body fat
        float term1 = 163.205f * (float)Math.Log10(_dietUser.WaistCircumferenceInches + _dietUser.HipCircumferenceInches - _dietUser.NeckCircumferenceInches);
        float term2 = 97.684f * (float)Math.Log10(_dietUser.HeightInInches);
        float term3 = 78.387f;

        return (term1 - term2 - term3)/100;
    }

    private float CalculateLeanBodyMassInPounds()
    {
        // Lean body mass is weight without fat included
        float nonFatPercentage = 1 - BodyFatPercentage;
        return _dietUser.WeightInLbs * nonFatPercentage;
    }

    private float CalculateBaseMetabolicRate()
    {
        // Katch-McArdle formula for BMR
        float leanBodyMassInKilograms = LeanBodyMassInPounds / 2.2f;
        return 370 + (21.6f * leanBodyMassInKilograms);
    }

    private float CalculateTotalDailyEnergyExpenditure()
    {
        float activityMultiplier = 1.15f + (.05f * _dietUser.HoursExercisePerWeek);

        return BaseMetabolicRate * activityMultiplier;
    }

    private int CalculateCalorieGoal()
    {
        float calorieGoal = _dietUser.Goal switch
        {
            0 => TotalDailyEnergyExpenditure * .75f,   // goal is to lose weight, need to eat less than TDEE
            1 => TotalDailyEnergyExpenditure,          // goal is to maintain weight, need to eat at TDEE level
            2 => TotalDailyEnergyExpenditure * 1.1f,   // goal is to gain weight, need to eat slightly above TDEE
            _ => throw new Exception("User goal was not 0, 1, or 2"),
        };

        return (int)calorieGoal;
    }

    private int CalculateProteinGrams()
    {
        float proteinGoal = _dietUser.Goal switch
        {
            0 => UserIsObese ? LeanBodyMassInPounds : _dietUser.WeightInLbs * 1.1f,  // goal is to lose weight, need more protein
            1 or 2 => _dietUser.WeightInLbs,
            _ => throw new Exception("User goal was not 0, 1, or 2"),
        };

        return (int)proteinGoal;
    }

    private int CalculateFatGrams()
    {
        float fatGoal = _dietUser.Goal switch
        {
            0 => _dietUser.WeightInLbs * .2f,
            1 => _dietUser.WeightInLbs * .25f,
            2 => _dietUser.WeightInLbs * .3f,
            _ => throw new Exception("User goal was not 0, 1, or 2"),
        };

        return (int)fatGoal;
    }

    private int CalculateCarbMinimumGrams()
    {
        float minimumCalories = CalorieGoal - CALORIE_WIGGLE_ROOM;

        float minCarbCalories = minimumCalories - (ProteinGramGoal * PROTEIN_CALORIES_PER_GRAM) - (FatGramGoal * FAT_CALORIES_PER_GRAM);

        return (int)(minCarbCalories / CARB_CALORIES_PER_GRAM);
    }

    private int CalculateCarbMaximumGrams()
    {
        float maximumCalories = CalorieGoal + CALORIE_WIGGLE_ROOM;

        float maxCarbCalories = maximumCalories - (ProteinGramGoal * PROTEIN_CALORIES_PER_GRAM) - (FatGramGoal * FAT_CALORIES_PER_GRAM);

        return (int)(maxCarbCalories / CARB_CALORIES_PER_GRAM);
    }

}
