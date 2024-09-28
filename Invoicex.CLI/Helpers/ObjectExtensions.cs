namespace Invoicex.CLI.Helpers;

public static class ObjectExtensions
{
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