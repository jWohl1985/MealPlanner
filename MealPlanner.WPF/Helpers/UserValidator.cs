using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take100.WPF.Helpers;

public static class UserValidator
{
    public static readonly int MAX_NAME_LENGTH = 20;

    public static readonly int MIN_AGE = 16;
    public static readonly int MAX_AGE = 85;

    public static readonly float MIN_WEIGHT = 60;
    public static readonly float MAX_WEIGHT = 500;

    public static readonly float MIN_NECK = 8;
    public static readonly float MAX_NECK = 30;

    public static readonly float MIN_WAIST = 20;
    public static readonly float MAX_WAIST = 70;

    public static readonly float MIN_HIPS = 20;
    public static readonly float MAX_HIPS = 70;

    public static string GetNameInputErrors(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return "Name cannot be empty";
        if (input.Length > MAX_NAME_LENGTH)
            return "Name is too long";

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
