using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

public class HashGenerator
{
    internal static String GenerateKey(Object sourceObject)
    {
        if (sourceObject == null)
        {
            throw new ArgumentNullException(nameof(sourceObject));
        }

        var hashString = ComputeHash(sourceObject);
        return hashString;
    }

    private static string ComputeHash(object obj)
    {
        var serializedObject = SerializeObject(obj);
        using (var sha256 = SHA256.Create())
        {
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(serializedObject));
            var sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }

    private static string SerializeObject(object obj)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        var sb = new StringBuilder();
        SerializeObjectRecursive(obj, sb, new HashSet<object>());
        return sb.ToString();
    }

    private static void SerializeObjectRecursive(object obj, StringBuilder sb, HashSet<object> visited)
    {
        if (obj == null || visited.Contains(obj))
        {
            return;
        }

        visited.Add(obj);

        var objType = obj.GetType();
        sb.Append($"<{objType.Name}>");

        foreach (PropertyInfo prop in objType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (prop.PropertyType.IsInterface)
                continue;

            var propValue = prop.GetValue(obj);
            if (propValue == null)
                continue;

            sb.Append($"<{prop.Name}>");
            if (prop.PropertyType.IsPrimitive || prop.PropertyType == typeof(string))
            {
                sb.Append(propValue);
            }
            else if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType))
            {
                foreach (object item in (IEnumerable)propValue)
                {
                    SerializeObjectRecursive(item, sb, visited);
                }
            }
            else
            {
                SerializeObjectRecursive(propValue, sb, visited);
            }
            sb.Append($"</{prop.Name}>");
        }

        sb.Append($"</{objType.Name}>");
    }
}