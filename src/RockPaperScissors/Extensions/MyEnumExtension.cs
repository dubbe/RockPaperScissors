using System;
using System.ComponentModel;

public static class MyEnumExtensions
{
    /// <summary>
    /// Extension to view enum-description as string
    /// </summary>
    /// <param name="val">The enum-value</param>
    /// <returns></returns>
    public static string ToDescriptionString(this Enum val)
    {
        DescriptionAttribute[] attributes = (DescriptionAttribute[])val
           .GetType()
           .GetField(val.ToString())
           .GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : string.Empty;
    }
} 