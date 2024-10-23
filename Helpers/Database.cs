using System.Reflection;
using Microsoft.Data.Sqlite;

namespace Helpers;
class Database
{
    public static string GetNameAttributes(object obj)
    {
        PropertyInfo[] properties = GetProperties(obj);

        string result = "";

        foreach (PropertyInfo property in properties)
        {
            if (property.GetValue(obj) != null)
            {
                result += property.Name + ", ";
            }
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
            if(property.GetValue(obj) != null){
                result += "@" + property.Name + ", ";
            }
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
            if (property.GetValue(obj) != null)
            {
                result += property.Name + " = @" + property.Name + ", ";
            }
        }

        if (result.EndsWith(", "))
        {
            result = result.Remove(result.Length - 2);
        }

        return result;
    }

    public static void AddParametersInNonQuery(SqliteCommand command, object obj)
    {

        var parameters = GetProperties(obj);

        foreach (var parameter in parameters)
        {
            command.Parameters.AddWithValue($"@{parameter.Name}", parameter.GetValue(obj));
        }
    }

    private static PropertyInfo[] GetProperties(object obj)
    {
        Type type = obj.GetType();
        return type.GetProperties();
    }
}