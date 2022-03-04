using System.ComponentModel.DataAnnotations;

namespace FuzzySetsCalc.Models
{
    public class ChartDisplaySettings
    {
        [Range(0.01, 10)]
        public float Precision { get; set; } = 0.125f;
        [CustomValidation(typeof(ChartDisplaySettings), nameof(ValidateMaximumX))]
        public float MaximumX { get; set; } = 20;
        [CustomValidation(typeof(ChartDisplaySettings), nameof(ValidateMinimumX))]
        public float MinimumX { get; set; } = 0;

        public static ValidationResult ValidateMaximumX(float maxX, ValidationContext context)
        {
            return context.ObjectInstance is ChartDisplaySettings settings && maxX > settings.MinimumX ? ValidationResult.Success : new ValidationResult("Maximum X not greater than Minimum X");
        }
        public static ValidationResult ValidateMinimumX(float minX, ValidationContext context)
        {
            return context.ObjectInstance is ChartDisplaySettings settings && minX < settings.MaximumX ? ValidationResult.Success : new ValidationResult("Minimum X not less than Maximum X");
        }

    }
}
