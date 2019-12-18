using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Controls;

namespace DriverPlan
{
    public class ValueWithinRangeRule : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public ValueWithinRangeRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int hValue;

            try
            {
                hValue = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }
            

            if (hValue < Min || hValue > Max)
            {
                return new ValidationResult(false, $"Please enter an age in the range: {Min}-{Max}.");
            }
            return ValidationResult.ValidResult;
        }
    }
}