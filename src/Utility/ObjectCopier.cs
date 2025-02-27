using System;
using System.Reflection;

namespace ii.InfinityEngine
{
    public static class ObjectCopier
    {
        public static T Clone<T>(T source)
        {
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            T clone = Activator.CreateInstance<T>();

            foreach (PropertyInfo property in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.CanRead && property.CanWrite)
                {
                    property.SetValue(clone, property.GetValue(source));
                }
            }

            foreach (FieldInfo field in typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                field.SetValue(clone, field.GetValue(source));
            }

            return clone;
        }
    }    
}