using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlanner.WPF.Helpers;

public static class InputValidation
{
    // Food constraints
    public static readonly int MAXIMUM_FOOD_NAME_LENGTH = 30;
    public static readonly int MAXIMUM_SERVING_SIZE_LENGTH = 20;
    public static readonly int MAXIMUM_NOTES_LENGTH = 50;
    public static readonly int MAXIMUM_CALORIES = 3000;
    public static readonly int MAXIMUM_MACRONUTRIENTS = 300;

    // User constraints
    public static readonly int MAXIMUM_USERNAME_LENGTH = 20;
    public static readonly int MINIMUM_AGE = 16;
    public static readonly int MAXIMUM_AGE = 85;
    public static readonly float MINIMUM_WEIGHT = 60;
    public static readonly float MAXIMUM_WEIGHT = 500;
    public static readonly float MINIMUM_NECK = 8;
    public static readonly float MAXIMUM_NECK = 30;
    public static readonly float MINIMUM_WAIST = 20;
    public static readonly float MAXIMUM_WAIST = 70;
    public static readonly float MINIMUM_HIPS = 20;
    public static readonly float MAXIMUM_HIPS = 70;

    public static string GetStringInputErrors(string input, int maxLength, bool commasAllowed=false, bool canBeEmpty=false)
    {
        if (!canBeEmpty && string.IsNullOrWhiteSpace(input))
            return "Field cannot be empty";

        if (input.Length > maxLength)
            return "Field is too long";

        // We don't want commas in most strings because it will mess up the CSV file that the database export generates
        if (!commasAllowed && input.Contains(','))
            return "Field cannot contain commas";

        return String.Empty;
    }

    public static string GetIntInputErrors(string input, int min, int max)
    {
        if (false == int.TryParse(input, out int result))
            return "Field must be an integer";

        if (result < min || result > max)
            return $"Field must be between {min}-{max}";

        return String.Empty;
    }

    public static string GetFloatInputErrors(string input, float min, float max)
    {
        if (false == float.TryParse(input, out float result))
            return "Field must be a number";

        if (result < min || result > max)
            return $"Field must be between {min}-{max}";

        return String.Empty;
    }

}
