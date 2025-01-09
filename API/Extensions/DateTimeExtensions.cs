using System;

namespace API.Extensions;

/// <summary>
/// Provides extension methods for DateTime-related calculations.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Calculates the age based on the provided date of birth.
    /// </summary>
    /// <param name="dob">The date of birth as a DateOnly object.</param>
    /// <returns>The calculated age in years.</returns>
    public static int CalculateAge(this DateOnly dob)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - dob.Year;

        // Adjust age if the birthday hasn't occurred yet this year.
        if (dob > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}
