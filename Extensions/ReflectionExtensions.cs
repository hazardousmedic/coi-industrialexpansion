using System;
using System.Reflection;

namespace IndustrialExpansion.Extensions
{
    internal static class ReflectionExtensions
    {
        public static void SetPrivateProperty<T>(this object obj, string propertyName, T value)
        {
            Type type = obj.GetType();
            while (type != null)
            {
                PropertyInfo prop = type.GetProperty(propertyName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(obj, value);
                    return;
                }
                FieldInfo field = type.GetField("<" + propertyName + ">k__BackingField",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                if (field != null)
                {
                    field.SetValue(obj, value);
                    return;
                }
                type = type.BaseType;
            }
            throw new InvalidOperationException(
                $"Property or backing field '{propertyName}' not found on {obj.GetType().Name} or its base types.");
        }

        public static void SetPrivateStaticProperty<T>(Type type, string fieldName, T value)
        {
            FieldInfo field = type.GetField(fieldName,
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (field == null)
            {
                field = type.GetField("<" + fieldName + ">k__BackingField",
                    BindingFlags.Static | BindingFlags.NonPublic);
            }
            if (field != null)
            {
                field.SetValue(null, value);
                return;
            }
            throw new InvalidOperationException(
                $"Static field or backing field '{fieldName}' not found on {type.Name}.");
        }
    }
}