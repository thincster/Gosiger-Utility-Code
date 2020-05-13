using System.Globalization;
using System.Windows.Controls;

namespace Okuma_Monitor_Tools.Utilities
{

    /// <summary>
    /// Checks for the Minimum Characters in a control.
    /// </summary>
    public class MinimumCharacterRule : ValidationRule
    {
        /// <summary>
        /// Minimum number of characters to validate..
        /// </summary>
        public int MinimumCharacters { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;

            if (charString.Length < MinimumCharacters)
            {
                return new ValidationResult(false, $"Must be at least {MinimumCharacters} characters.");

            }
            else
            {
                return new ValidationResult(true, null);
            }

        }
    }

    /// <summary>
    /// Checks for empty string in a control
    /// </summary>
    public class EmptyCharacterRule : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            if (string.IsNullOrWhiteSpace(charString))
            {
                return new ValidationResult(false, "Cannot be empty");
            }

            return new ValidationResult(true, null);
        }
    }

    /// <summary>
    /// Checks for the Minimum number  of numeric  characters in a control.
    /// </summary>
    public class MinimumNumberRule : ValidationRule
    {
        /// <summary>
        /// Minimum number of numeric characters to validate..
        /// </summary>
        public int MinimumNumbers { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;

            long num = 0;

            if (value != null)
            {
                bool canConvert = long.TryParse(value.ToString(), out num);

                if (charString.Length < MinimumNumbers || !canConvert)
                {
                    return new ValidationResult(false, $"Must be at least {MinimumNumbers} numbers.");

                }

            }


            return new ValidationResult(true, null);


        }
    }

    public class CheckIPOctetsRule : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            long num = 0;

            if (!string.IsNullOrWhiteSpace(value.ToString()) && charString.Length > 0 && charString.Length <= 3)
            {
                bool canConvert = long.TryParse(value.ToString(), out num);

                if (canConvert != true)
                {
                    return new ValidationResult(false, $"Must be a numeric value.");
                }
            }

            if (charString.Length == 0 || charString.Length > 3)
            {
                return new ValidationResult(false, $"Must be 1 to 3 digits.");

            }

            if (num == 0 || num > 255)
            {
                return new ValidationResult(false, $"Must be beween 1 and 255.");
            }

            return new ValidationResult(true, null);

        }
    }
}





