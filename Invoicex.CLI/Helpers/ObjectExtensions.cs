namespace Invoicex.CLI.Helpers;

/// <summary>
/// Provides extension methods for objects.
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// Gets the value of the property specified by the property path.
    /// </summary>
    /// <param name="obj">The object to get the property value from.</param>
    /// <param name="propertyPath">The property path.</param>
    /// <returns>The property value.</returns>
    public static object? GetPropertyValue(this object? obj, string propertyPath)
    {
        if (obj == null || string.IsNullOrEmpty(propertyPath))
            return null;

        string[] properties = propertyPath.Split('.');

        object? value = obj;
        foreach (string prop in properties)
        {
            if (value == null)
                return null;

            var type = value.GetType();
            var propertyInfo = type.GetProperty(prop);
            if (propertyInfo == null)
                return null;

            value = propertyInfo.GetValue(value);
        }
        
        return value;
    }
}