using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take100.WPF.ViewModels;

namespace Take100.WPF.Helpers
{
    public static class FoodValidator
    {
        public static readonly int MAXIMUM_FOOD_NAME_LENGTH = 30;
        public static readonly int MAXIMUM_SERVING_SIZE_LENGTH = 20;
        public static readonly int MINIMUM_CALORIES = 0;
        public static readonly int MAXIMUM_CALORIES = 3000;
        public static readonly int MINIMUM_MACRONUTRIENTS = 0;
        public static readonly int MAXIMUM_MACRONUTRIENTS = 300;

        public static string GetFoodNameErrors(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Food name cannot be empty";

            if (input.Length > MAXIMUM_FOOD_NAME_LENGTH)
                return $"Food name is too long";

            return String.Empty;
        }

        public static string GetCalorieErrors(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Field cannot be empty";

            if (false == float.TryParse(input, out float calories))
                return "Field must be a valid number";

            if (calories < 0 || calories > 3000)
                return $"Number must be between {MINIMUM_CALORIES}-{MAXIMUM_CALORIES}";

            return String.Empty;
        }

        public static string GetMacronutrientErrors(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Field cannot be empty";

            if (false == float.TryParse(input, out float inputNumber))
                return "Field must be a valid number.";

            if (inputNumber < 0 || inputNumber > 300)
                return $"Number must be between {MINIMUM_MACRONUTRIENTS}-{MAXIMUM_MACRONUTRIENTS}";

            return String.Empty;
        }

        public static string GetServingSizeErrors(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Serving size cannot be empty";

            if (input.Length > MAXIMUM_SERVING_SIZE_LENGTH)
                return $"Serving size is too long";

            return String.Empty;
        }
    }
}
