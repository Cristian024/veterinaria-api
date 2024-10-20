using System.Reflection;

namespace Helpers;
class Database
{
    public static string GetNameAttributes(object obj)
    {
        PropertyInfo[] properties = GetProperties(obj);

        string result = "";

        foreach (PropertyInfo property in properties)
        {
            result += property.Name + ", ";
        }

        if (result.EndsWith(", "))
        {
            result = result.Remove(result.Length - 2);
        }

        return result;
    }

    public static string GetNameAttributesInsertion(object obj)
    {
        PropertyInfo[] properties = GetProperties(obj);

        string result = "";

        foreach (PropertyInfo property in properties)
        {
            result += "@" + property.Name + ", ";
        }

        if (result.EndsWith(", "))
        {
            result = result.Remove(result.Length - 2);
        }

        return result;
    }

    public static string GetNameAttributesUpdate(object obj)
    {
        PropertyInfo[] properties = GetProperties(obj);

        string result = "";

        foreach (PropertyInfo property in properties)
        {
            result += property.Name + " = @" + property.Name + ", ";
        }

        if (result.EndsWith(", "))
        {
            result = result.Remove(result.Length - 2);
        }

        return result;
    }

    private static PropertyInfo[] GetProperties(object obj)
    {
        Type type = obj.GetType();
        return type.GetProperties();
    }
}